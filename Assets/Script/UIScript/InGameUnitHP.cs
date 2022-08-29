using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InGameUnitHP : MonoBehaviour
{


    [SerializeField] protected Slider slider;

    virtual public void SetData(float maxHp, float hp) { }
    virtual public void UpdateHpBar(float hp){ }

    virtual public void ReturnObject() { }
    virtual public void SetHide(bool isHide) { }


}
