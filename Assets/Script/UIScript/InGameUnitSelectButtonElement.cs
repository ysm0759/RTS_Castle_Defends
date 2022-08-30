using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUnitSelectButtonElement : MonoBehaviour
{
    List<UnitController> unitController;
    [SerializeField] Button button;
    [SerializeField] Image image;
    [SerializeField] Text text;


    public void Init()
    {
        unitController = new List<UnitController>();
        button.onClick.AddListener(SelectUnits);
    }
    public void AddSameKind(UnitController controller)
    {
        unitController.Add(controller);
        UpdateUI();
    }

    public void RemoveObject(GameObject deathUnit)
    {
        for (int i = 0; i < unitController.Count; i++)
        {
            if (unitController[i].gameObject == deathUnit)
            {
                unitController.RemoveAt(i);
            }
        }

        UpdateUI();
    }

    public void SelectUnits()
    {
        for(int i =0; i < unitController.Count;i++)
        {
            RTSUserUnitControlManager.instance.DragSelectUnit(unitController[i]);
        }
    }

    public void UpdateUI()
    {
        text.text = unitController.Count.ToString();
    }

    public void SetImage(Sprite sprite)
    {
        this.image.sprite = sprite;
    }
}
