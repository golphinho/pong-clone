using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody2D rb;

    //velocidad inicial de la bola
    [SerializeField]
    float _initialBallSpeed;

    //determina el l�mite de velocidad al que puede llegar la bola tras chocar con las palas
    [SerializeField]
    float _speedLimit = 19.25f;

    //determina lo mucho que acelera la bola al chocar con una pala. La velocidad final de la pelota tras el choque es la inicial multiplicada por este n�mero.
    [SerializeField]
    float _collisionAcceleration;

    //array usada para elegir aleatoriamente -1 � 1, con el fin de decidir si inicialmente la pelota va hacia la derecha o hacia la izquierda
    int[] sentidoInicial = {-1, 1};


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {

        //le da al inicio del juego una velocidad determinada a la pelota (el sentido del saque inicial es aleatorio)
        rb.velocity = new Vector2(sentidoInicial[UnityEngine.Random.Range(0, sentidoInicial.Length)], sentidoInicial[UnityEngine.Random.Range(0, sentidoInicial.Length)]) * _initialBallSpeed;
    }

    void Update()
    {
        //hace que la bola se desactive si se sale de la pantalla
        if (transform.position.x > 10 || transform.position.x < -10)
        {
            Destroy(gameObject);
            //activar sistema de part�culas guapo            
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {        

        //si choca con una de las palas, su velocidad aumentar� hasta cierto l�mite (tambi�n, independientemente de la velocidad de la pelota, subir� el contador de intercambio)
        if (collision.collider.CompareTag("Paddle") && rb.velocity.magnitude <= _speedLimit)
        {
            SpeedUpBall();
            GameManager.Instance.RallyCounterUp();

        }
        else if (collision.collider.CompareTag("Paddle") && rb.velocity.magnitude > _speedLimit)
        {
            Debug.Log("EXCEDISTE L�M DE VELOCIDAD!!!!!!!");
            GameManager.Instance.RallyCounterUp();

        }        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Comprueba con qu� trigger ha chocado para sumar los puntos correspondientes
        if (collision.CompareTag("Trigger1"))
        {
            GameManager.Instance.ScorePointP2(1);
        }
        else if (collision.CompareTag("Trigger2"))
        {
            GameManager.Instance.ScorePointP1(1);
        }
    }

    //Sube la velocidad de la pelota multiplicando el valor de su velocidad por un valor (_collisionAcceleration)
    private void SpeedUpBall()
    {
        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y) * _collisionAcceleration;
        Debug.Log("VELOCIDAD SUBIDA, AHORA EN: " + rb.velocity.magnitude);
    }

}
