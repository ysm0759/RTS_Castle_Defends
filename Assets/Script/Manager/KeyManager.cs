using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public enum KeyState
{
    NUM1,
    NUM2,
    NUM3,
    NUM4,
    NUM5,
    SKILL_COUNT, //TODO 이상하면 지울것
    TAB,
    Q,
    E,
    A,
    S,
    H,
    SHIFT,
    ESC,
    SPACE,
    NONE,
}

public enum Skill
{
    SKILL_SHOW,
    SKILL_USE,
    SKILL_USING_CANT_MOVE,
    SKILL_USING_CAN_MOVE,
    SKILL_CANCEL,

}


public class KeyManager : MonoBehaviour
{
    static private KeyManager Instance;
    public UnityEvent onUnitState;

    static public KeyManager instance
    {
        get
        {
            return Instance;
        }
    }

    public int test = 0;

    public KeyState keyState;
    public Skill skill;

    private void Awake()
    {
        Instance = this;
        keyState = KeyState.NONE;
        skill = Skill.SKILL_CANCEL;
    }

    private void Update()
    {

        if (skill == Skill.SKILL_USING_CANT_MOVE || skill == Skill.SKILL_USING_CAN_MOVE)
        {
            Debug.Log(skill);
            return;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            keyState = KeyState.NUM1;
            if (RTSUserUnitControlManager.instance.isSelectedHero())
                skill = Skill.SKILL_SHOW;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            keyState = KeyState.NUM2;
            if (RTSUserUnitControlManager.instance.isSelectedHero())
                skill = Skill.SKILL_SHOW;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            keyState = KeyState.NUM3;
            if (RTSUserUnitControlManager.instance.isSelectedHero())
                skill = Skill.SKILL_SHOW;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            keyState = KeyState.NUM4;
            if (RTSUserUnitControlManager.instance.isSelectedHero())
                skill = Skill.SKILL_SHOW;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            keyState = KeyState.NUM5;
            if (RTSUserUnitControlManager.instance.isSelectedHero())
                skill = Skill.SKILL_SHOW;
        }

        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            keyState = KeyState.TAB;
            skill = Skill.SKILL_CANCEL;
        }

        else if (Input.GetKeyDown(KeyCode.A))
        {
            keyState = KeyState.A;
            skill = Skill.SKILL_CANCEL;
            CursorManager.instance.SetCursor(CursorType.ATTACK);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            keyState = KeyState.S;
            RTSUserUnitControlManager.instance.StopSelectUnit();
            skill = Skill.SKILL_CANCEL;
        }
        else if (Input.GetKeyDown(KeyCode.H))
        {
            keyState = KeyState.H;
            RTSUserUnitControlManager.instance.HoldSelectUnit();
            skill = Skill.SKILL_CANCEL;
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //마우스는 MouseClick에 있음
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            //마우스는 MouseClick에 있음
            if (skill == Skill.SKILL_USE)
                skill = Skill.SKILL_CANCEL;
        }

        else if (Input.GetKeyDown(KeyCode.Q))
        {
            //화면 전환은 Camera에 있음
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            //화면 전환은 Camera에 있음
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            RTSUserUnitControlManager.instance.SelectHero();
        }
        else if (Input.anyKeyDown)
        {
            keyState = KeyState.NONE;
            skill = Skill.SKILL_CANCEL;
        }


    }





}
