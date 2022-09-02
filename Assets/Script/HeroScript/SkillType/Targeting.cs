using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeting : SkillType
{
    [SerializeField]
    private Projector[] projector;

    private Camera mainCamera;

    private UnitController unitController;

    float range;
    RaycastHit hit;

    private void Start()
    {
        mainCamera = Camera.main;
        unitController = GetComponent<UnitController>();

        projector = FindObjectsOfType<Projector>();
    }

    //마우스에 따른 원 이미지
    public override void ShowSkill(float scope, float range)
    {
        KeyManager.instance.skillKind = SkillKind.TARGETING;

        CursorManager.instance.SetCursor(CursorType.SHOW_SKILL_TARGET);

        projector[(int)ProjectorType.RANGE].gameObject.transform.position = this.transform.position + (Vector3.up * 200f);
        projector[(int)ProjectorType.RANGE].orthographicSize = range;
        projector[(int)ProjectorType.RANGE].enabled = true;
        this.range = range;
    }


    // 스킬 사용
    public override void UseSkill(float range, IWarriorSkill skill)
    {
        KeyManager.instance.skillKind = SkillKind.TARGETING;

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Enemy"));



        if (hit.collider == null)
        {
            //대상을 못찾음
            Debug.Log("대상을 못찾음");
            unitController.SetNavStop();
            ShowCancel();
            KeyManager.instance.skill = Skill.SKILL_SHOW_CANCEL;
            return;
        }
        else
        {
            Debug.Log("상대 찾음");
            KeyManager.instance.skill = Skill.SKILL_TRACE;
            StartCoroutine(DrawClose(skill));

        }


    }

    IEnumerator DrawClose(IWarriorSkill skill)
    {
        Vector3 tmp;
        CursorManager.instance.SetCursor(CursorType.DEFAULT);
        while (true)
        {
            tmp = hit.transform.position - transform.position;
            projector[(int)ProjectorType.RANGE].gameObject.transform.position = this.transform.position + (Vector3.up * 200f);
            RTSUserUnitControlManager.instance.hero.MoveTo(hit.transform.position);

            if (KeyManager.instance.skill != Skill.SKILL_TRACE)
            {
                ShowCancel();
                yield break;
            }
            if(Input.GetKeyDown(KeyCode.Escape) ||
                Input.GetKeyDown(KeyCode.S) || 
                (Input.GetKeyDown(KeyCode.H) && RTSUserUnitControlManager.instance.isSelectedHero()) || 
                Input.GetKeyDown(KeyCode.Mouse1))
            {
                ShowCancel();
                yield break;
            }
            if (tmp.magnitude < range)
            {


                skill.StartCoolDown(hit.collider);
                ShowCancel();
                yield break;
            }

            yield return null;

        }

    }


    public override void ShowCancel()
    {
        if (KeyManager.instance.keyState != KeyState.A)
        {
            CursorManager.instance.SetCursor(CursorType.DEFAULT);
        }
        RTSUserUnitControlManager.instance.SetSkillPointOnOff(false);
        projector[(int)ProjectorType.RANGE].enabled = false;
    }


}
