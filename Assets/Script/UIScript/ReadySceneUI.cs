using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadySceneUI : MonoBehaviour
{
    [SerializeField] GameObject enemyPortraitPerfab;
    [SerializeField] GameObject enemyPanel;
    [SerializeField] ReadyEnemyInfo readyEnemyInfo;
    [SerializeField] ReadyEnemyPortrait readyEnemyPortrait;

    public void OnClickGameStart()
    {
        GameStateManager.instance.gameState = GameState.GAME_START;
        GameManager.instance.GameStart();
    }

    public void OnClickStore()
    {
        GameStateManager.instance.gameState = GameState.STORE;
        GameManager.instance.StoreScene();
    }


    public void OnClickHeroSelect()
    {
        GameStateManager.instance.gameState = GameState.HERO_SELECT;
        GameManager.instance.HeroSelectScene();
    }

    public void Start()
    {
        UpdateEnemyPanel();
    }
    public void OnClickedBack()
    {
        GameStateManager.instance.gameState = GameState.MAIN;
        GameManager.instance.MainScene();
    }

    public void UpdateEnemyPanel()
    {
        for (int i = 0; i < EnemySpawnManager.instance.stageData.EnemySpawnData.Length; i++)
        {
            GameObject clone =Instantiate(enemyPortraitPerfab);
            clone.transform.SetParent(enemyPanel.transform);

            readyEnemyPortrait = clone.GetComponent<ReadyEnemyPortrait>();
            readyEnemyPortrait.SetData(EnemySpawnManager.instance.stageData.EnemySpawnData[i].unitDataScriptableObject, EnemySpawnManager.instance.stageData.EnemySpawnData[i].EA, readyEnemyInfo);

        }
    }
}
