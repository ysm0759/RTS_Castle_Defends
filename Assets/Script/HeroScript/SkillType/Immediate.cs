using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Immediate : MonoBehaviour
{



    public bool ImmediateSkill(ISkill skill)
    {
        skill.StartCoolDown();
        return true;
    }
}
