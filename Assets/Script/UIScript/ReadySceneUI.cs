using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadySceneUI : MonoBehaviour
{
    [SerializeField]
    public GameObject[] season;
    

    public void Season()
    {
        for(int i =0; i < (int)GameSeason.CNT;i++)
        {
            if(i < (int)GameManager.instance.gameSeason)
               season[i].SetActive(true);
            else
                season[i].SetActive(false);
        }

        season[(int)GameSeason.BOSS].SetActive(true);
    }


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

    public void OnClickedBack()
    {
        GameStateManager.instance.gameState = GameState.MAIN;
        GameManager.instance.MainScene();
    }

    public void UpdateEnemyPanel()
    {
        //for(int i =0; i < EnemySpawnManager.instance.stageData.EnemySpawnData.Length;i++)
        //{
        //    EnemySpawnManager.instance.stageData.EnemySpawnData[i].unitDataScriptableObject.sprite;

        //}
    }
}
