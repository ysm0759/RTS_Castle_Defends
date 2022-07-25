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




    public List<UnitController> SpawnUnits(UnitDataScriptableObject[] tmpData, bool[] isBuys)
    {
        List<UnitController> unitList = new List<UnitController>();


        for(int i =0; i < tmpData.Length; i++)
        {

            Debug.Log("실행2");
            if (isBuys[i] == false)
            {
                continue;
            }
            Debug.Log("실행4");
            for (int j = 0;  j < tmpData[i].population;j++)
            {

                Debug.Log("실행3");
                Vector3 position = new Vector3(120, 5f, 120);
                GameObject clone = Instantiate(unitPrefab[i], position, Quaternion.identity);
                clone.GetComponent<UnitInfo>().SetData(tmpData[i]);

                UnitController unit = clone.GetComponent<UnitController>();

                unitList.Add(unit);
            }
        }



        for (int i = 0 ; i < unitPrefab.Count ; ++i)
        {
            Vector3 position = new Vector3(120 , 5f ,120);

            GameObject clone = Instantiate(unitPrefab[i],position,Quaternion.identity);
            UnitController unit = clone.GetComponent<UnitController>();

            unitList.Add(unit);
            
        }
        
        return unitList;
    }

    


}
