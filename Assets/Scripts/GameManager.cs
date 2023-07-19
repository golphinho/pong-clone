using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }

    public int player1Score = 0;
    public int player2Score = 0;

    public int rallyCounter = 0;

    UIManager uiManager;

    //para mostrar el contador de intercambios una vez este iguale o supere a showRally
    public int showRally = 12;

    //para saber si algún jugador ha ganado
    public bool player1Won = false;
    public bool player2Won = false;

    public int winScore = 10;

    public bool mainMenuMusicIsPlaying = false;

    private void Awake()
    {        
        uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();

        //Hace que sea un singleton (que solo haya una instancia del objeto en cada momento)
        if (Instance == null)
        {
            Instance = this;

            //Hace que el GameManager no se destruya al cargar la escena
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            //referencia ciertos objetos en el Instance cuando se reinicia la escena 
            Instance.uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
            

            Debug.Log("Más de un GameManager en escenaa");
            Object.Destroy(this.gameObject);
        }

    }

    //Le suma puntos al jugador 1, reinicia la escena y reinicia el contador de intercambio
    public void ScorePointP1(int scoreToAdd)
    {
        AudioManager.Instance.Play("Score");
        player1Score += scoreToAdd;

        uiManager.UpdateScoreUIP1();
        Debug.Log("PUNTUACIÓN DE P1: " + player1Score);


        if (player1Score >= winScore)
        {
            player1Won = true;
        }
        else
        {
            StartCoroutine(WaitAndResetScene(1f));
        }       
        RallyCounterReset();

    }

    //Le suma puntos al jugador 2, reinicia la escena y reinicia el contador de intercambio
    public void ScorePointP2(int scoreToAdd)
    {
        AudioManager.Instance.Play("Score");
        player2Score += scoreToAdd;

        uiManager.UpdateScoreUIP2();
        Debug.Log("PUNTUACIÓN DE P2: " + player1Score);


        if (player2Score >= winScore)
        {
            player2Won = true;
        }
        else
        {
            StartCoroutine(WaitAndResetScene(1f));
        }
        RallyCounterReset();
    }

    public void ResetPoints()
    {
        player1Score = 0;
        player2Score = 0;
    }

    //reinicia el contador de intercambios 
    public void RallyCounterReset()
    {        
        rallyCounter = 0;
    }

    //sube el contador de intercambios en uno, y comprueba el número actual de intercambios para mostrarlo en la pantalla y supera showRally
    public void RallyCounterUp()
    {
        rallyCounter++;
        Debug.Log("Contador de intercambio en: " + rallyCounter);

        if (rallyCounter >= showRally)
        {
            uiManager.UpdateRallyUI();
        }

        if (rallyCounter > PlayerPrefs.GetInt("Best Rally"))
        {
            PlayerPrefs.SetInt("Best Rally", rallyCounter);
        }
    }

    IEnumerator WaitAndResetScene(float secondsToWait)
    {        
        yield return new WaitForSeconds(secondsToWait);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}
