using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InGameUnitHpBar : InGameUnitHP
{
    static public bool _uiOnOff = true;
    static public bool uiOnOff
    {
        get
        {
            return _uiOnOff;
        }
        set
        {
            _uiOnOff = value;
            uiOnOffEvent?.Invoke(_uiOnOff);
        }
    }


    static public UnityAction<bool> uiOnOffEvent;
    [SerializeField] Image hpBar;

    override public void SetData(UnitDataScriptableObject data)
    {
        slider.maxValue = data.maxHp;
        slider.value = data.hp;
        this.gameObject.SetActive(true);
        GetComponentInParent<IDamagable>().inGameUnitHP = this;
        uiOnOffEvent += SetHide;
        hpBar.color = GetComponentInParent<Enemy>() != null ? Color.red : Color.green;
    }


    override public void ReturnObject()
    {
        uiOnOffEvent -= SetHide;
        ObjectPool.ReturnObject("hpBar",gameObject);
    }

    override public void SetHide(bool isHide)
    {

        Debug.Log(isHide);
        this.gameObject.SetActive(isHide);
    }

    override public void UpdateHpBar(float hp)
    {
        slider.value = hp;
    }
    private void LateUpdate()
    {
        transform.LookAt(transform.position + Camera.main.transform.forward);
    }

}
