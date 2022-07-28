using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabMeteor : PrefabObject
{
    Collider[] hit;
    int layer;
    private void Awake()
    {
        hit = new Collider[10];
        if(gameObject.layer == LayerMask.GetMask("Enemy"))
        {
            layer = LayerMask.GetMask("Enemy");
        }
        else
        {
            layer = LayerMask.GetMask("User");
        }
    }
    private void Update()
    {
        dir = destination - gameObject.transform.position;
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, destination, 3f);

        if (dir.sqrMagnitude < 0.0001)
        {
            for(int i =0;i < Physics.OverlapSphereNonAlloc(gameObject.transform.position,5,hit, layer);i++)
            {
                hit[i].GetComponent<IDamagable>().Hit(damage);
            }

            ObjectPool.ReturnObject("meteor", gameObject);
        }

    }



}
