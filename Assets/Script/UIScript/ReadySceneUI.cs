using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadySceneUI : MonoBehaviour
{
    [SerializeField] GameObject enemyPortraitPerfab;
    [SerializeField] GameObject enemyPanel;
    [SerializeField] ReadyEnemyInfo readyEnemyInfo;
    [SerializeField] ReadyEnemyPortrait readyEnemyPortrait;
    [SerializeField] Text stageLevel;

    public void OnEnable()
    {
        stageLevel.text = EnemySpawnManager.instance.stageData.stageLevel;
    }
    public void OnClickGameStart()
    {
        GameManager.instance.GameStart();
    }

    public void OnClickStore()
    {
        GameManager.instance.StoreScene();
    }


    public void OnClickHeroSelect()
    {
        GameManager.instance.HeroSelectScene();
    }
    public void OnClickBackButton()
    {
        GameManager.instance.MainScene();
    }
    public void Start()
    {
        UpdateEnemyPanel();
    }
    public void OnClickBack()
    {
        GameManager.instance.MainScene();
    }

    public void UpdateEnemyPanel()
    {

        ClearPanel();
        AddEnemyPortraitPerfab();

    }

    public void ClearPanel()
    {
       Transform[]tmp = enemyPanel.GetComponentsInChildren<Transform>();

        for(int i =1; i < tmp.Length;i++)
        {
            Destroy(tmp[i].gameObject);
        }
    }

    public void AddEnemyPortraitPerfab()
    {

        for (int i = 0; i < EnemySpawnManager.instance.stageData.EnemySpawnData.Length; i++)
        {
            GameObject clone = Instantiate(enemyPortraitPerfab);
            clone.transform.SetParent(enemyPanel.transform);

            readyEnemyPortrait = clone.GetComponent<ReadyEnemyPortrait>();
            readyEnemyPortrait.SetData(EnemySpawnManager.instance.stageData.EnemySpawnData[i].unitDataScriptableObject, EnemySpawnManager.instance.stageData.EnemySpawnData[i].EA, readyEnemyInfo);

        }
    }

}
