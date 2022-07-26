using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRange : ObjectAttack
{
    public override void Attack(Collider[] hit)
    {
        Debug.Log("범위");

    }
}
