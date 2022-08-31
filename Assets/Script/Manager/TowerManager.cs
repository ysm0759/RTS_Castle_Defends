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
    [SerializeField] TowersList[] installTowerData; 
    [Space(20)]
    [SerializeField] GameObject explain;
    [SerializeField] GameObject onOffObject;
    TowerNode[] towerNodes; //타워 기본 노드

    private Camera mainCamera;
    static public TowerManager instance;
    TowerStatu towerStatu = TowerStatu.NONE;


    [Serializable]
    public class TowersList
    {
        public List<Tower> Towers;
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



    private void Awake()
    {
        mainCamera = Camera.main;
        instance = this;


        Transform[] tmp = GetComponentsInChildren<Transform>();
        towerNodes = new TowerNode[tmp.Length - 1];


        for (int i = 1; i < tmp.Length; i++)
        {
            towerNodes[i - 1] = new TowerNode();
            towerNodes[i - 1].node = tmp[i].gameObject;
        }


    }




    public void InitTowers()
    {

        for (int i = 0; i < installTowerData.Length; i++)
        {
            foreach (Tower tmp in installTowerData[i].Towers)
            {
                tmp.InitTower();
            }
        }
    }


    public void UpgradeTower(TowerScriptable towerData)
    {
        foreach (Tower tmp in installTowerData[(int)towerData.towerIndex].Towers)
        {
            tmp.SetTowerInfo(towerData);
        }
    }


    public void StartGame()
    {
        if (GameManager.instance.gameState == GameState.GAME_START ||
           GameManager.instance.gameState == GameState.MAIN)
        {

        }
        else
        {

        }
    }

    public void DisplayNode(bool isVisible)
    {
        for (int i = 0; i < towerNodes.Length; i++)
        {
            towerNodes[i].node.SetActive(!(towerNodes[i].isEmpty && !isVisible));
        }
    }


    public void ResetTowers()
    {
        for(int i =0; i < installTowerData.Length; i++)
        {
            towerNodes[i].SetClear();
            towerNodes[i].node.GetComponent<MeshRenderer>().enabled = true;
        }

        for(int i =0; i < installTowerData.Length;i++)
        {
            installTowerData[i].Towers.Clear();
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
        GameManager.instance.gameState = GameState.TOWER_SETTING;
    }


    IEnumerator TowerSetting(TowerScriptable data)
    {
        towerData = data;
        explain.gameObject.SetActive(true);
        onOffObject.SetActive(false);
        GameManager.instance.gameState = GameState.TOWER_SETTING;
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                onOffObject.gameObject.SetActive(true);
                explain.gameObject.SetActive(false);
                GameManager.instance.gameState = GameState.STORE;

                yield break;
            }

            if (towerStatu == TowerStatu.BUY)
            {
                RaycastHit hit;
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);


                //if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("TowerArea"))) // 타워 배치 가생성 코드 
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
                            if (GameManager.instance.UseCost(towerData.buyCost) == false)
                            {
                                yield return null;

                            }
                            else
                            {
                                towerNodes[idx].isEmpty = false;
                                hit.collider.GetComponent<MeshRenderer>().enabled = false;
                                GameObject tower = Instantiate(towerData.prefab);
                                tower.transform.SetParent(hit.transform);
                                tower.transform.position = hit.transform.position;
                                Tower tmp = tower.GetComponent<Tower>();
                                tmp.SetTowerInfo(towerData);
                                installTowerData[(int)towerData.towerIndex].Towers.Add(tmp);
                                tower.transform.localScale *= 5;



                                GameObject hpBar = ObjectPool.GetObject("hpBar");
                                hpBar.transform.SetParent(tower.transform);
                                hpBar.GetComponent<InGameUnitHpBar>().SetData(towerData.maxHp, towerData.hp);


                                //TODO :: 더 좋은 방법 있으면 바꾸기
                                if (towerData.towerIndex == TowerIndex.BLOCK)
                                {
                                    hpBar.transform.localPosition = Vector3.up * 5;

                                }
                                else if (towerData.towerIndex == TowerIndex.ARCHER)
                                {
                                    hpBar.transform.localPosition = Vector3.up * 8.7f;

                                }
                                if (towerData.towerIndex == TowerIndex.CATAPULT)
                                {
                                    hpBar.transform.localPosition = Vector3.up * 3;

                                }




                            }

                        }
                    }
                }
            }
            // 5 , 8.7 , 3



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
                        Tower towerTmp = towerNodes[idx].node.GetComponentInChildren<Tower>();
                        Destroy(towerTmp.gameObject);

                        installTowerData[(int)towerTmp.GetTowerInfo().towerIndex].Towers.Remove(towerTmp);
                        GameManager.instance.ReturnCost(towerData.buyCost);

                    }
                }
            }


            yield return null;
        }

    }

}
