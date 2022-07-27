using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectNormal : ObjectAttack
{
    public override void Attack(Collider[] hit, float damage )
    {
        hit[0].transform.GetComponent<IDamagable>()?.Hit(damage);
    }
}
