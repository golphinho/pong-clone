using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : PaddleMovement
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Si el botón "VerticalPlayer2" está en estado pulsado, se mueve al personaje una cierta longitud
        if (Input.GetButton("VerticalPlayer2"))
        {

            //si se está pulsando el botón en sentido positivo (ver ProjectSettings > Input Manager), el objeto subirá
            if (Input.GetAxisRaw("VerticalPlayer2") > 0)
            {

                MoveUp();

                //si se pulsa en sentido negativo, el objeto bajará
            }
            else if (Input.GetAxisRaw("VerticalPlayer2") < 0)
            {

                MoveDown();

            }

        }
    }
}
