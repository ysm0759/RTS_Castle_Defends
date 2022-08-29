using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public EnemyStageData stageData;

    public static EnemySpawnManager instance;

    [SerializeField] private GameObject rangeObject;
    [SerializeField] private BoxCollider rangeCollider;
    private int _enemyEA = 0;

    public int enemyEA
    {
        get
        {
            return _enemyEA;
        }
        set
        {
            _enemyEA = value;
            Debug.Log(_enemyEA);
            if(_enemyEA <= 0)
            {
                Debug.Log("게임 종료");
                stageData = stageData.NextStage;
                if(stageData != null)
                {
                    GameManager.instance.NextStage();
                }

            }
        }
    }


    private void Awake()
    {
        instance = this;
        rangeCollider = rangeObject.GetComponent<BoxCollider>();
    }

    public void SetEnemySpawn()
    {



        for (int i = 0; i < stageData.EnemySpawnData.Length; i++)
        {
            for (int j = 0; j < stageData.EnemySpawnData[i].EA; j++)
            {
                enemyEA++;

                Vector3 originPosition = rangeObject.transform.position;
                // 콜라이더의 사이즈를 가져오는 bound.size 사용
                float range_X = rangeCollider.bounds.size.x;
                float range_Z = rangeCollider.bounds.size.z;

                range_X = Random.Range((range_X / 2) * -1, range_X / 2);
                range_Z = Random.Range((range_Z / 2) * -1, range_Z / 2);
                Vector3 RandomPostion = new Vector3(range_X, 0f, range_Z);

                Vector3 respawnPosition = originPosition + RandomPostion;



                GameObject clone = Instantiate(stageData.EnemySpawnData[i].PrefabObject, transform.position, Quaternion.identity);

                clone.transform.position = respawnPosition;

                clone.GetComponent<UnitInfo>().SetData(stageData.EnemySpawnData[i].unitDataScriptableObject);




                GameObject hpBar = ObjectPool.GetObject("hpBar");
                hpBar.transform.SetParent(clone.transform);
                hpBar.GetComponent<InGameUnitHpBar>().SetData(stageData.EnemySpawnData[i].unitDataScriptableObject.maxHp , stageData.EnemySpawnData[i].unitDataScriptableObject.hp);
                hpBar.transform.localPosition = Vector3.up *2;



            }
        }

    }


    public void SetEnemyDestination()
    { 
        
    }
    public void NextStage()
    {

    }
}
