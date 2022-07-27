﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectProjectile : ObjectAttack
{
    [SerializeField] GameObject prefab;
    [SerializeField] int multiAttack = 1;
    public override void Attack(Collider[] hit, float damage)
    {
        for(int i =0; i < multiAttack; i++)
        {
            GameObject clone = Instantiate(prefab, gameObject.transform.position, Quaternion.identity);
            clone.GetComponent<PrefabObject>().SetDestinationDamage(hit[i].transform.position, damage);
        }

    }
}