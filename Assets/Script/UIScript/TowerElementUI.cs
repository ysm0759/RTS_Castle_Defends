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
    


}
