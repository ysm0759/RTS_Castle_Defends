using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UserUnit: MonoBehaviour , IDamagable
{
    [SerializeField]
    private UnitInfo _unitInfo;
    public UnityAction<GameObject> unityAction;


    public UnitInfo unitInfo
    {
        get
        {
            return _unitInfo;
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

    private void OnDead()
    {
        inGameUnitHP?.ReturnObject();
        GetComponent<Collider>().enabled= false;
        unityAction.Invoke(gameObject);
        Destroy(gameObject,2f);
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
