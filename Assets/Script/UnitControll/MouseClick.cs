using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseClick : MonoBehaviour
{
    [SerializeField]
    private LayerMask   layerUnit;
    [SerializeField]
    private LayerMask   layerGround;

    private Camera  mainCamera;
    [SerializeField]
    private GameObject skillPoint;


    private void Awake()
    {
        mainCamera = Camera.main;

    }


    private void LateUpdate() {

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (EventSystem.current.IsPointerOverGameObject())
            {
                Debug.Log("???");
                return;
            }
            if (KeyManager.instance.skill == Skill.SKILL_SHOW && RTSUserUnitControlManager.instance.isSelectedHero())
            {
                Debug.Log(KeyManager.instance.skill);
                if (KeyManager.instance.skill != Skill.SKILL_USING_CANT_MOVE && KeyManager.instance.skill != Skill.SKILL_USING_CANT_MOVE)
                {
                    KeyManager.instance.skill = Skill.SKILL_USE; 
                }

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerGround))
                {
                    skillPoint.SetActive(true);
                    skillPoint.transform.position = hit.point;
                    RTSUserUnitControlManager.instance.hero.MoveTo(hit.point);
                }

                return;
            }

            // trace상태이면서 이동
            if (KeyManager.instance.keyState == KeyState.A)
            {
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerGround))
                {
                    RaycastHit enumyhit;
                    if (Physics.Raycast(ray, out enumyhit, Mathf.Infinity, LayerMask.GetMask("Enemy")))
                    {
                        RTSUserUnitControlManager.instance.AttackFocus(enumyhit.collider);
                    }
                    else
                    {
                        RTSUserUnitControlManager.instance.MoveSelectUnits(hit.point);
                    }

                    CursorManager.instance.SetCursor(CursorType.DEFAULT);


                    return;
                }
            }
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerUnit))
            {
                if(hit.transform.GetComponent<UnitController>() == null) return;

                if(Input.GetKey(KeyCode.LeftShift))
                    RTSUserUnitControlManager.instance.ShiftClickSelectUnit(hit.transform.GetComponent<UnitController>());
                else
                    RTSUserUnitControlManager.instance.ClickSelectUnit(hit.transform.GetComponent<UnitController>());
            }
            else
            {
                if(!Input.GetKey(KeyCode.LeftShift))
                {
                    RTSUserUnitControlManager.instance.DeselectAll();
                }
            }
        }
        if(Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if( Physics.Raycast(ray, out hit , Mathf.Infinity,layerGround))
            {
                RTSUserUnitControlManager.instance.MoveSelectUnits(hit.point);
            }
        }
        
    }



    public void SetSkillPointOnOff(bool onOff)
    {
        skillPoint.SetActive(onOff);
    }







}
