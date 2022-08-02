using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{

    [SerializeField] GameObject towerPrefab;

    private Camera mainCamera;

    [SerializeField] Dictionary<string,GameObject> towerDic;


    static public TowerManager instance;


    private void Awake()
    {
        mainCamera = Camera.main;
        instance = this;
        towerDic = new Dictionary<string, GameObject>();
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if(Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("TowerArea")))
            {
                GameObject tower = Instantiate(towerPrefab);
                towerDic.Add(hit.collider.gameObject.name, tower);
                tower.transform.SetParent(hit.transform);
                tower.transform.position = hit.transform.position;
                tower.transform.localScale *= 10;
                
            }
        }

    }


    private void ResetTowers()
    {
        foreach(string tmp in towerDic.Keys)
        {
            towerDic[tmp].GetComponent<Tower>().ResetTower();
        }
    }




}
