using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserUnit: MonoBehaviour , IDamagable
{
    [SerializeField]
    private UnitInfo _unitInfo;



    public UnitInfo unitInfo
    {
        get
        {
            return _unitInfo;
        }
    }


    public void Hit(float damage)
    {
        
        if(unitInfo.hp <= 0)
        {
            inGameUnitHP?.ReturnObject();
            Die();
        }
        unitInfo.hp -= (damage - unitInfo.df) >= 0 ? (damage - unitInfo.df) : 1;
        inGameUnitHP?.UpdateHpBar(unitInfo.hp);
    }

    private void Die()
    {

        //죽었을 때

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
