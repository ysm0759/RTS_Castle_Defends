using UnityEngine;
using UnityEngine.UI;

public class TowerStoreUI : MonoBehaviour
{

    [SerializeField] TowerScriptable[] defaultData;
    [SerializeField] TowerElementUI[] elementData;


    [SerializeField] TowerScriptable data;


    
    [SerializeField] Text hp;
    [SerializeField] Text nextHp;
    [SerializeField] Text df;
    [SerializeField] Text nextDf;
    [SerializeField] Text attackDamage;
    [SerializeField] Text nextAttackDamage;
    [SerializeField] Text attackSpeed;
    [SerializeField] Text nextAttackSpeed;
    [SerializeField] Text level;
    [SerializeField] Text upgradeCost;
    [SerializeField] Text explain;

    [SerializeField] Image image;

    private void Awake()
    {
        elementData = GetComponentsInChildren<TowerElementUI>();
        OnClickedReset();

    }
    public void SetTowerData(TowerScriptable towerData)
    {
        data = towerData;
        SetInfo();
    }

    private void SetInfo()
    {
        hp.text = data.hp.ToString();
        df.text = data.df.ToString();
        attackDamage.text = data.damage.ToString();
        attackSpeed.text = data.attackSpeed.ToString();
        level.text = data.level.ToString();
        upgradeCost.text = data.upgradeCost.ToString();
        explain.text = data.explain;

        image.sprite = data.sprite;
        
        if(data.nextData != null)
        {
            nextHp.text = string.Format("{0:0.#}", (data.nextData.hp- data.hp));
            nextDf.text = string.Format("{0:0.#}", (data.nextData.df- data.df));
            nextAttackDamage.text = string.Format("{0:0.#}", (data.nextData.damage- data.damage));
            nextAttackSpeed.text = string.Format("{0:0.#}", (data.nextData.attackSpeed- data.attackSpeed));
            upgradeCost.text = string.Format("{0}", data.upgradeCost);
        }
        else
        {
            nextHp.text = "";
            nextDf.text = "";
            nextAttackDamage.text = "";
            nextAttackSpeed.text = "";
            upgradeCost.text = "MAX";
        }
    }


    public void OnClickedBuy()
    {
        TowerManager.instance.BuyTower(data);
    }
    public void OnClickedSell()
    { 
        TowerManager.instance.SellTower(data);
    }

    public void OnClickedUpgrade()
    {
        if(data.nextData == null  )
        {
            return;
        }
        else if (GameManager.instance.UseCost(data.upgradeCost))
        {
            
            data = data.nextData;
            elementData[(int)data.towerIndex].SetData(data);
            TowerManager.instance.UpgradeTower(data);
        }
    }

    public void OnClickedReset()
    {
        for(int i =0; i <elementData.Length;i++)
        {
            elementData[i].SetData(defaultData[i]);
        }

        data = defaultData[0];
        SetInfo();
    }

}
