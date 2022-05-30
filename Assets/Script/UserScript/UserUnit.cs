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
        unitInfo.hp -= (damage - unitInfo.df) >= 0 ? (damage - unitInfo.df) : 1;
        Debug.Log(unitInfo.hp);
    }
}
