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
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private ReadySceneUI readySceneUI;
    [SerializeField] private GameObject readyPanel;
    [SerializeField] private GameObject storePanel;
    [SerializeField] private Text costPanel;


    [SerializeField] private int maxCost;
    [SerializeField] private int curCost;



    public GameSeason gameSeason;
    public int gameYear;



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
        gameSeason = GameSeason.SPRING;
        mouseDrag.SetActive(false);
        skillUI.SetActive(false);
        mainPanel.SetActive(true);
        readyPanel.SetActive(false);
        storePanel.SetActive(false);

    }


    public void MainScene()
    {
        mouseDrag.SetActive(false);
        skillUI.SetActive(false);
        mainPanel.SetActive(true);
        readyPanel.SetActive(false);
        storePanel.SetActive(false);

    }

    public void ReadyScene()
    {
        mouseDrag.SetActive(false);
        skillUI.SetActive(false);
        mainPanel.SetActive(false);
        readyPanel.SetActive(true);
        storePanel.SetActive(false);

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
        EnemySpwanManager.instance.SetEnemySpwan();
    }

    public void StoreScene()
    {
        mouseDrag.SetActive(false);
        skillUI.SetActive(false);
        mainPanel.SetActive(false);
        readyPanel.SetActive(false);
        storePanel.SetActive(true);
        UpdateCostPanel();
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

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            gameSeason++;

            if (gameSeason == GameSeason.CNT)
                gameSeason = GameSeason.SPRING;
            readySceneUI.Season();
        }
    }



}
