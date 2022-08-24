using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum Stat
{
    HP,
    DF,
    DAMAGE,
    ATTACK_SPEED,
    MOVE_SPEED,
    ATTACK_RANGE,
    POPULATION,
    LEVLE_COST,

}


public class StatUI : MonoBehaviour
{

    [SerializeField] Sprite[] icons;
    [SerializeField] Stat stat;
    [SerializeField] Image icon;
    [SerializeField] StatExplainPanel statExplainPanel;




    public void Awake()
    {
        icon.sprite = icons[(int)stat];

    }


    public void OnMouseEnterFunc()
    {
        statExplainPanel.gameObject.SetActive(true);
        statExplainPanel.SetData(stat.ToString());
        statExplainPanel.transform.position = this.transform.position + (Vector3.right * 100);
            
    }
    public void OnMouseExitFunc()
    {
        statExplainPanel.gameObject.SetActive(false);
            
    }
}
