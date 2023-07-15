using System.Collections;
using System.Collections.Generic;
using UnityEditor.AnimatedValues;
using UnityEngine;

public class CPU : PaddleBase
{
    Rigidbody2D ballRB;

    GameObject ball;

    [SerializeField]
    float directionOffsetMinimum = -1f;
    [SerializeField]
    float directionOffsetMaximum = 1f;


    float directionOffset;

    void Awake()
    {
        ball = GameObject.FindGameObjectWithTag("Ball");
        ballRB = ball.GetComponent<Rigidbody2D>();
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
                this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(this.transform.position.x, -0.5f, 0f), (_paddleSpeed * Time.deltaTime));
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
