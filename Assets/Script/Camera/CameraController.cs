using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Screen Move")]
    [SerializeField] float panSpeed = 20f;
    [SerializeField] float panBorderThickness = 10f;
    [SerializeField] Vector2 panLimitX;
    [SerializeField] Vector2 panLimitZ;
 


    [Header("Scoroll")]
    [SerializeField] float scrollSpeed;
    [SerializeField] float minY;
    [SerializeField] float maxY;



    Vector3 pos;


    private void Update()
    {

        if (GameManager.instance.gameState == GameState.GAME_START || GameManager.instance.gameState == GameState.TOWER_SETTING)
        {

            pos = transform.position;

            CameraZoom();
            CameraMove();

            transform.position = pos;
        }


    }

    void CameraZoom()
    {

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        pos.y -= scroll * scrollSpeed *100f * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

    }

    void CameraMove()
    {

        if (Input.GetKey(KeyCode.UpArrow) || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            pos.z += panSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.DownArrow) || Input.mousePosition.y <= panBorderThickness)
        {
            pos.z -= panSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.mousePosition.x <= panBorderThickness)

        {
            pos.x -= panSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            pos.x += panSpeed * Time.deltaTime;
        }

        pos.x = Mathf.Clamp(pos.x, panLimitX.x, panLimitX.y);
        pos.z = Mathf.Clamp(pos.z, panLimitZ.x, panLimitZ.y);

    }
}
