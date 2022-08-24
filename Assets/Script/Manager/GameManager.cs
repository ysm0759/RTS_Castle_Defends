using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameSeason
{
    SPRING,
    SUMMER,
    FALL,
    WINTER,
    BOSS,
    CNT,
}


public class GameManager : MonoBehaviour
{

    [SerializeField] private GameObject mouseDrag;
    [SerializeField] private GameObject skillUI;

    [Header("Scenes")]
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private ReadySceneUI readySceneUI;
    [SerializeField] private GameObject readyPanel;
    [SerializeField] private GameObject storePanel;
    
    
    [Header("Cost")]
    [SerializeField] private Text costPanel;
    [SerializeField] private int maxCost;
    [SerializeField] private int curCost;


    [Header("Camera")]
    [SerializeField] GameObject mainCamera;
    [SerializeField] GameObject readyCamera;




    static private GameManager Instance;

    static public GameManager instance
    {
        get
        {
            return Instance;
        }
    }


    private void Start()
    {
        Instance = this;
        mouseDrag.SetActive(false);
        skillUI.SetActive(false);
        mainPanel.SetActive(true);
        readyPanel.SetActive(false);
        storePanel.SetActive(false);
        mainCamera.SetActive(false);
        readyCamera.SetActive(true);
        TowerManager.instance.DisplayNode(false);

    }


    public void MainScene()
    {
        mouseDrag.SetActive(false);
        skillUI.SetActive(false);
        mainPanel.SetActive(true);
        readyPanel.SetActive(false);
        storePanel.SetActive(false);
        mainCamera.SetActive(false);
        readyCamera.SetActive(true);
    }

    public void ReadyScene()
    {
        mouseDrag.SetActive(false);
        skillUI.SetActive(false);
        mainPanel.SetActive(false);
        readyPanel.SetActive(true);
        storePanel.SetActive(false);
        mainCamera.SetActive(true);
        readyCamera.SetActive(false);
    }


    public void HeroSelectScene()
    {
        mouseDrag.SetActive(false);
        skillUI.SetActive(false);
        mainPanel.SetActive(false);
        readyPanel.SetActive(true);
        storePanel.SetActive(false);

    }

    public void GameStart()
    {
        mouseDrag.SetActive(true);
        skillUI.SetActive(true);
        mainPanel.SetActive(false);
        readyPanel.SetActive(false);
        storePanel.SetActive(false);
        RTSUserUnitControlManager.instance.InitUnit();
        EnemySpawnManager.instance.SetEnemySpawn();
        TowerManager.instance.DisplayNode(false);
    }

    public void StoreScene()
    {
        mouseDrag.SetActive(false);
        skillUI.SetActive(false);
        mainPanel.SetActive(false);
        readyPanel.SetActive(false);
        storePanel.SetActive(true);
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
