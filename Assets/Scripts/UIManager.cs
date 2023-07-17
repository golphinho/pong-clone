using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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

    private void Start()
    {
        //Actualiza la puntuación del jugador cada vez que se carga la escena (de forma que se mantenga el marcador actualizado)
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
    }

    //Actualiza la puntuación del jugador 1 en la UI
    public void UpdateScoreUIP1()
    {
        Debug.Log("ACTUALIZACIÓN P1");        
        scoreTextP1.SetText(GameManager.Instance.player1Score.ToString());
    }

    //Actualiza la puntuación del jugador 2 en la UI
    public void UpdateScoreUIP2()
    {
        Debug.Log("ACTUALIZACIÓN P2");
        scoreTextP2.SetText(GameManager.Instance.player2Score.ToString());
    }

    public void UpdateRallyUI()
    {
        rallyUI.SetText("Rally:" + GameManager.Instance.rallyCounter);
    }

    //pausa el juego, mostrando el menú de pausa
    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;

        //TODO: Poner los sonidos que tocan
        AudioManager.Instance.Play("Pause");
        AudioManager.Instance.Pause("BGM");
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;

        AudioManager.Instance.UnPause("BGM");
    }

}
