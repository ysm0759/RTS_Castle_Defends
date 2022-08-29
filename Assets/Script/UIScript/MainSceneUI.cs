using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneUI : MonoBehaviour
{

    public void OnClickQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }


    public void OnClickStart()
    {
        GameManager.instance.ReadyScene();
    }

    public void OnClikcTutorial()
    {

    }
}
