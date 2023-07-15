using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    GameManager GameManager;
    [SerializeField]
    TMP_Text scoreTextP1;
    [SerializeField]
    TMP_Text scoreTextP2;

    private void Awake()
    {
        GameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        if (scoreTextP1 != null && scoreTextP2 != null)
        {
            scoreTextP1.text = GameManager.Instance.player1Score.ToString();
            scoreTextP2.text = GameManager.Instance.player2Score.ToString();
        }
    }

    public void UpdateScoreUIP1()
    {
        if (scoreTextP1 != null)
        {
            scoreTextP1.text = GameManager.Instance.player1Score.ToString();
        }
    }

    public void UpdateScoreUIP2()
    {
        if (scoreTextP2 != null)
        {
            scoreTextP2.text = GameManager.Instance.player2Score.ToString();
        }
    }


}
