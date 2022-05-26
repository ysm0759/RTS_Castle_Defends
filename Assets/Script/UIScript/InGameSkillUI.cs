using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameSkillUI : MonoBehaviour
{

    public Image[] skillUI;


    static private InGameSkillUI Instance;
    static public InGameSkillUI instance
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
