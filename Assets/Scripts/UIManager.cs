using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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

    [HideInInspector]
    public bool gameIsPaused = false;
    [HideInInspector]
    public int soundIsPaused = 0; //es int y no bool para poder usarlo como �ndice de array

    //Los dos sprites del bot�n de sonido (que se intercambiar�n)
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

}
