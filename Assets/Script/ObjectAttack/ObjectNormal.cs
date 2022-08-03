using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectNormal : ObjectAttack
{
    public override void Attack(Collider hit, float damage, string name = null)
    {
        hit.transform.GetComponent<IDamagable>()?.Hit(damage);
    }
}
