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
        unitInfo.hp -= (unitInfo.df - damage) >= 0 ? 1 : Mathf.Abs(unitInfo.df - damage);
    }
}
