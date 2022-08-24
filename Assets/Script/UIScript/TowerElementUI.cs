using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerElementUI : MonoBehaviour
{

    [SerializeField] TowerScriptable towerData;
    [SerializeField] TowerStoreUI towerStoreUI;
    [SerializeField] Text cost;

    public void OnClickedTower()
    {
        towerStoreUI.SetTowerData(towerData);
    }

    public void SetData(TowerScriptable towerData)
    {
        this.towerData = towerData;
        cost.text = towerData.buyCost.ToString();
        towerStoreUI.SetTowerData(this.towerData);
    }

}
