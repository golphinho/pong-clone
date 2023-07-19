using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleBase : MonoBehaviour
{
    
    public float _paddleSpeed = 9.81f;

    string[] randomSoundEffect = { "Hit", "Hit2" };

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //activa la animaci�n de la pala al chocar con la pelota
        if (collision.collider.CompareTag("Ball"))
        {
            //est� escrito de esta forma (y no con una variable con el animador), porque de la otra forma daba un error ignorable pero que me molestaba (lo mismo con la l�nea de OnCollisionExit)
            gameObject.GetComponent<Animator>().SetBool("hasCollidedWithBall", true);
        }        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //desactiva la animaci�n de la pala al dejar de chocar con la pelota
        if (collision.collider.CompareTag("Ball"))
        {
            gameObject.GetComponent<Animator>().SetBool("hasCollidedWithBall", false);            
            AudioManager.Instance.Play(randomSoundEffect[Random.Range(0, randomSoundEffect.Length)]);
        }
    }

    //Mueve al objeto que llama a la funci�n hacia arriba
    public void MoveUp()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y + _paddleSpeed * Time.deltaTime);
    }

    //Mueve al objeto que llama a la funci�n hacia abajo
    public void MoveDown()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y - _paddleSpeed * Time.deltaTime);
    }



}
