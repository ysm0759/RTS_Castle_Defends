using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum TowerStatu
{
    BUY,
    SELL,
    NONE,
    CNT,
}

public class TowerManager : MonoBehaviour
{

    [SerializeField] TowerScriptable towerData;
    Dictionary<string, GameObject> towerDic;

    private Camera mainCamera;
    static public TowerManager instance;

    TowerStatu towerStatu = TowerStatu.NONE;



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
                    Destroy(tmp.GetComponentInChildren<Tower>().gameObject);
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
        towerDic.Clear();
    }


    private void BuyTower()
    {
        if (towerStatu != TowerStatu.BUY)
            return;

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
                    towerDic.Add(hit.collider.gameObject.name, hit.collider.gameObject);
                    tower.transform.SetParent(hit.transform);
                    tower.transform.position = hit.transform.position;
                    tower.GetComponent<Tower>().SetTowerInfo(towerData);
                    tower.GetComponent<Tower>().InitTower();
                    tower.transform.localScale *= 5;
                }
            }
        }
    }

}
