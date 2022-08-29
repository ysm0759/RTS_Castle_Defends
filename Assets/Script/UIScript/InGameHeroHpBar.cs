using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameHeroHpBar : InGameUnitHP
{


    [SerializeField] private Text text;

    override public void SetData(float maxHp , float hp)
    {
        slider.maxValue = maxHp;
        slider.value = hp;
        text.text = hp.ToString();
        this.gameObject.SetActive(true);
        FindObjectOfType<Hero>().inGameHeroHpBar = this;
    }


    override public void UpdateHpBar(float hp)
    {
        text.text = hp.ToString();
        slider.value = hp;
    }


}
