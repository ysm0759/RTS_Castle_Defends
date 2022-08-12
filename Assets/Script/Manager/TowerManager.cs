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
    [Space(20)]
    [SerializeField] TowerScriptable towerData;
    [Space(20)]
    [SerializeField] TowersList[] towersList;
    [Space(20)]
    [SerializeField] GameObject explain;
    [SerializeField] GameObject onOffObject;

    GameObject towerGameObject;
    Dictionary<string, GameObject> towerDic;


    private Camera mainCamera;
    static public TowerManager instance;
    TowerStatu towerStatu = TowerStatu.NONE;


    

    [Serializable]
    public class TowersList
    {
        public List<GameObject> Towers;
    }

    public class TowerNodes
    {
        public GameObject tower;
        public bool isEmpty;

        public void SetClear()
        {
            Destroy(tower);
            isEmpty = true;
        }
    }

    private void Awake()
    {
        mainCamera = Camera.main;
        instance = this;
        towerDic = new Dictionary<string, GameObject>();
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.O))
        {
            ResetTowers();
        }
    }


    private void InitTowers()
    {
        foreach (string tmp in towerDic.Keys)
        {
            towerDic[tmp].GetComponentInChildren<Tower>().InitTower();
        }
    }

    public void UpgradeTower(TowerScriptable towerData)
    {
        foreach (GameObject tmp in towersList[(int)towerData.towerIndex].Towers)
        {
            Tower towerTmp = tmp.GetComponent<Tower>();
            towerTmp.SetTowerInfo(towerData);
        }
    }




    private void ResetTowers()
    {
        foreach (string tmp in towerDic.Keys)
        {
            Debug.Log(tmp);
            Debug.Log(towerDic[tmp].gameObject.GetComponent<MeshRenderer>());
            towerDic[tmp].gameObject.GetComponent<MeshRenderer>().enabled = true;
            Destroy(towerDic[tmp].GetComponentInChildren<Tower>().gameObject);

        }

        for (int i = 0; i < towersList.Length; i++)
        {
            towersList[i].Towers.Clear();
        }

        towerDic.Clear();
    }


    public void SellTower(TowerScriptable data)
    {
        towerStatu = TowerStatu.SELL;
        StartCoroutine(TowerSetting(data));
    }

    public void BuyTower(TowerScriptable data)
    {
        towerStatu = TowerStatu.BUY;
        StartCoroutine(TowerSetting(data));
    }


    IEnumerator TowerSetting(TowerScriptable data)
    {
        towerData = data;
        explain.gameObject.SetActive(true);
        onOffObject.SetActive(false);
        //TODO 타워 만들고 세팅하는것 수정

        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                onOffObject.gameObject.SetActive(true);
                explain.gameObject.SetActive(false);
                yield break;
            }

            if (towerStatu == TowerStatu.BUY)
            {

                //if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("TowerArea")))
                //{

                //    GameObject tower = Instantiate(towerData.prefab);
                //    tower.transform.SetParent(hit.transform);
                //    tower.transform.position = hit.transform.position;
                //    tower.transform.localScale *= 5;
                //    towerGameObject = tower;

                //}
                //else
                //{

                //    if (towerGameObject != null && !towerGameObject.GetComponent<Tower>().isSetTower)
                //    {
                //        Destroy(towerGameObject);
                //    }

                //}
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
                            tower.transform.localScale *= 5;
                            GameManager.instance.UseCost(towerData.buyCost);
                        }
                    }
                }
            }




            if (Input.GetMouseButtonDown(0) && towerStatu == TowerStatu.SELL)
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
                        GameManager.instance.ReturnCost(towerData.buyCost);

                    }
                }
            }


            yield return null;
        }

    }

}
