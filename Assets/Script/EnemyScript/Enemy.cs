using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour, IDamagable
{

    [SerializeField]
    private UnitInfo _unitInfo;

    public UnityAction<Collider> onDead;
    public PrefabObject prefabObject;
    private Collider col;
    private void Awake()
    {
        col = GetComponent<Collider>();
        prefabObject = GetComponent<PrefabObject>();
    }
    public UnitInfo unitInfo
    {
        get
        {
            return _unitInfo;
        }
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
    public void Hit(float damage)
    {
        unitInfo.hp -= (damage - unitInfo.df) >= 0 ? (damage - unitInfo.df) : 1;
        if (unitInfo.isAlive == false)
        {
            OnDead();
        }
        inGameUnitHP?.UpdateHpBar(unitInfo.hp);
    }


    public void OnDead()
    {
        inGameUnitHP.ReturnObject();
        GetComponent<Collider>().enabled = false;

        if (onDead !=null)
        {
            onDead.Invoke(col);
        }

        if (prefabObject == null)
        {
            Destroy(gameObject, 2f);

        }
        else
        {
            StartCoroutine(ReturnSummon());

        }
    }

    IEnumerator ReturnSummon()
    {


        yield return new WaitForSeconds(2f);
        ObjectPool.ReturnObject("bat", this.gameObject);
        unitInfo.isAlive = true;
    }

}

