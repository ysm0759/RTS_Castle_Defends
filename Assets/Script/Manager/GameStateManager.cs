using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public enum GameState
{
    MAIN,
    TUTORAIL,
    READY,
    HERO_SELECT,
    GAME_START,
    EXIT,
    CNT,
}




public class GameStateManager : MonoBehaviour
{

    public GameState gameState;

    static private GameStateManager Instance;
    static public GameStateManager instance
    {
        get
        {
            return Instance;
        }
    }


    private void Awake()
    {
        Instance = this;
        gameState = GameState.MAIN;
    }

    
}
