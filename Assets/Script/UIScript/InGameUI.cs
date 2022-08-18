using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{

    public Image[] skillUI;


    static private InGameUI Instance;
    static public InGameUI instance
    {
        get
        {
            return Instance;
        }
    }


    private void Awake()
    {
        Instance = this;
    }


}
