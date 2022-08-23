using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadyEnemyPortrait : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] Text ea;
    UnitDataScriptableObject data;
    [SerializeField] ReadyEnemyInfo readyEnemyInfoPanel;

    public void SetData(UnitDataScriptableObject data ,int ea, ReadyEnemyInfo readyEnemyInfoPanel)
    {
        this.data = data;
        image.sprite = data.sprite;
        this.ea.text = ea.ToString();
        this.gameObject.transform.localScale = Vector3.one;
        this.readyEnemyInfoPanel = readyEnemyInfoPanel;
    }

    public void OnMouseEnterFunc()
    {
        readyEnemyInfoPanel.SetData(data,transform);
        readyEnemyInfoPanel.gameObject.SetActive(true);
    }

    public void OnMouseExitFunc()
    {
        readyEnemyInfoPanel.gameObject.SetActive(false);
    }

}
