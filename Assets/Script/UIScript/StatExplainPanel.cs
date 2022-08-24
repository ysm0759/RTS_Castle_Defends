using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatExplainPanel : MonoBehaviour
{
    [SerializeField] Text explain;
    private void Awake()
    {
        Debug.Log("?!!!!?");
        this.gameObject.SetActive(false);
    }
    public void SetData(string text)
    {
       this.explain.text = text;
    }
}
