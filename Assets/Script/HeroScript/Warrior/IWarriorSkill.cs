using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWarriorSkill 
{
    void UseSkill();
    bool IsCoolDown();
    void ShowRange();
    void CanselSkill();
    void StartCoolDown(Collider hit = null);
}
