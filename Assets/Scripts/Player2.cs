using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : PaddleBase
{
    //enumeraci�n usada para poder pasar el input obtenido en Update a FixedUpdate
    public enum KeyState { Off, Down, Up, Mouse };

    public KeyState vertical2State = KeyState.Off;

    UIManager uiManager;

    [SerializeField]
    Camera mainCamera;

    private void Awake()
    {
        uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();

        Time.timeScale = 1.0f;
    }

    void Update()
    {
        //Si el bot�n "VerticalPlayer2" est� en estado pulsado, se mueve al personaje una cierta longitud
        if (Input.GetButton("VerticalPlayer2") && uiManager.gameIsPaused == false)
        {

            //si se est� pulsando el bot�n en sentido positivo (ver ProjectSettings > Input Manager), el objeto subir�
            if (Input.GetAxisRaw("VerticalPlayer2") > 0)
            {
                vertical2State = KeyState.Up;

                //si se pulsa en sentido negativo, el objeto bajar�
            }
            else if (Input.GetAxisRaw("VerticalPlayer2") < 0)
            {
                vertical2State = KeyState.Down;
            }

        }

        //si el bot�n izquierdo del rat�n se est� pulsando, el personaje se dirigir� hacia donde est� (en el eje y)
        if (Input.GetMouseButton(1))
        {
            vertical2State = KeyState.Mouse;
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
        else if (vertical2State == KeyState.Mouse)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, -mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCamera.transform.position.z)).y, 0f), (_paddleSpeed * Time.deltaTime));
        }

        vertical2State = KeyState.Off;


    }


}