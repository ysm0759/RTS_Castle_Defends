using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUnitSelectButtons : MonoBehaviour
{
    [SerializeField] GameObject buttonPrefab;

    public InGameUnitSelectButtonElement inGameUnitSelectButtonElement;

    public List<InGameUnitSelectButtonElement> buttonList;
    public void AddButton(UnitDataScriptableObject unit)
    {
        GameObject button = Instantiate(buttonPrefab);
        button.transform.SetParent(gameObject.transform);
        inGameUnitSelectButtonElement = button.GetComponent<InGameUnitSelectButtonElement>();
        inGameUnitSelectButtonElement.Init();
        inGameUnitSelectButtonElement.SetImage(unit.sprite);
        buttonList.Add(inGameUnitSelectButtonElement);
    }

    public void AddGroup(UnitController unitController)
    {
        inGameUnitSelectButtonElement.AddSameKind(unitController);
    }

    public void Clear()
    {
        foreach(InGameUnitSelectButtonElement tmp in buttonList)
        {
            Destroy(tmp.gameObject);
        }
        buttonList.Clear();
    }

}
