using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField]
    private GameObject mouseDrag;
    [SerializeField]
    private GameObject skillUI;
    [SerializeField]
    private GameObject mainPanel;
    [SerializeField]
    private ReadySceneUI readySceneUI;

    [SerializeField]
    private GameObject readyPanel;


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
    }


    public void MainScene()
    {
        mouseDrag.SetActive(false);
        skillUI.SetActive(false);
        mainPanel.SetActive(true);
        readyPanel.SetActive(false);
    }

    public void ReadyScene()
    {
        mouseDrag.SetActive(false);
        skillUI.SetActive(false);
        mainPanel.SetActive(false);
        readyPanel.SetActive(true);
    }


    public void HeroSelectScene()
    {
        mouseDrag.SetActive(false);
        skillUI.SetActive(false);
        mainPanel.SetActive(false);
        readyPanel.SetActive(true);
    }

    public void GameStart()
    {
        mouseDrag.SetActive(true);
        skillUI.SetActive(true);
        mainPanel.SetActive(false);
        readyPanel.SetActive(false);
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
