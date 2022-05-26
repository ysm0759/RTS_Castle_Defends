using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillType : MonoBehaviour
{

    public abstract void UseSkill(float range , IWarriorSkill skill);
    public abstract void ShowSkill(float scope, float range);

    public abstract void ShowCancel();

}
