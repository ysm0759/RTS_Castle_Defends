using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerElementUI : MonoBehaviour
{

    [SerializeField] TowerScriptable towerData;
    [SerializeField] TowerStoreUI towerStoreUI;

    public void OnClickedTower()
    {
        towerStoreUI.SetTowerData(towerData);
    }

    public void SetData(TowerScriptable towerData)
    {
        this.towerData = towerData;
        towerStoreUI.SetTowerData(this.towerData);
    }

}
