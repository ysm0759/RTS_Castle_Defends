using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum GameState
{
    MAIN,
    TUTORAIL,
    READY,
    HERO_SELECT,
    GAME_START,
    STORE,
    EXIT,
    TOWER_SETTING,
    CNT,
}




public class GameManager : MonoBehaviour
{

    [SerializeField] private GameObject mouseDrag;
    [SerializeField] private GameObject skillUI;

    [Header("Scenes")]
    [SerializeField] private MainSceneUI mainPanel;
    [SerializeField] private ReadySceneUI readyPanel;
    [SerializeField] private StoreSceneUI storePanel;
    
    
    [Header("Cost")]
    [SerializeField] private Text costPanel;
    [SerializeField] private int maxCost;
    [SerializeField] private int curCost;
    [SerializeField] private Canvas noMoneyPanel;
    

    [Header("Camera")]
    [SerializeField] GameObject mainCamera;
    [SerializeField] GameObject readyCamera;



    public GameState gameState;
    static private GameManager Instance;

    static public GameManager instance
    {
        get
        {
            return Instance;
        }
    }
    private void Awake()
    {
        Instance = this;

    }

    private void Start()
    {
       
        mouseDrag.SetActive(false);
        skillUI.SetActive(false);
        mainPanel.gameObject.SetActive(true);
        readyPanel.gameObject.SetActive(false);
        storePanel.gameObject.SetActive(false);
        mainCamera.SetActive(false);
        readyCamera.SetActive(true);
        TowerManager.instance.DisplayNode(false);
        gameState = GameState.MAIN;


    }

    public void StageClear()
    {
        ReadyScene();
        readyPanel.UpdateEnemyPanel();
        RTSUserUnitControlManager.instance.StageFinish();
        TowerManager.instance.InitTowers();


    }
    public void StageLose()
    {
        ReadyScene();
        EnemySpawnManager.instance.RemoveAllEnemy();
        RTSUserUnitControlManager.instance.StageFinish();
        TowerManager.instance.InitTowers();
    }

    public void MainScene()
    {
        mouseDrag.SetActive(false);
        skillUI.SetActive(false);
        mainPanel.gameObject.SetActive(true);
        readyPanel.gameObject.SetActive(false);
        storePanel.gameObject.SetActive(false);
        mainCamera.SetActive(false);
        readyCamera.SetActive(true);
        gameState = GameState.MAIN;
        TowerManager.instance.OffNode();
    }
    
    public void ReadyScene()
    {
        mouseDrag.SetActive(false);
        skillUI.SetActive(false);
        mainPanel.gameObject.SetActive(false);
        readyPanel.gameObject.SetActive(true);
        storePanel.gameObject.SetActive(false);
        mainCamera.SetActive(true);
        readyCamera.SetActive(false);
        gameState = GameState.READY;
        TowerManager.instance.OnNode();
    }


    public void HeroSelectScene()
    {
        mouseDrag.SetActive(false);
        skillUI.SetActive(false);
        mainPanel.gameObject.SetActive(false);
        readyPanel.gameObject.SetActive(true);
        storePanel.gameObject.SetActive(false);
        gameState = GameState.HERO_SELECT;


    }

    public void GameStart()
    {
        mouseDrag.SetActive(true);
        skillUI.SetActive(true);
        mainPanel.gameObject.SetActive(false);
        readyPanel.gameObject.SetActive(false);
        storePanel.gameObject.SetActive(false);
        gameState = GameState.GAME_START;

        RTSUserUnitControlManager.instance.InitUnit();
        EnemySpawnManager.instance.SetEnemySpawn();
        TowerManager.instance.DisplayNode(false);
    }

    public void StoreScene()
    {
        mouseDrag.SetActive(false);
        skillUI.SetActive(false);
        mainPanel.gameObject.SetActive(false);
        readyPanel.gameObject.SetActive(false);
        storePanel.gameObject.SetActive(true);
        gameState = GameState.STORE;

        UpdateCostPanel();
        TowerManager.instance.DisplayNode(true);

    }


    public void UpdateCostPanel()
    {
        costPanel.text =  $"{curCost} / {maxCost}";
    }


    public bool UseCost(int cost)
    {
        if(curCost - cost < 0)
        {
            noMoneyPanel.gameObject.SetActive(true);
            return false; //돈 없음
        }
        else
        {
            curCost -= cost;
            UpdateCostPanel(); // 돈있음
            return true; 
        
        }
    }
    
    public void IncreasedCost(int cost)
    {
        curCost += cost;
        maxCost += cost;
        UpdateCostPanel();

    }


    public void ReturnCost(int cost)
    {
        curCost += cost;
        UpdateCostPanel();
    }
    public void ResetCost()
    {
        curCost = maxCost;
        UpdateCostPanel();
    }




}
