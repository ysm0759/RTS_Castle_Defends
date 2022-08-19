using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUnitHpBar : MonoBehaviour
{
    [SerializeField] Slider slider;

    public void SetData(UnitDataScriptableObject data)
    {
        slider.maxValue = data.maxHp;
        slider.value = data.hp;
        this.gameObject.SetActive(true);
    }

    public void SetHide(bool isHide)
    {
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
