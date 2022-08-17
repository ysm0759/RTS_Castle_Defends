using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSUserUnitControlManager : MonoBehaviour
{
    [SerializeField]
    private UserUnitSpawner unitSpawner;
    [SerializeField]
    private UnitStoreUI unitStoreUI;
    private List<UnitController> selectedUnitList;          //플레이어가 클릭 or 드래그로 선택한 유닛
    public List<UnitController> unitList{private set; get;} //맵에 존재하는 모든 유닛

    static private RTSUserUnitControlManager Instance;


    private MouseClick mouseClick;

    public UnitController hero;




    static public RTSUserUnitControlManager instance
    {
        get
        {
            return Instance;
        }
    }



    private void Awake()
    {
        Instance = this;
        selectedUnitList = new List<UnitController>();
        mouseClick = GetComponent<MouseClick>();

    }

    //마우스 클릭으로 유닛을 선택할 때 호출
    public void ClickSelectUnit(UnitController newUnit)
    {
        //기존에 선택되어 있는 모든 유닛 해제
        DeselectAll();
        //하나만 선택
        SelectUnit(newUnit);
    }

    //Shift + 마우스 클릭으로 유닛을 선택할 떄 호출
    public void ShiftClickSelectUnit(UnitController newUnit)
    {
        // 기존에 선택되어 있는 유닛을 선택했으면
        if (selectedUnitList.Contains(newUnit))
        {
            //선택 해제
            DeselectUnit(newUnit);
        }
        // 새로운 유닛을 선택했으면
        else
        {
            //선택
            SelectUnit(newUnit);
        }
    }

    public void DragSelectUnit(UnitController newUnit)
    {
        if( !selectedUnitList.Contains(newUnit))
        {
            SelectUnit(newUnit);
        }
    }

    public void DeselectAll()
    {
        for (int i = 0; i < selectedUnitList.Count; ++i)
        {
            selectedUnitList[i].DeselectUnit();
        }
        selectedUnitList.Clear();
    }

    public void MoveSelectUnits(Vector3 end) //유닛 배치
    {
        int cnt = selectedUnitList.Count;
        Vector3 tmpVec = end;
        int dir = 0;
        int max = 2;
        int dirCnt = 0;
        float place = 1f;
        // dir 0 오른쪽 
        // dir 1 아래
        // dir 2 왼쪽
        // dir 3 위 

        for (int i = 0; i < cnt; ++i)
        {
            if(KeyManager.instance.keyState == KeyState.A)
            {
                selectedUnitList[i].AttackMove();

            }
            else
            {
                selectedUnitList[i].DirectMove();
            }
            if(0 == i)
            {
                selectedUnitList[i].MoveTo(tmpVec);
                tmpVec.z += place;
                dirCnt = 1;
                continue;
            }
            else
            {
                if (dirCnt == max)
                {
                    dir++;
                    dirCnt = 0;
                    if (4 == dir)
                    {

                        selectedUnitList[i].MoveTo(tmpVec);
                        max += 2;
                        tmpVec.z += place;
                        dirCnt = 1;
                        dir = 0;
                        continue;
                    }
                }

                selectedUnitList[i].MoveTo(tmpVec);
                if (0 == dir)
                {
                    tmpVec.x += place;
                }
                else if(1 == dir)
                {
                    tmpVec.z -= place;
                }
                else if(2 == dir)
                {
                    tmpVec.x -=place;
                }
                else if(3 == dir)
                {
                    tmpVec.z += place;
                }

                dirCnt++;

            }
        }
        KeyManager.instance.keyState = KeyState.NONE;
    }

    //매개변수로 받아온  newUnit 선택 설정
    private void SelectUnit(UnitController newUnit)
    {   
        // 유닛이 선택되었을 때 호출하는 메소드
        newUnit.SelectUnit();
        // 선택한 유닛 정보를 리스트에 저장
        selectedUnitList.Add(newUnit);
    }


    public void SelectHero()
    {
        if(hero.isSelected())
        {
            DeselectAll();
        }
        // 유닛이 선택되었을 때 호출하는 메소드
        hero.SelectUnit();
        // 선택한 유닛 정보를 리스트에 저장
        selectedUnitList.Add(hero);
    }

    public bool isSelectedHero()
    {
        if( null == hero)
        {
            return false;
        }
        return hero.isSelected();
    }

    //매개 변수로 받아온 newUnit을 선택 해제 설정
    private void DeselectUnit(UnitController newUnit)
    {
        // 유닛이 해제되었을 떄 호출하는 메소드
        newUnit.DeselectUnit();
        // 선택한 유닛 정보를 리스트에서 삭제
        selectedUnitList.Remove(newUnit);   
    }

    public void HoldSelectUnit()
    {
        for (int i = 0; i < selectedUnitList.Count; ++i)
        {
            selectedUnitList[i].Hold();
        }
    }

    public void StopSelectUnit()
    {
        for (int i = 0; i < selectedUnitList.Count; ++i)
        {
            selectedUnitList[i].Stop();
        }
    }



    public void SetSkillPointOnOff(bool onOff)
    {
        mouseClick.SetSkillPointOnOff(onOff);
    }

    public void InitUnit()
    {
        unitList = unitSpawner.SpawnUnits(unitStoreUI.GetScriptableData(), unitStoreUI.GetIsBuys());

        for (int i = 0; i < unitList.Count; i++)
        {
            if (unitList[i].tag == "Hero")
            {
                hero = unitList[i];
            }
        }
    }


    public void AttackFocus(Collider hit)
    {
        for (int i = 0; i < selectedUnitList.Count; ++i)
        {
            selectedUnitList[i].AttackFocus(hit);
        }
    }

}


	

