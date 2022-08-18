﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserUnitSpawner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> unitPrefab;

    [SerializeField]
    private List<GameObject> heroPrefab;

    [SerializeField]
    private List<Transform> unitPrefabPosition;
    
    [SerializeField]
    private InGameUnitSelectButtons inGameUnitSelectButtons;

    //오브젝트 풀링
    //오브젝트 풀링 하면서 
    // ?? 생성하고 

    [SerializeField]
    public SubArray[] UnitIndex;

    [Serializable]
    public struct SubArray
    {
        [SerializeField]
        public GameObject[] LevelObjects;
    }



    public List<UnitController> SpawnUnits(UnitDataScriptableObject[] tmpData, bool[] isBuys)
    {
        List<UnitController> unitList = new List<UnitController>();

        Vector3 position = new Vector3(120, 5f, 120);



        for (int i =0; i < tmpData.Length; i++)
        {

            if (isBuys[i] == false)
            {
                continue;
            }

            //생성되는 부분
            inGameUnitSelectButtons.AddButton(tmpData[i]);

            Vector3 tmpVec = unitPrefabPosition[i].position;
            int dir = 0;
            int max = 2;
            int dirCnt = 0;
            float place = 4f;

            for (int j = 0;  j < tmpData[i].population;j++)
            {


                GameObject clone = Instantiate(UnitIndex[i].LevelObjects[tmpData[i].level - 1], position, Quaternion.identity);
                clone.GetComponent<UnitInfo>().SetData(tmpData[i]);
                UnitController unit = clone.GetComponent<UnitController>();
                unitList.Add(unit);
                inGameUnitSelectButtons.AddGroup(unit);
                if (0 == j)
                {
                    clone.transform.position = tmpVec;
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
                            clone.transform.position = tmpVec;

                            max += 2;
                            tmpVec.z += place;
                            dirCnt = 1;
                            dir = 0;
                            continue;
                        }
                    }
                    clone.transform.position = tmpVec;

                    if (0 == dir)
                    {
                        tmpVec.x += place;
                    }
                    else if (1 == dir)
                    {
                        tmpVec.z -= place;
                    }
                    else if (2 == dir)
                    {
                        tmpVec.x -= place;
                    }
                    else if (3 == dir)
                    {
                        tmpVec.z += place;
                    }

                    dirCnt++;

                }

            }
        }



        for (int i = 0 ; i < unitPrefab.Count ; ++i)
        {

            GameObject clone = Instantiate(unitPrefab[i],position,Quaternion.identity);
            UnitController unit = clone.GetComponent<UnitController>();
            unitList.Add(unit);
        }

        GameObject heroClone = Instantiate(heroPrefab[0], position, Quaternion.identity);
        UnitController heroUnitController = heroClone.GetComponent<UnitController>();
        unitList.Add(heroUnitController);

        return unitList;




    }


}
