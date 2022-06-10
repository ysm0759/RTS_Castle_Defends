using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeting : SkillType
{

    private float canUseSkillRange;


    private Camera mainCamera;


    private UnitController unitController;


    private void Start()
    {
        mainCamera = Camera.main;
        unitController = GetComponent<UnitController>();
    }

    //마우스에 따른 원 이미지
    public override void ShowSkill(float scope, float range)
    {
        CursorManager.instance.SetCursor(CursorType.SHOW_SKILL_TARGET);
    }


    // 스킬 사용
    public override void UseSkill(float range , IWarriorSkill skill)
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Enemy"));



        if (hit.collider == null)
        {
            //대상을 못찾음
            unitController.SetNavStop();
            ShowCancel();
            KeyManager.instance.skill = Skill.SKILL_SHOW_CANCEL;
            return;
        }
        else
        {
            // 사정거리 안에 들어오면 스킬쓰고 쿨 돌리기 작업
            RTSUserUnitControlManager.instance.hero.MoveTo(hit.transform.position);
            KeyManager.instance.skill = Skill.SKILL_USE;
            CursorManager.instance.SetCursor(CursorType.DEFAULT);
            skill.StartCoolDown();

        }



    }



    public override void ShowCancel()
    {
        if (KeyManager.instance.keyState != KeyState.A)
        {
            CursorManager.instance.SetCursor(CursorType.DEFAULT);
        }
        RTSUserUnitControlManager.instance.SetSkillPointOnOff(false);

    }


}
