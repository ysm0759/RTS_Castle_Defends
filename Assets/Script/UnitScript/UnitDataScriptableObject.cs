using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum UnitType
{
    USER_MELEE,
    USER_RANGER,
    USER_MAGICIAN,
    USER_UNIT_COUNT,

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


    public UnitType unitType;
    public float maxHp; //최대체력
    public float hp;  //체력
    public float df; //방어력
    public float moveSpeed; //이동속도
    public float attackSpeed;  //공격속도 
    public float damage;  //데미지
    public float attackRange;  //공격 거리
    public int upgradeCost;  //업그레이드 가격
    public int population;  //생성되는 유닛 인구수
    public int level;     //유닛 레벨
    public string attackName; // 오브젝트 풀링 할 네임
    public int multiAttack;

    public UnitDataScriptableObject nextStat;

    public static float traceRange = 15; // 추적 거리

    [TextArea]
    public string explain; // 유닛 설명
    public Sprite sprite; //유닛 초상화
    public Sprite buttonSprite; //인게임 유닛 클릭 버튼
}

