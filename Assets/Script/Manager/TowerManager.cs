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
    [SerializeField] TowersList[] towersListNode;
    [Space(20)]
    [SerializeField] GameObject explain;
    [SerializeField] GameObject onOffObject;

    GameObject nodeGameObject;

    TowerNode[] towerNodes;

    private Camera mainCamera;
    static public TowerManager instance;
    TowerStatu towerStatu = TowerStatu.NONE;


    private void Awake()
    {
        mainCamera = Camera.main;
        instance = this;
        

        Transform[] tmp = GetComponentsInChildren<Transform>();
        towerNodes = new TowerNode[tmp.Length - 1];


        for (int i = 1; i < tmp.Length; i++)
        {
            towerNodes[i-1] = new TowerNode();
            towerNodes[i - 1].node = tmp[i].gameObject;
        }
    }

    [Serializable]
    public class TowersList
    {
        public List<GameObject> Towers;
    }

    public class TowerNode
    {
        public GameObject node;
        public bool isEmpty = true;

        public void SetClear()
        {
            Destroy(node.GetComponentInChildren<Tower>().gameObject);
            isEmpty = true;
        }
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

        for (int i = 0; i < towersListNode.Length; i++)
        {
            foreach (GameObject tmp in towersListNode[i].Towers)
            {
                towerNodes[Int32.Parse(tmp.name)].node.GetComponentInChildren<Tower>().InitTower();
            }
        }
    }

    public void UpgradeTower(TowerScriptable towerData)
    {
        foreach (GameObject tmp in towersListNode[(int)towerData.towerIndex].Towers)
        {
            Tower towerTmp = tmp.GetComponentInChildren<Tower>();
            towerTmp.SetTowerInfo(towerData);
        }
    }




    private void ResetTowers()
    {

        for(int i =0;i < towersListNode.Length;i++)
        {
            foreach(GameObject tmp in towersListNode[i].Towers)
            {
                int index = Int32.Parse(tmp.name);
                towerNodes[index].SetClear();
                towerNodes[index].node.GetComponent<MeshRenderer>().enabled = true;
            }
            towersListNode[i].Towers.Clear();
        }
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
                RaycastHit hit;
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);


                //if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("TowerArea")))
                //{
                //    int idx = Int32.Parse(hit.collider.name);

                //    if (towerNodes[idx].isEmpty)
                //    {
                //        nodeGameObject = hit.collider.gameObject;
                //        towerNodes[idx].isEmpty = false;
                //        GameObject tower = Instantiate(towerData.prefab);
                //        tower.transform.SetParent(hit.transform);
                //        tower.transform.position = hit.transform.position;
                //        tower.GetComponent<Tower>().SetTowerInfo(towerData);
                //        tower.transform.localScale *= 5;
                //    }
                //}
                //else
                //{
                //    if (nodeGameObject != null)
                //    {
                //        int idx = Int32.Parse(nodeGameObject.name);
                //        towerNodes[idx].isEmpty = true;
                //        Tower towerTmp = towerNodes[idx].node.GetComponentInChildren<Tower>();
                //        Destroy(towerTmp.gameObject);
                //        nodeGameObject = null;
                //    }

                //}

                if (Input.GetMouseButtonDown(0))
                {

                    if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("TowerArea")))
                    {
                        int idx = Int32.Parse(hit.collider.name);
                        if (towerNodes[idx].isEmpty)
                        {
                            towerNodes[idx].isEmpty = false;
                            hit.collider.GetComponent<MeshRenderer>().enabled = false;
                            GameObject tower = Instantiate(towerData.prefab);
                            towersListNode[(int)towerData.towerIndex].Towers.Add(hit.collider.gameObject);
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
                    int idx = Int32.Parse(hit.collider.name);
                    if (!towerNodes[idx].isEmpty)
                    {
                        towerNodes[idx].isEmpty = true;
                        hit.collider.GetComponent<MeshRenderer>().enabled = true;
                        Tower towerTmp =towerNodes[idx].node.GetComponentInChildren<Tower>();
                        towersListNode[(int)towerData.towerIndex].Towers.Remove(hit.collider.gameObject);
                        GameManager.instance.ReturnCost(towerData.buyCost);
                        Destroy(towerTmp.gameObject);

                    }
                }
            }


            yield return null;
        }

    }

}
