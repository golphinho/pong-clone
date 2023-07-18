using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField]
    TMP_Text scoreTextP1;
    [SerializeField]
    TMP_Text scoreTextP2;

    [SerializeField]
    TMP_Text rallyUI;

    [SerializeField]
    GameObject pauseMenu;

    [SerializeField]
    Button soundButton;

    [SerializeField]
    GameObject winP1;
    [SerializeField]
    GameObject winP2;
    [SerializeField]
    GameObject winCPU;

    [HideInInspector]
    public bool gameIsPaused = false;
    [HideInInspector]
    public int soundIsPaused = 0; //es int y no bool para poder usarlo como �ndice de array

    [SerializeField]
    GameObject mainmenuButtons1;
    [SerializeField]
    GameObject mainmenuButtons2;

    [SerializeField]
    GameObject difficultyButton;

    [SerializeField]
    TMP_Text bestRallyUI;

    //Los dos sprites del bot�n de sonido (que se intercambiar�n cada vez que se pulse el bot�n)
    [SerializeField] Sprite[] soundButtonSprites;    

    private void Start()
    {
        //Actualiza la puntuaci�n del jugador cada vez que se carga la escena (de forma que se mantenga el marcador actualizado)
        if (scoreTextP1 != null && scoreTextP2 != null)
        {
            scoreTextP1.text = GameManager.Instance.player1Score.ToString();
            scoreTextP2.text = GameManager.Instance.player2Score.ToString();
        }

        //reinicia el texto del contador de intercambios cada vez que se carga la escena
        if (rallyUI != null)
        {
            rallyUI.SetText(" ");
        }

        //Establece el sprite del bot�n de sonido dependiendo de si el sonido est� o no pausado
        soundButton.image.sprite = soundButtonSprites[soundIsPaused];

        //esribe el texto correspondiente en el bot�n de cambio de dificultad de la CPU
        if (difficultyButton != null)
        {
            if (PlayerPrefs.GetInt("CPU Difficulty") == 0)
            {
                difficultyButton.GetComponent<TMP_Text>().SetText("Dificultad: Puntos gratis");
            }
            else if (PlayerPrefs.GetInt("CPU Difficulty") == 1)
            {
                difficultyButton.GetComponent<TMP_Text>().SetText("Dificultad: facil");
            }
            else if (PlayerPrefs.GetInt("CPU Difficulty") == 2)
            {
                difficultyButton.GetComponent<TMP_Text>().SetText("Dificultad: normal");
            }
            else if (PlayerPrefs.GetInt("CPU Difficulty") == 3)
            {
                difficultyButton.GetComponent<TMP_Text>().SetText("Dificultad: dificil");
            }
            else if (PlayerPrefs.GetInt("CPU Difficulty") == 4)
            {
                difficultyButton.GetComponent<TMP_Text>().SetText("Dificultad: muy dificil");
            }

        }

        //escribe el mejor rally del jugador en el campo correspondiente
        if(bestRallyUI != null && PlayerPrefs.GetInt("Best Rally") != 0)
        {
            bestRallyUI.SetText("Best Rally: " + PlayerPrefs.GetInt("Best Rally"));
        }

    }

    private void Update()
    {

        //pausa el juego si se pulsa el bot�n de pausar
        if (Input.GetButtonDown("Pause") && gameIsPaused == false)
        {
            PauseGame();       
            
        }else if (Input.GetButtonDown("Pause") && gameIsPaused == true)
        {
            ResumeGame();
        }

        //cambia el estado del sonido del juego si se pulsa el bot�n correspondiente
        if (Input.GetButtonDown("Sound"))
        {
            SoundStateSwitch();
        }

        if (GameManager.Instance.player1Won == true)
        {
            winP1.SetActive(true);
            //TODO: Poner sonido
            AudioManager.Instance.Play("WinPlayer");
            Debug.Log("Victoria del jugador 1!");

            GameManager.Instance.player1Won = false;
            StartCoroutine(WaitAndGoToMainMenu(2f));
        }
        else if (GameManager.Instance.player2Won == true)
        {

            //se comprueba si el objeto en escena es el jugador 2 o la CPU (con sus componentes)
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("Paddle").Length; i++)
            {
                if (GameObject.FindGameObjectsWithTag("Paddle")[i].GetComponent<CPU>() != null)
                {
                    winCPU.SetActive(true);
                    AudioManager.Instance.Play("WinCPU");
                    Debug.Log("Victoria de la CPU!");

                }
                else if (GameObject.FindGameObjectsWithTag("Paddle")[i].GetComponent<Player2>() != null)
                {
                    winP2.SetActive(true);
                    AudioManager.Instance.Play("WinPlayer");
                    Debug.Log("Victoria del jugador 2!");
                }
            }

            GameManager.Instance.player2Won = false;
            StartCoroutine(WaitAndGoToMainMenu(2f));
        }
    }

    //Actualiza la puntuaci�n del jugador 1 en la UI
    public void UpdateScoreUIP1()
    {
        Debug.Log("ACTUALIZACI�N P1");        
        scoreTextP1.SetText(GameManager.Instance.player1Score.ToString());
    }

    //Actualiza la puntuaci�n del jugador 2 en la UI
    public void UpdateScoreUIP2()
    {
        Debug.Log("ACTUALIZACI�N P2");
        scoreTextP2.SetText(GameManager.Instance.player2Score.ToString());
    }

    public void UpdateRallyUI()
    {
        rallyUI.SetText("Rally:" + GameManager.Instance.rallyCounter);
    }

    //pausa el juego, mostrando el men� de pausa
    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;

        //TODO: Poner los sonidos que tocan
        AudioManager.Instance.Play("Pause");
        AudioManager.Instance.Pause("BGM");
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;

        AudioManager.Instance.UnPause("BGM");
    }

    //cambia el estado del audio, y de su bot�n
    public void SoundStateSwitch()
    {

        if (soundIsPaused == 1)
        {
            AudioListener.volume = 1f;
            soundIsPaused = 0;

        }
        else if(soundIsPaused == 0)
        {
            AudioListener.volume = 0f;
            soundIsPaused = 1;
        }

        soundButton.image.sprite = soundButtonSprites[soundIsPaused];
    }

    //Hace que se muestre el men� derivado del principal en el que se escoge entre el modo singleplayer o multiplayer (local)
    public void StartMenu_Play()
    {
        mainmenuButtons1.SetActive(false);
        mainmenuButtons2.SetActive(true);
    }

    //vuelve al men� anterior desde el men� en el que se escoge el modo de juego
    public void StartMenu_Back()
    {
        mainmenuButtons2.SetActive(false);
        mainmenuButtons1.SetActive(true);
    }

    //cierra el juego (desde el men� principal)
    public void StartMenu_Exit() {

        Application.Quit();
        Debug.Log("AAAAAAplicac�on Cerradaaaaaaaaaaaa");
    }

    public void GoToSettingsScene() {

        SceneManager.LoadScene("Scenes/SettingsMenu");


    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Scenes/StartMenu");
        GameManager.Instance.ResetPoints();
    }

    public void StartMenu_Singleplayer()
    {
        SceneManager.LoadScene("Scenes/GameCPU");
        Ball.counterScreenShouldAppear = true;
    }

    public void StartMenu_Multiplayer()
    {
        SceneManager.LoadScene("Scenes/Game2P");
        Ball.counterScreenShouldAppear = true;
    }

    //espera los segundos requeridos, resetea todos los puntos, y manda al jugador al men� principal
    IEnumerator WaitAndGoToMainMenu(float secondsToWait)
    {
        yield return new WaitForSeconds(secondsToWait);
        SceneManager.LoadScene("Scenes/StartMenu");
        GameManager.Instance.ResetPoints();

    }

    //Cambia la dificultad de la CPU del juego
    public void ChangeCPUDifficulty()
    {

        if (PlayerPrefs.GetInt("CPU Difficulty") <= 3)
        {
            PlayerPrefs.SetInt("CPU Difficulty", PlayerPrefs.GetInt("CPU Difficulty") + 1);
        }else if (PlayerPrefs.GetInt("CPU Difficulty") == 4)
        {
            PlayerPrefs.SetInt("CPU Difficulty", 0);
        }

        if (difficultyButton != null)
        {
            if (PlayerPrefs.GetInt("CPU Difficulty") == 0)
            {
                difficultyButton.GetComponent<TMP_Text>().SetText("Dificultad: Puntos gratis");
            }
            else if (PlayerPrefs.GetInt("CPU Difficulty") == 1)
            {
                difficultyButton.GetComponent<TMP_Text>().SetText("Dificultad: facil");
            }
            else if (PlayerPrefs.GetInt("CPU Difficulty") == 2)
            {
                difficultyButton.GetComponent<TMP_Text>().SetText("Dificultad: normal");
            }
            else if (PlayerPrefs.GetInt("CPU Difficulty") == 3)
            {
                difficultyButton.GetComponent<TMP_Text>().SetText("Dificultad: dificil");
            }
            else if (PlayerPrefs.GetInt("CPU Difficulty") == 4)
            {
                difficultyButton.GetComponent<TMP_Text>().SetText("Dificultad: muy dificil");
            }

        }

    }

}
