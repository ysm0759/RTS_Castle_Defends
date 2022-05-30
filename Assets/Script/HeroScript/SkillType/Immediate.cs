using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Immediate : SkillType
{


    public override void ShowCancel()
    {

    }

    public override void ShowSkill(float scope, float range)
    {

    }

    public override void UseSkill(float range, IWarriorSkill skill)
    {

    }
    public override bool ImmediateSkill(IWarriorSkill skill)
    {
        skill.StartCoolDown();
        return true;
    }
}
