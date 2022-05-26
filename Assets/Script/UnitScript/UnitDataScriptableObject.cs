using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum UnitType
{
    USER_MELEE,
    USER_RANGER,
    USER_MAGICIAN,
    USER_TANKER,
    USER_AIR,
    ENEMY_MELEE,
    ENEMY_RANGER,
    ENEMY_MAGICIAN,
    ENEMY_TANKER,
    ENEMY_AIR,
    HERO_WARRIOR
}

[CreateAssetMenu(fileName = "UnitData.asset", menuName = "Unit/UnitData")]
public class UnitDataScriptableObject : ScriptableObject
{


    public UnitType enumType;
    public float maxHp; //최대체력
    public float hp;  //체력
    public float df; //방어력
    public float moveSpeed; //이동속도
    public float attackSpeed;  //공격속도 
    public float damage;  //데미지
    public float attackRange;  //공격 거리
    public float upgradeCost;  //업그레이드 가격
    public float level;     //유닛 레벨
    public static float traceRange = 10; // 추적 거리

}

