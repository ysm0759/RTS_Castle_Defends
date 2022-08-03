﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerInfo : MonoBehaviour
{
    bool _isAlive = true;

    public bool isAlive
    {
        get
        {
            return _isAlive;
        }
        private set
        {
            _isAlive = value;
        }

    }


    [SerializeField]
    private TowerScriptable data;

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

    private void Awake()
    {

        this._maxHp = data.maxHp;
        this._hp = data.hp;
        this._df = data.df;
        this._attackSpeed = data.attackSpeed;
        this._damage = data.damage;
        this._attackRange = data.attackRange;
        this._upgradeCost = data.upgradeCost;
        this._attackName = data.attackName;

    }

    public void SetData(TowerScriptable data)
    {
        this._maxHp = data.maxHp;
        this._hp = data.hp;
        this._df = data.df;
        this._attackSpeed = data.attackSpeed;
        this._damage = data.damage;
        this._attackRange = data.attackRange;
        this._upgradeCost = data.upgradeCost;
        this._attackName = data.attackName;


    }



}