using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSummoner : ObjectAttack
{
    public int EA;


    public override void Attack(Collider hit, float damage, string name = null)
    {
        for(int i = 0; i < EA; i ++)
        {
            GameObject clone = ObjectPool.GetObject(name);
            clone.SetActive(true);
            clone.transform.position = transform.position;
        }
    }
}

