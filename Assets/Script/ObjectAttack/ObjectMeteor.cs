using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMeteor : ObjectAttack
{

    [SerializeField] GameObject prefab;
    public override void Attack(Collider[] hit, float damage)
    {

        GameObject clone = Instantiate(prefab, hit[0].transform.position + (Vector3.up *20), Quaternion.identity);
        clone.GetComponent<PrefabObject>().SetDestinationDamage(hit[0].transform.position, damage);

    }
}
