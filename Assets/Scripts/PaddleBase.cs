using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleBase : MonoBehaviour
{
    
    public float _paddleSpeed = 9.81f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //activa la animación de la pala al chocar con la pelota
        if (collision.collider.CompareTag("Ball"))
        {
            //está escrito de esta forma (y no con una variable con el animador), porque de la otra forma daba un error ignorable pero que me molestaba (lo mismo con la línea de OnCollisionExit)
            gameObject.GetComponent<Animator>().SetBool("hasCollidedWithBall", true);
        }        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //desactiva la animación de la pala al dejar de chocar con la pelota
        if (collision.collider.CompareTag("Ball"))
        {
            gameObject.GetComponent<Animator>().SetBool("hasCollidedWithBall", false);
        }
    }

    //Mueve al objeto que llama a la función hacia arriba
    public void MoveUp()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y + _paddleSpeed * Time.deltaTime);
    }

    //Mueve al objeto que llama a la función hacia abajo
    public void MoveDown()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y - _paddleSpeed * Time.deltaTime);
    }



}
