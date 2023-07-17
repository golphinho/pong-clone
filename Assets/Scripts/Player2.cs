using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : PaddleBase
{
    //enumeración usada para poder pasar el input obtenido en Update a FixedUpdate
    public enum KeyState {Off, Down, Up};

    public KeyState vertical2State = KeyState.Off;

    UIManager uiManager;

    private void Awake()
    {
        uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
    }

    void Update()
    {
        //Si el botón "VerticalPlayer2" está en estado pulsado, se mueve al personaje una cierta longitud
        if (Input.GetButton("VerticalPlayer2") && uiManager.gameIsPaused == false)
        {

            //si se está pulsando el botón en sentido positivo (ver ProjectSettings > Input Manager), el objeto subirá
            if (Input.GetAxisRaw("VerticalPlayer2") > 0)
            {
                vertical2State = KeyState.Up;

                //si se pulsa en sentido negativo, el objeto bajará
            }
            else if (Input.GetAxisRaw("VerticalPlayer2") < 0)
            {
                vertical2State = KeyState.Down;
            }

        }
    }

    private void FixedUpdate()
    {
        if (vertical2State == KeyState.Up)
        {
            MoveUp();
        }
        else if (vertical2State == KeyState.Down)
        {
            MoveDown();
        }

        vertical2State = KeyState.Off;
    }


}