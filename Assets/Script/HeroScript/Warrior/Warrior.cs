using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : Hero
{
    IWarriorSkill[] skills;


    private void Start()
    {
        skills = GetComponents<IWarriorSkill>();

    }

    public override void UseSkill(int num)
    {
        skills[num].UseSkill();
    }
    public override bool isCoolDown(int num)
    {
        return skills[num].IsCoolDown();
    }

    public override void ShowRange(int num)
    {
        skills[num].ShowRange();
    }

    public override void CancelSkill(int num)
    {
         skills[num].CanselSkill();
    }


}
