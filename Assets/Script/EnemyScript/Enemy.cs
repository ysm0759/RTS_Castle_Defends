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

    public InGameUnitHeroHpBar _inGameUnitHpBar;
    public InGameUnitHeroHpBar inGameUnitHpBar { 
        get
        {
            return _inGameUnitHpBar;
        }
        set
        {
            _inGameUnitHpBar = value;
        }
    }

    public void Hit(float damage)
    {
        if (unitInfo.hp <= 0)
        {
            OnDead();
            inGameUnitHpBar.ReturnObject();
            Destroy(gameObject);

        }
        unitInfo.hp -= (damage - unitInfo.df) >= 0 ? (damage - unitInfo.df) : 1;
        Debug.Log(inGameUnitHpBar);
        inGameUnitHpBar?.UpdateHpBar(unitInfo.hp);
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

