using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : PaddleBase
{
    //enumeraci�n usada para poder pasar el input obtenido en Update a FixedUpdate
    public enum KeyState {Off, Down, Up};

    public KeyState vertical2State = KeyState.Off;

    UIManager uiManager;

    private void Awake()
    {
        uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();

        Time.timeScale = 1.0f;
    }

    void Update()
    {
        //Si el bot�n "VerticalPlayer2" est� en estado pulsado, se mueve al personaje una cierta longitud
        if (Input.GetButton("VerticalPlayer1") && uiManager.gameIsPaused == false)
        {

            //si se est� pulsando el bot�n en sentido positivo (ver ProjectSettings > Input Manager), el objeto subir�
            if (Input.GetAxisRaw("VerticalPlayer1") > 0)
            {
                vertical2State = KeyState.Up;                

            //si se pulsa en sentido negativo, el objeto bajar�
            }
            else if (Input.GetAxisRaw("VerticalPlayer1") < 0)
            {
                vertical2State = KeyState.Down;
            }            

        }
    }

    private void FixedUpdate()
    {
        if(vertical2State == KeyState.Up)
        {
            MoveUp();
        }else if(vertical2State == KeyState.Down) { 
            MoveDown();
        }

        vertical2State = KeyState.Off;
    }


}