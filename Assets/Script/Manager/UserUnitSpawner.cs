using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserUnitSpawner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> unitPrefab;

    private List<Transform> unitPrefabPosition;

    //오브젝트 풀링
    //오브젝트 풀링 하면서 
    // ?? 생성하고 


    
    public List<UnitController> SpawnUnits()
    {
        List<UnitController> unitList = new List<UnitController>();


        for (int i = 0 ; i < unitPrefab.Count ; ++i)
        {
            Vector3 position = new Vector3(120 , 10f ,120);

            GameObject clone = Instantiate(unitPrefab[i],position,Quaternion.identity);
            UnitController unit = clone.GetComponent<UnitController>();

            unitList.Add(unit);
            
        }
        //유닛 넣어주기
        
        return unitList;
    }




}
