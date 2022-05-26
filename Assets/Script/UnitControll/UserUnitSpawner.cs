using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserUnitSpawner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> unitPrefab;

    private List<Transform> unitPrefabPosition;


    // 데이터를 얘한테 주면
    // 얘가 포지션 , 스크립트 , 기타등등으로  시작할 때 유닛을 줘야함
    // 어케 해야함 .. ?
    // 프리팹을 들고 있어야하나 .. ?

    public List<UnitController> SpawnUnits()
    {
        List<UnitController> unitList = new List<UnitController>();


        for (int i = 0 ; i < unitPrefab.Count ; ++i)
        {
            Vector3 position = new Vector3(120 + i*3, 10f ,120+i*3);

            GameObject clone = Instantiate(unitPrefab[i],position,Quaternion.identity);
            UnitController unit = clone.GetComponent<UnitController>();

            unitList.Add(unit);
            
        }
        //유닛 넣어주기
        
        return unitList;
    }




}
