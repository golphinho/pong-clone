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
        //Si el bot�n "VerticalPlayer2" est� en estado pulsado, se mueve al personaje una cierta longitud
        if (Input.GetButton("VerticalPlayer2"))
        {

            //si se est� pulsando el bot�n en sentido positivo (ver ProjectSettings > Input Manager), el objeto subir�
            if (Input.GetAxisRaw("VerticalPlayer2") > 0)
            {

                MoveUp();

                //si se pulsa en sentido negativo, el objeto bajar�
            }
            else if (Input.GetAxisRaw("VerticalPlayer2") < 0)
            {

                MoveDown();

            }

        }
    }
}
