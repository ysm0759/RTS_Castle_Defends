using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public enum KeyState
{
    NUM1,
    NUM2,
    NUM3,
    //NUM4,
    //NUM5,
    SKILL_COUNT, 
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
    SKILL_SHOW_CANCEL,
    SKILL_USE,
    SKILL_TRACE,
    SKILL_USING_CANT_MOVE,
    SKILL_USING_CAN_MOVE,
    SKILL_DONE,
    SKILL_NONE,

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
    public SkillKind skillKind;

    private void Awake()
    {
        Instance = this;
        keyState = KeyState.NONE;
        skill = Skill.SKILL_NONE;
        skillKind = SkillKind.NONE;
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (Skill.SKILL_USING_CANT_MOVE == skill || Skill.SKILL_USING_CAN_MOVE == skill)
                return;
            keyState = KeyState.NUM1;
            if (RTSUserUnitControlManager.instance.isSelectedHero())
                skill = Skill.SKILL_SHOW;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (Skill.SKILL_USING_CANT_MOVE == skill || Skill.SKILL_USING_CAN_MOVE == skill)
                return;
            keyState = KeyState.NUM2;
            if (RTSUserUnitControlManager.instance.isSelectedHero())
                skill = Skill.SKILL_SHOW;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (Skill.SKILL_USING_CANT_MOVE == skill || Skill.SKILL_USING_CAN_MOVE == skill)
                return;
            keyState = KeyState.NUM3;
            if (RTSUserUnitControlManager.instance.isSelectedHero())
                skill = Skill.SKILL_SHOW;
        }
        //else if (Input.GetKeyDown(KeyCode.Alpha4))
        //{
        //    if (Skill.SKILL_USING_CANT_MOVE == skill || Skill.SKILL_USING_CAN_MOVE == skill)
        //        return;
        //    keyState = KeyState.NUM4;
        //    if (RTSUserUnitControlManager.instance.isSelectedHero())
        //        skill = Skill.SKILL_SHOW;
        //}
        //else if (Input.GetKeyDown(KeyCode.Alpha5))
        //{
        //    if (Skill.SKILL_USING_CANT_MOVE == skill || Skill.SKILL_USING_CAN_MOVE == skill)
        //        return;
        //    keyState = KeyState.NUM5;
        //    if (RTSUserUnitControlManager.instance.isSelectedHero())
        //        skill = Skill.SKILL_SHOW;
        //}
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            keyState = KeyState.TAB;
            if (Skill.SKILL_SHOW == skill)
                skill = Skill.SKILL_SHOW_CANCEL;
            InGameUnitHpBar.uiOnOff = !InGameUnitHpBar.uiOnOff;
        }

        else if (Input.GetKeyDown(KeyCode.A))
        {
            keyState = KeyState.A;
            if (Skill.SKILL_SHOW == skill)
                skill = Skill.SKILL_SHOW_CANCEL;
            CursorManager.instance.SetCursor(CursorType.ATTACK);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            keyState = KeyState.S;

            RTSUserUnitControlManager.instance.StopSelectUnit();
            if (Skill.SKILL_SHOW == skill)
                skill = Skill.SKILL_SHOW_CANCEL;
        }
        else if (Input.GetKeyDown(KeyCode.H))
        {
            keyState = KeyState.H;
            RTSUserUnitControlManager.instance.HoldSelectUnit();
            if (Skill.SKILL_SHOW == skill)
                skill = Skill.SKILL_SHOW_CANCEL;
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //마우스는 MouseClick에 있음
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            //마우스는 MouseClick에 있음
            //if (skill == Skill.SKILL_SHOW)
            //    skill = Skill.SKILL_SHOW_CANCEL;
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
        else if (Input.GetKeyDown(KeyCode.L))
        {
            EnemySpawnManager.instance.SetEnemySpawn();
            
        }
        else if (Input.anyKeyDown)
        {
            keyState = KeyState.NONE;
            if (Skill.SKILL_SHOW == skill)
                skill = Skill.SKILL_SHOW_CANCEL;
        }

        

        //SkillState();

    }


    private void SkillState()
    {
        if (Skill.SKILL_USING_CANT_MOVE == skill || Skill.SKILL_USING_CAN_MOVE == skill)
        {

        }
        else if (keyState >= KeyState.SKILL_COUNT && RTSUserUnitControlManager.instance.isSelectedHero())
        {
            skill = Skill.SKILL_SHOW;
        }
        else if (keyState == KeyState.TAB ||
                 keyState == KeyState.A ||
                 keyState == KeyState.ESC ||
                 keyState == KeyState.H ||
                 keyState == KeyState.NONE ||
                 keyState == KeyState.S)
        {
            skill = Skill.SKILL_SHOW_CANCEL;
        }

    }




}
