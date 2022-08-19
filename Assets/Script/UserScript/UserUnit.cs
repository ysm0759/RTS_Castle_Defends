using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserUnit: MonoBehaviour , IDamagable
{
    [SerializeField]
    private UnitInfo _unitInfo;

    public InGameUnitHpBar _inGameUnitHpBar;


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
            inGameUnitHpBar?.ReturnObject();
            Die();
        }
        unitInfo.hp -= (damage - unitInfo.df) >= 0 ? (damage - unitInfo.df) : 1;
        inGameUnitHpBar?.UpdateHpBar(unitInfo.hp);
    }

    private void Die()
    {

        //죽었을 때

    }

    public InGameUnitHpBar inGameUnitHpBar
    {
        get
        {
            return _inGameUnitHpBar;
        }
        set
        {
            _inGameUnitHpBar = value;
        }
    }

}
