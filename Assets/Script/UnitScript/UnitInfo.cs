using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitInfo : MonoBehaviour
{

    public static float traceRange = 10; // 추적 거리


    bool _isAlive = true;

    public bool isAlive
    {
        get
        {
            return _isAlive;
        }
        set
        {
            _isAlive = value;
        }

    }


    public UnitDataScriptableObject _data;
    [SerializeField]
    public UnitDataScriptableObject data
    {
        get
        {
            return _data;
        }
        private set
        {
            _data = value;
        }
    }

    public UnitType _type;
    [SerializeField]
    public UnitType type
    {
        get
        {
            return _type;
        }
        private set
        {
            _type = value;
        }
    }

    [SerializeField]
    private float _maxHp; //최대체력
    public float maxHp//최대체력
    {
        get
        {
            return _maxHp;
        }
        set
        {
            _maxHp = value;
        }
    }



    [SerializeField]
    private float _hp; //체력
    public float hp//체력
    {
        get
        {
            return _hp;
        }
        set
        {
            _hp = value;
            if (_hp <= 0)
                isAlive = false;
        }
    }
    
    

    [SerializeField]
    private float _df; //방어력
    public float df//방어력
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
    [SerializeField]
    private float _moveSpeed; //이동속도
    public float moveSpeed//이동속도
    {
        get
        {
            return _moveSpeed;
        }
        set
        {
            _moveSpeed = value;
        }
    }
    [SerializeField]
    private float _attackSpeed; //공격속도 
    public float attackSpeed//이동속도
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
    [SerializeField]
    private float _damage; //데미지
    public float damage//데미지
    {
        get
        {
            return _damage;
        }
        set
        {
            _damage = value;
        }
    }
    [SerializeField]
    private float _attackRange; //공격 거리
    public float attackRange//공격 거리
    {
        get
        {
            return _attackRange;
        }
        set
        {
            _attackRange = value;
        }
    }
    [SerializeField]
    private float _upgradeCost; //업그레이드 가격
    public float upgradeCost//업그레이드 가격
    {
        get
        {
            return _upgradeCost;
        }
        set
        {
            _upgradeCost = value;
        }
    }
    [SerializeField]
    private string _attackName; //업그레이드 가격
    public string attackName//업그레이드 가격
    {
        get
        {
            return _attackName;
        }
        set
        {
            _attackName = value;
        }
    }

    [SerializeField]
    private int _multiAttack;

    public int multiAttack//멀티공격
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




    //private void Awake()
    //{

    //    this._maxHp         = data.maxHp;
    //    this._hp            = data.hp;
    //    this._df            = data.df;
    //    this._moveSpeed     = data.moveSpeed;
    //    this._attackSpeed   = data.attackSpeed;
    //    this._damage        = data.damage;
    //    this._attackRange   = data.attackRange;
    //    this._upgradeCost   = data.upgradeCost;
    //    this._attackName    = data.attackName;
    //    this._multiAttack   = data.multiAttack;
    //}

    public void SetData(UnitDataScriptableObject data)
    {
        this._type = data.unitType;
        this._maxHp = data.maxHp;
        this._hp = data.hp;
        this._df = data.df;
        this._moveSpeed = data.moveSpeed;
        this._attackSpeed = data.attackSpeed;
        this._damage = data.damage;
        this._attackRange = data.attackRange;
        this._upgradeCost = data.upgradeCost;
        this._attackName = data.attackName;
        this._multiAttack = data.multiAttack;

    }



}
