using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpwanManager : MonoBehaviour
{
    public GameObject unitPrefab;
    public Transform center;
    public int destance;
    public int offsetZ;
    public EnemyStageData stageData;


    public static EnemySpwanManager instance;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        transform.position = transform.position + Vector3.right * destance;
        transform.position = transform.position + Vector3.up * offsetZ;

    }

    private void Update()
    {

    }


    public void SetEnemySpwan()
    {
        for (int i = 0; i < stageData.EnemySpawnData.Length; i++)
        {
            for (int j = 0; j < stageData.EnemySpawnData[i].EA; j++)
            {
                GameObject clone = Instantiate(stageData.EnemySpawnData[i].PrefabObject, transform.position, Quaternion.identity);
                clone.transform.position *= Random.Range(0.1f, 2f);
                clone.GetComponent<UnitInfo>().SetData(stageData.EnemySpawnData[i].unitDataScriptableObject);
            } 
        }
        
        
    }


    public void NextStage()
    {

    }
}
