using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ProjectorType
{
    SCOPE,
    RANGE,
}

public class Range : SkillType
{

    [SerializeField]
    private Projector[] projector;

    private Camera mainCamera;


    private Collider[] hitSkillPoint;
    private UnitController unitController;

    private void Start()
    {
        projector = FindObjectsOfType<Projector>();
        
        mainCamera = Camera.main;
        projector[(int)ProjectorType.SCOPE].enabled = false;
        projector[(int)ProjectorType.RANGE].enabled = false;

        unitController = GetComponent<UnitController>();

    }

    //마우스에 따른 원 이미지
    public override void ShowSkill(float scope, float range)
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground"));

        CursorManager.instance.SetCursor(CursorType.SHOW_SKILL_RANGE);
        projector[(int)ProjectorType.SCOPE].gameObject.transform.position = hit.point + (Vector3.up * 200f);
        projector[(int)ProjectorType.SCOPE].orthographicSize = scope;
        projector[(int)ProjectorType.SCOPE].enabled = true;

        projector[(int)ProjectorType.RANGE].gameObject.transform.position = this.transform.position + (Vector3.up * 200f);
        projector[(int)ProjectorType.RANGE].orthographicSize = range;
        projector[(int)ProjectorType.RANGE].enabled = true;


    }


    // 스킬 사용
    public override void UseSkill(float range  , IWarriorSkill skill)// 넘겨준 코루틴 받고)
    {

        CursorManager.instance.SetCursor(CursorType.DEFAULT);
        Collider[] hitSkillPoint;
        hitSkillPoint = Physics.OverlapCapsule(transform.position - Vector3.up * 100, transform.position - Vector3.down * 100, range, LayerMask.GetMask("Skill"));
        projector[(int)ProjectorType.RANGE].gameObject.transform.position = this.transform.position + (Vector3.up * 200f);
        if (hitSkillPoint.Length >= 1)
        {
            RTSUserUnitControlManager.instance.SetSkillPointOnOff(false);
            //스킬 사용
            // 코루틴 시작 가능 ?
            skill.StartCoolDown();
            unitController.SetNavStop();
            ShowCancel();
        }
    }


    public override void ShowCancel()
    {
        if(KeyManager.instance.keyState != KeyState.A)
        {
            CursorManager.instance.SetCursor(CursorType.DEFAULT);
        }
        RTSUserUnitControlManager.instance.SetSkillPointOnOff(false);
        projector[(int)ProjectorType.SCOPE].enabled = false;
        projector[(int)ProjectorType.RANGE].enabled = false;
    }


}
