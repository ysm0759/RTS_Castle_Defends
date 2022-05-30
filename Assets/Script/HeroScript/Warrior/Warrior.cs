using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : Hero
{
    IWarriorSkill[] aaa;


    private void Start()
    {
        aaa = GetComponents<IWarriorSkill>();

    }

    public override void UseSkill(int num)
    {
        aaa[num].UseSkill();
    }
    public override bool isCoolDown(int num)
    {
        return aaa[num].IsCoolDown();
    }

    public override void ShowRange(int num)
    {
        aaa[num].ShowRange();
    }

    public override void CancelSkill(int num)
    {

         aaa[num].CanselSkill();
    }

}
