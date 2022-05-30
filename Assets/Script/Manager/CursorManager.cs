using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CursorType
{
    DEFAULT = 0,
    ATTACK,
    BUILD,
    SHOW_SKILL_RANGE,
    SHOW_SKILL_TARGET,
}

public class CursorManager : MonoBehaviour
{
    [SerializeField] Texture2D cursorDefault;
    [SerializeField] Texture2D cursorAttack;
    [SerializeField] Texture2D cursorBuild;
    [SerializeField] Texture2D showSkillRange;

    [SerializeField] Texture2D showSkillTarget;
    Vector2 showSkillTargetPoint;

    static private CursorManager Instance;
    
    static public CursorManager instance
    {
        get
        {
            return Instance;
        }
    }
    
    
    void Start()
    {
        Instance = this;
        Cursor.SetCursor(cursorDefault, Vector2.zero, CursorMode.Auto);

        showSkillTargetPoint = new Vector2(showSkillTarget.width / 2, showSkillTarget.height / 2);
    }

    public void SetCursor(CursorType cursortype)
    {

        switch (cursortype)
        {


            case CursorType.DEFAULT:
                Cursor.visible = true;
                Cursor.SetCursor(cursorDefault, Vector2.zero, CursorMode.Auto);
                break;
            case CursorType.ATTACK:
                Cursor.visible = true;
                Cursor.SetCursor(cursorAttack, Vector2.zero, CursorMode.Auto);
                break;
            case CursorType.BUILD:
                Cursor.visible = true;
                Cursor.SetCursor(cursorBuild, Vector2.zero, CursorMode.Auto);
                break;
            case CursorType.SHOW_SKILL_RANGE:
                Cursor.visible = false;
                //Cursor.SetCursor(showSkillRange, Vector2.zero, CursorMode.ForceSoftware);
                break;

            case CursorType.SHOW_SKILL_TARGET:
                Cursor.visible = true;
                Cursor.SetCursor(showSkillTarget, showSkillTargetPoint, CursorMode.Auto);
                break;
        }
    }




}
