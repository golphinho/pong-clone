using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleMovement : MonoBehaviour
{
    
    Rigidbody2D rb;

    public float paddleSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //
        if (Input.GetButtonDown("Vertical"))
        {
            if (Input.GetAxisRaw("Vertical") < 0)
            {
                rb.velocity = new Vector2(0f ,-paddleSpeed * Time.deltaTime);

            }else if (Input.GetAxisRaw("Vertical") > 0)
            {
                rb.velocity = new Vector2(0f, paddleSpeed * Time.deltaTime);
            }
        }
    }
}
