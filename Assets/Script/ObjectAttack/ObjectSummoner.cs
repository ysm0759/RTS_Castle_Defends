using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSummoner : ObjectAttack
{

    public override void Attack(Collider hit, float damage, string name = null)
    {

        GameObject clone = ObjectPool.GetObject(name);
        clone.transform.position = transform.position;
    }
}

