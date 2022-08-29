using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonBossCamera : MonoBehaviour
{

    [SerializeField]
    private Transform foucs;
    private float rotateSpeed = 60f;
    private float zoomMax = 90f;
    private float zoomMin = 60f;
    private float zoomSpeed = 10;
    private Vector3 axisVec;


    void Start()
    {
        transform.LookAt(foucs);
    }

    void Update()
    {
        MainCameraRotate();
        CameraRotate();
        CameraZoom();
    }


    void MainCameraRotate()
    {
        transform.LookAt(foucs);
        if (GameState.MAIN == GameManager.instance.gameState)
            transform.RotateAround(foucs.position, Vector3.up, 30f * Time.deltaTime);

    }
    void CameraRotate()
    {
        transform.LookAt(foucs);
        if (Input.GetKey(KeyCode.Q))
            transform.RotateAround(foucs.position, Vector3.up, rotateSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.E))
            transform.RotateAround(foucs.position, Vector3.up, -rotateSpeed * Time.deltaTime);
    }
    void CameraZoom()
    {
        if(GameManager.instance.gameState != GameState.GAME_START)
        {
            return;
        }

        float tmpAxis = Input.GetAxis("Mouse ScrollWheel");


        if ((foucs.position - transform.position).sqrMagnitude > zoomMax * zoomMax)
        {
            if (tmpAxis < 0)
            {
                tmpAxis = 0;
            }
            else
            {

            }
        }
        if ((foucs.position - transform.position).sqrMagnitude < zoomMin * zoomMin)
        {
            if (tmpAxis > 0)
            {
                tmpAxis = 0; 
            }
            else
            {

            }
        }

        axisVec = transform.forward * tmpAxis * zoomSpeed;
        transform.position = transform.position + axisVec;

    }

}
