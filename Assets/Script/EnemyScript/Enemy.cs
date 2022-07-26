using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour , IDamagable
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

    public Transform GetTransform()
    {
        return this.transform;
    }
    public void Hit(float damage)
    {
        unitInfo.hp -= (damage - unitInfo.df) >= 0 ? (damage - unitInfo.df) : 1;
    }

}

