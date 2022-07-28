using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMeteor : ObjectAttack
{

    public override void Attack(Collider[] hit, float damage)
    {
        GameObject clone = ObjectPool.GetObject("meteor");
        clone.transform.position = hit[0].transform.position + (Vector3.up * 20);
        clone.GetComponent<PrefabObject>().SetDestinationDamage(hit[0].transform.position, damage);

    }
}
