﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "TowerData.asset", menuName = "Tower/TowerData")]
public class TowerScriptable : ScriptableObject
{

    public int maxHp;
    public int hp;
    public int df;
    public int damage;
    public float attackSpeed;
    public float attackRange;
    public string attackName;
    public int multiAttack;
    public TowerScriptable nextData;
    public int upgradeCost;  //업그레이드 가격
    public int level;     //유닛 레벨


    [TextArea]
    public string explain; // 유닛 설명
    public Sprite sprite;




}