using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreNoMoneyPanel : MonoBehaviour
{
    [SerializeField] float time;
    [SerializeField] float duringTime;

    [SerializeField] GameObject panel;
    [SerializeField] Text text;
    Color color;

    private void OnEnable()
    {
        time = 0;
        color = text.color;
        color.a = 1f;
        text.color= color;
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (time < duringTime)
        {
            color = text.color;
            color.a -= 0.3f*Time.deltaTime /duringTime;
            text.color = color;
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
}
