using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Hero : MonoBehaviour
{

    public abstract void UseSkill(int num);
    public abstract bool isCoolDown(int num);
    public abstract void ShowRange(int num);

    public abstract void CancelSkill(int num);

    [SerializeField]
    private GameObject maker;
    private int prevSkill = 0;


    private void Update()
    {
        if (KeyManager.instance.skill == Skill.SKILL_USING_CANT_MOVE || KeyManager.instance.skill == Skill.SKILL_USING_CAN_MOVE)
        {
            return;
        }

        // 캔슬 되었을때
        if (KeyManager.instance.skill == Skill.SKILL_SHOW_CANCEL)
        {
            CancelSkill(prevSkill);
            return;
        }

        //   Key값이 1, 2 ,3, 4, 5 가아니면
        if ((int)KeyManager.instance.keyState >= (int)KeyState.SKILL_COUNT)
        {
            return;
        }
        //쿨타임 상태
        if (isCoolDown((int)KeyManager.instance.keyState) == true)
        {
            KeyManager.instance.keyState = KeyState.NONE;
            //CancelSkill(prevSkill);
            return;
        }


        if (prevSkill != (int)KeyManager.instance.keyState)
        {
            CancelSkill(prevSkill);
            prevSkill = (int)KeyManager.instance.keyState;
        }


        // 에임 상태
        if (KeyManager.instance.skill == Skill.SKILL_SHOW)
            ShowRange((int)KeyManager.instance.keyState);

        // 스킬 사용
        if (KeyManager.instance.skill == Skill.SKILL_USE)
            UseSkill((int)KeyManager.instance.keyState);

 

    }

}
