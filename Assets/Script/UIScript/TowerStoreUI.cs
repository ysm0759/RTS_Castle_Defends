using UnityEngine;
using UnityEngine.UI;

public class TowerStoreUI : MonoBehaviour
{

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
            nextHp.text = (data.hp- data.nextData.hp).ToString();
            nextDf.text = (data.df - data.nextData.df).ToString();
            nextAttackDamage.text = (data.damage - data.nextData.damage).ToString();
            nextAttackSpeed.text = (data.attackSpeed - data.nextData.attackSpeed).ToString();
        }
        else
        {
            nextHp.text = "";
            nextDf.text = "";
            nextAttackDamage.text = "";
            nextAttackSpeed.text = "";
        }



    }
}
