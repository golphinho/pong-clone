using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Player2 : PaddleBase
{
    //enumeración usada para poder pasar el input obtenido en Update a FixedUpdate
    public enum KeyState { Off, Down, Up, Touch };

    public KeyState vertical2State = KeyState.Off;

    UIManager uiManager;

    [SerializeField]
    Camera mainCamera;

    [SerializeField] LayerMask layerToDetect;

    private void Awake()
    {
        uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();

        Time.timeScale = 1.0f;
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

        for (int i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Moved)
            {
                Ray pointerRay = mainCamera.ScreenPointToRay(Input.GetTouch(i).position);

                if (Physics2D.Raycast(pointerRay.origin, pointerRay.direction, 15f, layerToDetect))
                {
                    vertical2State = KeyState.Touch;
                    Debug.DrawRay(pointerRay.origin, pointerRay.direction * 10f, Color.cyan);
                }

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
        else if (vertical2State == KeyState.Touch)
        {
            for (int i = 0; i < Input.touchCount; ++i)
            {
                Ray pointerRay = mainCamera.ScreenPointToRay(Input.GetTouch(i).position);

                if (Physics2D.Raycast(pointerRay.origin, pointerRay.direction, 15f, layerToDetect))
                {
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, -mainCamera.ScreenToWorldPoint(new Vector3(Input.GetTouch(i).position.x, Input.GetTouch(i).position.y, mainCamera.transform.position.z)).y, 0f), (_paddleSpeed * Time.deltaTime));
                }
            }
        }

        vertical2State = KeyState.Off;


    }


}