using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "TowerData.asset", menuName = "Tower/TowerData")]
public class TowerScriptable : ScriptableObject
{
    [SerializeField] float _attackSpeed;
    [SerializeField] float _attackRange;
    [SerializeField] int _multiAttack;

    [SerializeField] Sprite image;
    [SerializeField] TowerScriptable nextData;

   

    [SerializeField] int _maxHp;

    public int maxHp
    {
        get
        {
            return _maxHp;
        }
        private set
        {
            _maxHp = value;
        }
          
    }

    [SerializeField] int _hp;

    public int hp
    {
        get
        {
            return _hp;
        }
        set
        {
            _hp = value;
        }
    }



    [SerializeField] int _df;

    public int df
    {
        get
        {
            return _df;
        }
        set
        {
            _df = value;
        }
    }


    [SerializeField] int _damage;

    public int damage
    {
        get
        {
            return damage;
        }
        set
        {
            damage = value;
        }
    }


    public float attackSpeed
    {
        get
        {
            return _attackSpeed;
        }
        set
        {
            _attackSpeed = value;
        }
    }
    

    public int multiAttack
    {
        get
        {
            return _multiAttack;
        }
        set
        {
            _multiAttack = value;
        }
    }
    
    public float attackRange
    {
        get
        {
            return _attackRange;
        }
        set
        {
            _attackRange = value
        }
    }


}
