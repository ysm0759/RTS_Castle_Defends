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
        image.sprite = data.sprite;
        
    }
}
