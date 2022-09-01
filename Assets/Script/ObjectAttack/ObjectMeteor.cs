﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMeteor : ObjectAttack
{

    public override void Attack(Collider hit, float damage, string name = null)
    {
        GameObject clone = ObjectPool.GetObject(name);
        clone.SetActive(true);
        clone.transform.position = hit.transform.position + (Vector3.up * 20);
        PrefabObject tmp = clone.GetComponent<PrefabObject>();
        tmp.SetDestinationDamage(hit.transform.position, damage);
        tmp.SetName(name);

    }
}
