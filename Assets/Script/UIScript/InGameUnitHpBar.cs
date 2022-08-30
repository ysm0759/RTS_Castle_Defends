using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InGameUnitHpBar : InGameUnitHP
{
    static public bool _uiOnOff = false;
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

    override public void SetData(float maxHP, float hp)
    {
        slider.maxValue = maxHP;
        slider.value = hp;
        this.gameObject.SetActive(true);
        GetComponentInParent<IDamagable>().inGameUnitHP = this;
        uiOnOffEvent += SetHide;
        hpBar.color = HPColor();
    }

    Color HPColor()
    {
        if (GetComponentInParent<Enemy>() != null)
        {
            return Color.red;
        }
        else
        {
            if (GetComponentInParent<Tower>() != null)
            {
                return Color.cyan;
            }
            else
            {
                return Color.green;
            }
        }
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
