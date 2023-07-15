using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    Rigidbody2D rb;

    //velocidad inicial de la bola
    [SerializeField]
    float _initialBallSpeed;

    //determina el límite de velocidad al que puede llegar la bola tras chocar con las palas
    [SerializeField]
    float _speedLimit;

    //determina lo mucho que acelera la bola al chocar con una pala. La velocidad final de la pelota tras el choque es la inicial multiplicada por este número.
    [SerializeField]
    float _collisionAcceleration;

    //array usada para elegir aleatoriamente -1 ó 1, con el fin de decidir si inicialmente la pelota va hacia la derecha o hacia la izquierda
    int[] sentidoInicial = {-1, 1};

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        //le da al inicio del juego una velocidad determinada a la pelota (el sentido del saque inicial es aleatorio)
        rb.velocity = new Vector2(sentidoInicial[UnityEngine.Random.Range(0, sentidoInicial.Length)] * _initialBallSpeed, sentidoInicial[UnityEngine.Random.Range(0, sentidoInicial.Length)] * _initialBallSpeed);
    }

    void Update()
    {
        //hace que la bola se desactive si se sale de la pantalla
        if (transform.position.x > 10 || transform.position.x < -10)
        {
            Destroy(gameObject);
            //activar sistema de partículas guapo            
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {        

        //si choca con una de las palas, su velocidad aumentará hasta cierto límite
        if (collision.collider.CompareTag("Paddle") && Mathf.Sqrt( Mathf.Pow((rb.velocity.x), 2f) + Mathf.Pow((rb.velocity.y), 2f)) <= _speedLimit)
        {
            rb.velocity = new Vector2(rb.velocity.x * _collisionAcceleration, rb.velocity.y * _collisionAcceleration);
            Debug.Log("VELOCIDAD SUBIDA, AHORA EN: " + Mathf.Sqrt(Mathf.Pow((rb.velocity.x), 2f) + Mathf.Pow((rb.velocity.y), 2f)));


        }else if (collision.collider.CompareTag("Paddle") && Mathf.Sqrt(Mathf.Pow((rb.velocity.x), 2f) + Mathf.Pow((rb.velocity.y), 2f)) > _speedLimit)
        {
            Debug.Log("EXCEDISTE LÍM DE VELOCIDAD!!!!!!!");
        }

    }
}
