using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : PaddleBase
{
    //enumeración usada para poder pasar el input obtenido en Update a FixedUpdate
    public enum KeyState {Off, Down, Up, Mouse};

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
        //Si el botón "VerticalPlayer2" está en estado pulsado, se mueve al personaje una cierta longitud
        if (Input.GetButton("VerticalPlayer1") && uiManager.gameIsPaused == false)
        {

            //si se está pulsando el botón en sentido positivo (ver ProjectSettings > Input Manager), el objeto subirá
            if (Input.GetAxisRaw("VerticalPlayer1") > 0)
            {
                vertical2State = KeyState.Up;                

            //si se pulsa en sentido negativo, el objeto bajará
            }
            else if (Input.GetAxisRaw("VerticalPlayer1") < 0)
            {
                vertical2State = KeyState.Down;
            }            

        }

        //si el botón izquierdo del ratón se está pulsando, el personaje se dirigirá hacia donde está (en el eje y)
        if (Input.GetMouseButton(0))
        {
            vertical2State = KeyState.Mouse;
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
        else if (vertical2State == KeyState.Mouse)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, -mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCamera.transform.position.z)).y, 0f), (_paddleSpeed * Time.deltaTime));
        }

        vertical2State = KeyState.Off;

        
    }


}