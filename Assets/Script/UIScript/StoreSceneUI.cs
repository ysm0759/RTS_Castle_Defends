using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreSceneUI : MonoBehaviour
{

    [SerializeField] UnitStoreUI unitStoreUI;
    [SerializeField] GameObject[] menuObjects;
    [SerializeField] private GameObject resetPanel;
    public void OnClickReset()
    {
        resetPanel.SetActive(true);
    }

    public void OnClickStore()
    {
        resetPanel.SetActive(false);
    }


    public void OnClickResetYes()
    {

        unitStoreUI.OnClickReset();
        GameManager.instance.ResetCost();
        TowerManager.instance.ResetTowers();
        resetPanel.SetActive(false);
    }

    public void OnClickResetNo()
    {
        resetPanel.SetActive(false);
    }

    public void SelectMenu(int menu)
    {
        this.menuObjects[(int)StoreMenu.UNIT].SetActive(menu == (int)StoreMenu.UNIT);
        this.menuObjects[(int)StoreMenu.TOWER].SetActive(menu == (int)StoreMenu.TOWER);
    }

    public void OnClickBackButton()
    {
        GameManager.instance.ReadyScene();
    }
}
