using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameHeroHpBar : InGameUnitHP
{


    [SerializeField] private Text text;

    override public void SetData(UnitDataScriptableObject data)
    {
        slider.maxValue = data.maxHp;
        slider.value = data.hp;
        text.text = data.hp.ToString();
        this.gameObject.SetActive(true);
        FindObjectOfType<Hero>().inGameHeroHpBar = this;
    }


    override public void UpdateHpBar(float hp)
    {
        text.text = hp.ToString();
        slider.value = hp;
    }


}
