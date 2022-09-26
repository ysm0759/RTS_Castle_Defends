using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum StoreMenu
{
    UNIT,
    TOWER,
}


public class UnitStoreUI : MonoBehaviour
{
    /// <summary>
    /// TODO :: unit Store UI FIX !!
    /// </summary>
    private UnitDataScriptableObject[] data = new UnitDataScriptableObject[(int)UnitType.USER_UNIT_COUNT];
    private bool[] isBuy = new bool[(int)UnitType.USER_UNIT_COUNT];


    [SerializeField] private UnitDataScriptableObject[] defaultUnit;

    [SerializeField] private GameObject[] lockImage = new GameObject[(int)UnitType.USER_UNIT_COUNT];


    [SerializeField] private Text level;

    [SerializeField] private Text hp;
    [SerializeField] private Text hpLevel;

    [SerializeField] private Text df;
    [SerializeField] private Text dfLevel;

    [SerializeField] private Text damage;
    [SerializeField] private Text damageLevel;

    [SerializeField] private Text attackSpeed;
    [SerializeField] private Text attackSpeedLevel;

    [SerializeField] private Text moveSpeed;
    [SerializeField] private Text moveSpeedLevel;

    [SerializeField] private Text attackRange;
    [SerializeField] private Text attackRangeLevel;

    [SerializeField] private Text population;
    [SerializeField] private Text populationLevel;

    [SerializeField] private Text explain;

    [SerializeField] private Text upGradeCost;
    [SerializeField] private Image image;



    private int unitType = 0;


    private void Awake()
    {

        OnClickUnit(defaultUnit[0]);


    }

    //필요한게 코스트
    //잠구고 열고 
    public void OnClickUnit(UnitDataScriptableObject firstData)
    {
        unitType = (int)firstData.unitType;
        if (data[unitType] == null)
        {
            data[unitType] = firstData;
        }

        image.sprite = data[unitType].sprite;
        level.text = data[unitType].level.ToString();
        hp.text = data[unitType].hp.ToString();
        df.text = data[unitType].df.ToString();
        damage.text = data[unitType].damage.ToString();
        attackSpeed.text = data[unitType].attackSpeed.ToString();
        moveSpeed.text = data[unitType].moveSpeed.ToString();
        attackRange.text = data[unitType].attackRange.ToString();
        population.text = data[unitType].population.ToString();
        explain.text = data[unitType].explain.ToString();

        if (null == data[unitType].nextStat)
        {
            hpLevel.text = "";
            dfLevel.text = "";
            damageLevel.text = "";
            attackSpeedLevel.text = "";
            moveSpeedLevel.text = "";
            attackRangeLevel.text = "";
            populationLevel.text = "";
            upGradeCost.text = "MAX";
            return;
        }
        hpLevel.text = string.Format("{0:0.#}", (data[unitType].nextStat.hp - data[unitType].hp));
        dfLevel.text = string.Format("{0:0.#}", (data[unitType].nextStat.df - data[unitType].df));
        damageLevel.text = string.Format("{0:0.#}", (data[unitType].nextStat.damage - data[unitType].damage));
        attackSpeedLevel.text = string.Format("{0:0.#}", (data[unitType].nextStat.attackSpeed - data[unitType].attackSpeed));
        moveSpeedLevel.text = string.Format("{0:0.#}", (data[unitType].nextStat.moveSpeed - data[unitType].moveSpeed));
        attackRangeLevel.text = string.Format("{0:0.#}", (data[unitType].nextStat.attackRange - data[unitType].attackRange));
        populationLevel.text = string.Format("{0:0.#}", (data[unitType].nextStat.population - data[unitType].population));
        upGradeCost.text = string.Format("{0:0.#}", data[unitType].upgradeCost);

    }



    public void OnClickUpGrade()
    {
        if (null == data[unitType] || null == data[unitType].nextStat || lockImage[unitType].gameObject.activeSelf == true)
            return;

        if (GameManager.instance.UseCost(data[unitType].upgradeCost))
        {
            data[unitType] = data[unitType].nextStat;
            OnClickUnit(data[unitType]);
            GameManager.instance.UpdateCostPanel();
        }

    }




    public void OnClickBuy()
    {
        if (lockImage[unitType].gameObject.activeSelf == true)
        {
            if (GameManager.instance.UseCost(10))
            {
                isBuy[unitType] = true;
                lockImage[unitType].SetActive(false);
            }
        }
    }


    public void OnClickSell()
    {
        if (lockImage[unitType].gameObject.activeSelf == false)
        {
            GameManager.instance.ReturnCost(10);
            UnitDataScriptableObject tmp = defaultUnit[unitType];
            while (true)
            {
                if(tmp == data[unitType])
                {
                    data[unitType] = null;
                    lockImage[unitType].SetActive(true);
                    isBuy[unitType] = false;
                    OnClickUnit(defaultUnit[unitType]);
                    return;
                }

                GameManager.instance.ReturnCost(tmp.upgradeCost);
                tmp = tmp.nextStat;
                Debug.Log("??");
            }
        }
    }


    public UnitDataScriptableObject[] GetScriptableData()
    {
        return data;
    }

    public bool[] GetIsBuys()
    {
        return isBuy;
    }


    public void OnClickReset()
    {
        for (int i = 0; i < (int)UnitType.USER_UNIT_COUNT; i++)
        {
            data[i] = null;
            lockImage[i].SetActive(true);
            isBuy[i] = false;
        }
        OnClickUnit(defaultUnit[0]);
    }


}
