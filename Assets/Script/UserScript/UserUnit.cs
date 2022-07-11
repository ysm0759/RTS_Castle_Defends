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
        
        if(unitInfo.hp <= 0)
        {
            Die();
        }
        Debug.Log(unitInfo.hp);
    }

    public Transform GetTransform()
    {
        return this.transform;
    }

    private void Die()
    {

        //죽었을 때

    }

}
