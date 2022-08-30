using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TowerCastle : MonoBehaviour , IDamagable
{
    [SerializeField] TowerScriptable castleData;
    [SerializeField] GameObject hpBarPrefab;
    private TowerInfo towerInfo;
    private GameObject burning;
    private GameObject destroy;

    bool isBurn;
    bool isDestroy;
    float burnHp;

    private void Awake()
    {
        towerInfo = GetComponent<TowerInfo>();
        towerInfo.SetData(castleData);
        inGameUnitHP = hpBarPrefab.GetComponent<InGameUnitHpBar>();
        InitTower();
    }

    public void InitTower()
    {
        this.gameObject.SetActive(true);
        this.towerInfo.hp = towerInfo.maxHp;
        inGameUnitHP.SetData(towerInfo.maxHp, towerInfo.hp);
        burnHp = towerInfo.maxHp * 0.5f;
        isBurn = false;
        isDestroy = false;
        inGameUnitHP.SetData(towerInfo.maxHp, towerInfo.hp);
    }



    public void Hit(float damage) 
    {
        towerInfo.hp -= (damage - towerInfo.df) > 1 ? (damage - towerInfo.df) : 1;

        inGameUnitHP?.UpdateHpBar(towerInfo.hp);

        if (towerInfo.hp <= burnHp && isBurn == false)
        {
            isBurn = true;
            burning = ObjectPool.GetObject("burning");
            burning.transform.position = transform.position;
            burning.SetActive(true);
        }
        else if (towerInfo.hp <= 0 && isDestroy == false)
        {
            this.gameObject.SetActive(false);
            ObjectPool.ReturnObject("burning", burning);
            destroy = ObjectPool.GetObject("destroy");
            destroy.transform.position = transform.position;
            destroy.SetActive(true);
            GameManager.instance.StageLose();
            Invoke("returnDestroyObject", 3f);
            InitTower();
        }

    }

    private void returnDestroyObject()
    {
        ObjectPool.ReturnObject("destroy", destroy);
    }




    public InGameUnitHP _inGameUnitHP;
    public InGameUnitHP inGameUnitHP
    {
        get
        {
            return _inGameUnitHP;
        }
        set
        {
            _inGameUnitHP = value;
        }
    }

}
