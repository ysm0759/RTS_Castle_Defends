using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

enum TowerStatu
{
    BUY,
    SELL,
    NONE,
    CNT,
}

public enum TowerIndex
{
    BLOCK,
    ARCHER,
    CATAPULT,

    CNT,
}

public class TowerManager : MonoBehaviour
{

    [SerializeField] TowerScriptable towerData;
    Dictionary<string, GameObject> towerDic;

    [SerializeField] TowersList[] towersList;

    private Camera mainCamera;
    static public TowerManager instance;

    TowerStatu towerStatu = TowerStatu.NONE;


    [Serializable]
    public class TowersList
    {
        public List<GameObject> Towers;
    }


    private void Awake()
    {
        mainCamera = Camera.main;
        instance = this;
        towerDic = new Dictionary<string, GameObject>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            towerStatu = (TowerStatu)((int)(++towerStatu) % (int)TowerStatu.CNT);
        }
        if(Input.GetKeyDown(KeyCode.O))
        {
            ResetTowers();
        }
        SellTower();
        BuyTower();
    }


    private void InitTowers()
    {
        foreach(string tmp in towerDic.Keys)
        {
            towerDic[tmp].GetComponentInChildren<Tower>().InitTower();
        }
    }

    public void UpgradeTower(TowerScriptable towerData)
    {
        foreach(GameObject tmp in towersList[(int)towerData.towerIndex].Towers)
        {
            Tower towerTmp = tmp.GetComponent<Tower>();
            towerTmp.SetTowerInfo(towerData);
            towerTmp.InitTower();
        }

    }


    private void SellTower()
    {
        if(towerStatu != TowerStatu.SELL)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("TowerArea")))
            {
                GameObject tmp;
                if (towerDic.TryGetValue(hit.collider.gameObject.name, out tmp))
                {
                    hit.collider.GetComponent<MeshRenderer>().enabled = true;
                    Tower towerTmp = tmp.GetComponentInChildren<Tower>();
                    GameManager.instance.ReturnCost(towerTmp.GetTowerInfo().buyCost);
                    towersList[(int)towerData.towerIndex].Towers.Remove(towerTmp.gameObject);

                    Destroy(towerTmp.gameObject);
                    towerDic.Remove(hit.collider.gameObject.name);

                }
            }
        }
    }



    private void ResetTowers()
    {
        foreach(string tmp in towerDic.Keys)
        {
            Debug.Log(tmp);
            Debug.Log(towerDic[tmp].gameObject.GetComponent<MeshRenderer>());
            towerDic[tmp].gameObject.GetComponent<MeshRenderer>().enabled = true;
            Destroy(towerDic[tmp].GetComponentInChildren<Tower>().gameObject);

        }

        for(int i =0; i < towersList.Length;i++)
        {
            towersList[i].Towers.Clear();
        }

        towerDic.Clear();
    }


    private void BuyTower()
    {
        StartCoroutine(TowerSetting());
       
    }


    IEnumerator TowerSetting()
    {

        //TODO 타워 만들고 세팅하는것 수정
        while(true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("TowerArea")))
                {
                    if (!towerDic.ContainsKey(hit.collider.gameObject.name))
                    {
                        hit.collider.GetComponent<MeshRenderer>().enabled = false;
                        GameObject tower = Instantiate(towerData.prefab);
                        towersList[(int)towerData.towerIndex].Towers.Add(tower);
                        towerDic.Add(hit.collider.gameObject.name, hit.collider.gameObject);
                        tower.transform.SetParent(hit.transform);
                        tower.transform.position = hit.transform.position;
                        tower.GetComponent<Tower>().SetTowerInfo(towerData);
                        tower.GetComponent<Tower>().InitTower();
                        tower.transform.localScale *= 5;
                    }
                }
            }
            yield return null;
        }

    }

}
