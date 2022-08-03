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

    [SerializeField] GameObject towerPrefab;
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
        Debug.Log(towerStatu);
        SellTower();
        BuyTower();
    }


    private void SetTowersInfo()
    {
        foreach(string tmp in towerDic.Keys)
        {
            towerDic[tmp].GetComponent<Tower>().SetTowerInfo();
        }
    }

    private void SellTower()
    {
        if(towerStatu != TowerStatu.SELL)
            return;

        Debug.Log("판매");
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
                    Destroy(tmp.GetComponentInChildren<Transform>().gameObject);
                    towerDic.Remove(hit.collider.gameObject.name);

                }
            }
        }
    }

    private void ResetTowers()
    {
        foreach(string tmp in towerDic.Keys)
        {
            towerDic[tmp].gameObject.GetComponent<MeshRenderer>().enabled = true;
            Destroy(towerDic[tmp].GetComponentInChildren<Transform>().gameObject);
        }
        towerDic.Clear();
    }


    private void BuyTower()
    {
        if (towerStatu != TowerStatu.BUY)
            return;
        Debug.Log("구매");

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("TowerArea")))
            {
                GameObject tmp;
                if (!towerDic.TryGetValue(hit.collider.gameObject.name, out tmp))
                {
                    hit.collider.GetComponent<MeshRenderer>().enabled = false;
                    GameObject tower = Instantiate(towerPrefab);
                    towerDic.Add(hit.collider.gameObject.name, tower);
                    tower.transform.SetParent(hit.transform);
                    tower.transform.position = hit.transform.position;
                    tower.transform.localScale *= 10;
                }
            }
        }
    }

}
