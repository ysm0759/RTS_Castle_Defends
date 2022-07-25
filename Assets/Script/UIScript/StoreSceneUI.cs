using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreSceneUI : MonoBehaviour
{
    Hashtable hashtable = new Hashtable();
    private UnitDataScriptableObject[] data = new UnitDataScriptableObject[(int)UnitType.USER_UNIT_COUNT];

    [SerializeField] private GameObject[] rockImage = new GameObject[(int)UnitType.USER_UNIT_COUNT];
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

    [SerializeField] private UnitDataScriptableObject defaultUnit;
    
    [SerializeField] private GameObject resetPanel;


    private int unitType = 0;


    //필요한게 코스트
    //잠구고 열고 
    public void OnClickUnit(UnitDataScriptableObject firstData)
    {
        unitType = (int)firstData.unitType;
        if (data[unitType] == null)
        {
            data[unitType] = firstData;
            SetHashtableKeyValue(data[unitType],false);
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

        if( null == data[unitType].nextStat)
        {
            hpLevel.text = "";
            dfLevel.text = "";
            damageLevel.text = "";
            attackSpeedLevel.text = "";
            moveSpeedLevel.text = "";
            attackRangeLevel.text = "";
            populationLevel.text = "";
            upGradeCost.text = "";
            return;
        }
        hpLevel.text = (data[unitType].nextStat.hp - data[unitType].hp).ToString();
        dfLevel.text = (data[unitType].nextStat.df - data[unitType].df).ToString();
        damageLevel.text = (data[unitType].nextStat.damage - data[unitType].damage).ToString();
        attackSpeedLevel.text = (data[unitType].nextStat.attackSpeed - data[unitType].attackSpeed).ToString();
        moveSpeedLevel.text = (data[unitType].nextStat.moveSpeed - data[unitType].moveSpeed).ToString();
        attackRangeLevel.text = (data[unitType].nextStat.attackRange - data[unitType].attackRange).ToString();
        populationLevel.text = (data[unitType].nextStat.population - data[unitType].population).ToString();
        upGradeCost.text = data[unitType].upgradeCost.ToString();
    }

    public void OnClickStore()
    {
        OnClickUnit(defaultUnit);
        resetPanel.SetActive(false);
    }

    public void OnClickUpGrade()
    {
        if (null == data[unitType] || null == data[unitType].nextStat || rockImage[unitType].gameObject.activeSelf == true)
            return;
        
        if(GameManager.instance.UseCost(data[unitType].upgradeCost))
        {

            data[unitType] = data[unitType].nextStat;
            OnClickUnit(data[unitType]);
            GameManager.instance.UpdateCostPanel();
        }

    }


    public void OnClickBackButton()
    {
        GameStateManager.instance.gameState = GameState.READY;
        GameManager.instance.ReadyScene();
    }

    public void OnClickResetYes()
    {
        for(int i =0; i < (int)UnitType.USER_UNIT_COUNT; i++)
        {
            data[i] = null;
        }
        OnClickUnit(defaultUnit);
        GameManager.instance.ResetCost();
        resetPanel.SetActive(false);
    }
    
    public void OnClickResetNo()
    {
        resetPanel.SetActive(false);
    }
    
    public void OnClickReset()
    {
        resetPanel.SetActive(true);
    }

    public void OnClickBuy(UnitDataScriptableObject data, int cost)
    {
        if((bool)hashtable[data] == false || GameManager.instance.UseCost(cost) && rockImage[unitType].gameObject.activeSelf == true)
        {
            Debug.Log("?? 드가나 ");
            hashtable[data] = true;
            rockImage[unitType].SetActive(false);
        }
    }


    private void SetHashtableKeyValue(UnitDataScriptableObject key, bool value)
    {
        if(hashtable.Contains(key))
        {
            Debug.Log("있는 데이터!");
            return;
        }
        hashtable.Add(key, value);
    }

    public UnitDataScriptableObject[] GetScriptableData()
    {
        return data;
    }
}
