using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InGameUnitHpBar : MonoBehaviour
{
    [SerializeField] Slider slider;
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


    public void SetData(UnitDataScriptableObject data)
    {
        slider.maxValue = data.maxHp;
        slider.value = data.hp;
        this.gameObject.SetActive(true);
        GetComponentInParent<IDamagable>().inGameUnitHpBar = this;
        uiOnOffEvent += SetHide;
    }


    public void ReturnObject()
    {
        uiOnOffEvent -= SetHide;
        ObjectPool.ReturnObject("hpBar",gameObject);
    }

    public void SetHide(bool isHide)
    {

        Debug.Log(isHide);
        this.gameObject.SetActive(isHide);
    }

    public void UpdateHpBar(float hp)
    {
        slider.value = hp;
    }

    private void LateUpdate()
    {
        transform.LookAt(transform.position + Camera.main.transform.forward);
    }

}
