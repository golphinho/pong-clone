using System.Collections;
using System.Collections.Generic;
using UnityEditor.AnimatedValues;
using UnityEngine;

public class CPU : PaddleBase
{
    Rigidbody2D ballRB;

    GameObject ball;

    enum CPUDifficulty {freePoints, easy, normal, hard, veryHard};

    [SerializeField]
    CPUDifficulty currentCPUDifficulty = CPUDifficulty.normal; 

    //mínimo y máximo del rango de números entre los que se va a elegir aleatoriamente un offset con respecto a la posición en y de la pelota.
    //podría hacerse con un Vector2, pero me da cierta pereza implementarlo así que se queda así 😎👍
    float directionOffsetMinimum;
    float directionOffsetMaximum;


    float directionOffset;   

    void Awake()
    {
        ball = GameObject.FindGameObjectWithTag("Ball");
        ballRB = ball.GetComponent<Rigidbody2D>();


        //modifica el rango de aleatoriedad del offset en la dirección del objeto para cada dificultad
        if (currentCPUDifficulty == CPUDifficulty.normal)
        {
            directionOffsetMinimum = -2f;
            directionOffsetMaximum = 2f;

        }else if((currentCPUDifficulty == CPUDifficulty.hard))
        {
            directionOffsetMinimum = -1f;
            directionOffsetMaximum = 1f;

        }
        else if ((currentCPUDifficulty == CPUDifficulty.easy))
        {
            directionOffsetMinimum = -3f;
            directionOffsetMaximum = 3f;

        }
        else if ((currentCPUDifficulty == CPUDifficulty.veryHard))
        {
            directionOffsetMinimum = 0f;
            directionOffsetMaximum = 0f;

        }
        else if ((currentCPUDifficulty == CPUDifficulty.freePoints))
        {
            directionOffsetMinimum = -5f;
            directionOffsetMaximum = 5f;

        }
    }

    private void Start()
    {
        directionOffset = Random.Range(directionOffsetMinimum, directionOffsetMaximum);
        StartCoroutine(OffsetResetter());
    }

    private void FixedUpdate()
    {
        if (ball != null && ballRB != null)
        {
            //mueve el objeto hacia la pelota si esta se dirige hacia él, y hacia el centro si la bola se dirige al otro jugador.
            //no se moverá exactamente hacia donde está la pelota para evitar que gane siempre
            if (ballRB.velocity.x < 0)
            {
                //mueve el objeto hacia la pelota con un cierto margen aleatorio
                this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(this.transform.position.x, ball.transform.position.y + directionOffset, 0f), (_paddleSpeed * Time.deltaTime));
            }
            else if (ballRB.velocity.x > 0)
            {
                //mueve el objeto hacia el centro
                this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(this.transform.position.x, 0f, 0f), (_paddleSpeed * Time.deltaTime));
            }
        }
    }

    //resetea el offset de la dirección del objeto cada segundo, para evitar comportamientos erráticos cambiándolo cada vez que se llama a FixedUpdate
    IEnumerator OffsetResetter()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        yield return new WaitForSeconds(1);
        directionOffset = Random.Range(directionOffsetMinimum, directionOffsetMaximum);
        StartCoroutine (OffsetResetter());
    }
}
