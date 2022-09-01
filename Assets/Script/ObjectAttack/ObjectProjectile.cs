using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectProjectile : ObjectAttack
{
    public override void Attack(Collider hit, float damage, string name = null)
    {
        GameObject clone = ObjectPool.GetObject(name);
        clone.SetActive(true);
        clone.transform.position = transform.position;

        PrefabObject tmp = clone.GetComponent<PrefabObject>();
        tmp.SetDestinationDamage(hit.transform.position, damage,hit);
        tmp.SetName(name);
    }
}
