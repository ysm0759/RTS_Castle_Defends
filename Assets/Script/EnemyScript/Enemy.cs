using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour, IDamagable
{

    [SerializeField]
    private UnitInfo _unitInfo;

    public UnityAction<Collider> onDead;

    private Collider col;
    private void Awake()
    {
        col = GetComponent<Collider>();
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
        if (unitInfo.hp <= 0)
        {
            OnDead();
            inGameUnitHP.ReturnObject();
            Destroy(gameObject);

        }
        unitInfo.hp -= (damage - unitInfo.df) >= 0 ? (damage - unitInfo.df) : 1;
        Debug.Log(inGameUnitHP);
        inGameUnitHP?.UpdateHpBar(unitInfo.hp);
    }


    public void OnDead()
    {
        Debug.Log(onDead);
        if(onDead !=null)
        {
            onDead.Invoke(col);
        }
    }



}

