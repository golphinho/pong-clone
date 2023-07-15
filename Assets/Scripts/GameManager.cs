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

    int rallyCounter = 0;

    UIManager uiManager;    


    private void Awake()
    {
        uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();

        //Hace que sea un singleton (que solo haya una instancia del objeto en cada momento)
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("Más de un GameManager en escenaa");
            Object.Destroy(this.gameObject);
        }

        //Hace que el GameManager no se destruya al cargar la escena
        DontDestroyOnLoad(gameObject);



    }

    //Le suma puntos al jugador 1, reinicia la escena y reinicia el contador de intercambio
    public void ScorePointP1(int scoreToAdd)
    {
        //TODO: (sonido)
        player1Score += scoreToAdd;        

        uiManager.UpdateScoreUIP1();
        Debug.Log("PUNTUACIÓN DE P1: " + player1Score);

        StartCoroutine(WaitAndResetScene(1f));

        RallyCounterReset();

    }

    //Le suma puntos al jugador 2, reinicia la escena y reinicia el contador de intercambio
    public void ScorePointP2(int scoreToAdd)
    {
        //TODO: (sonido)
        player2Score += scoreToAdd;

        uiManager.UpdateScoreUIP2();
        Debug.Log("PUNTUACIÓN DE P2: " + player2Score);

        StartCoroutine(WaitAndResetScene(1f));

        RallyCounterReset();


    }

    //reinicia el contador de intercambios 
    public void RallyCounterReset()
    {        
        rallyCounter = 0;
    }

    //sube el contador de intercambios en uno
    public void RallyCounterUp()
    {
        rallyCounter++;
        Debug.Log("Contador de intercambio en: " + rallyCounter);
    }

    IEnumerator WaitAndResetScene(float secondsToWait)
    {        
        yield return new WaitForSeconds(secondsToWait);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);        
    }
}
