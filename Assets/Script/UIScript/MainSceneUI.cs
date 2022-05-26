using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneUI : MonoBehaviour
{

    public void OnClickQuit()
    {
        Application.Quit();
    }


    public void OnClickStart()
    {
        GameStateManager.instance.gameState = GameState.READY;
        GameManager.instance.ReadyScene();
    }

    public void OnClikcTutorial()
    {

    }
}
