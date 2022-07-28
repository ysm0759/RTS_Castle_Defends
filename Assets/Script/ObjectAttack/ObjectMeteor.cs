using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMeteor : ObjectAttack
{

    public override void Attack(Collider[] hit, float damage, string name = null)
    {
        GameObject clone = ObjectPool.GetObject(name);
        clone.transform.position = hit[0].transform.position + (Vector3.up * 20);
        clone.GetComponent<PrefabObject>().SetDestinationDamage(hit[0].transform.position, damage);

    }
}
