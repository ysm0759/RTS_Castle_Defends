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

    public void RemoveObject(UnitController controller)
    {
        unitController.Remove(controller);
        UpdateUI();

    }
    public void SelectUnits()
    {
        for(int i =0; i < unitController.Count;i++)
        {
            RTSUserUnitControlManager.instance.ShiftClickSelectUnit(unitController[i]);
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
