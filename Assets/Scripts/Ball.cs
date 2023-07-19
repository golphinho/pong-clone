using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using TMPro;
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

    public static bool counterScreenShouldAppear = true;

    readonly int[] initialBallDirection = { -1, 1 };

    [SerializeField]
    GameObject CounterScreenObject;
    [SerializeField]
    TMP_Text CounterText;

    [SerializeField]
    GameObject ballParticleSystem;

    [SerializeField]
    GameObject self;

    bool ballIsDestroyed = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {       

        //La velocidad en x siempre es 1 para evitar problemas (quita las var)
        UnityEngine.Random.InitState(System.DateTime.Now.Millisecond);
        
        //le da al inicio del juego una velocidad determinada a la pelota (la direcci�n y velocidad del saque inicial es aleatoria;
        //la velocidad en x es siempre (1 � -1) * _initialBallSpeed para evitar problemas)
        rb.velocity = new Vector2(initialBallDirection[Random.Range(0, initialBallDirection.Length)], Random.Range(-1.5f, 1.5f)) * _initialBallSpeed;
        Debug.Log("VELOCIDAD INICIAL X: " + rb.velocity.x + "VELOCIDAD INICIAL Y: " + rb.velocity.y);

        //Hace aparecer el contador del inicio de la partida
        if (counterScreenShouldAppear)
        {
            Time.timeScale = 0f;
            StartCoroutine(CounterScreen());
            
        }
    }

    void Update()
    {
        //hace que la bola se desactive si se sale de la pantalla (y activa el sistema de part�culas de su destrucci�n)
        if (transform.position.x > 10 || transform.position.x < -10)
        {            
            if (ballParticleSystem != null && ballIsDestroyed == false)
            {
                ballParticleSystem.SetActive(true);
                AudioManager.Instance.Play("BallDestruction");
                self.GetComponent<SpriteRenderer>().enabled = false;
                rb.velocity = Vector2.zero;
                ballIsDestroyed = true;
            }         
            
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

    IEnumerator CounterScreen()
    {
        CounterScreenObject.SetActive(true);
        CounterText.SetText("3");
        AudioManager.Instance.Play("CounterDown");
        yield return new WaitForSecondsRealtime(1f);
        CounterText.SetText("2");
        AudioManager.Instance.Play("CounterDown");
        yield return new WaitForSecondsRealtime(1f);
        CounterText.SetText("1");
        AudioManager.Instance.Play("CounterDown");
        yield return new WaitForSecondsRealtime(1f);
        AudioManager.Instance.Play("CounterDown");
        CounterScreenObject.SetActive(false);
        Time.timeScale = 1f;
        counterScreenShouldAppear = false;
    }
}
