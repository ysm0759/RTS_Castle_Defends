using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUnitHeroHpBar : MonoBehaviour
{


    [SerializeField] protected Slider slider;


    virtual public void SetData(UnitDataScriptableObject data)
    {
        slider.maxValue = data.maxHp;
        slider.value = data.hp;
        this.gameObject.SetActive(true);
        GetComponentInParent<IDamagable>().inGameUnitHpBar = this;
    }


    public void UpdateHpBar(float hp)
    {
        slider.value = hp;
    }



    virtual public void ReturnObject() { }
    virtual public void SetHide(bool isHide) { }


}
