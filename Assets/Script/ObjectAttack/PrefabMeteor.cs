using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabMeteor : PrefabObject
{
    Collider[] hit;
    int layer;
    private void OnEnable()
    {
        hit = new Collider[10];
        if(gameObject.layer == LayerMask.GetMask("Enemy"))
        {
            layer = LayerMask.GetMask("User");
            Debug.Log("공격대상 아군");
        }
        else
        {
            layer = LayerMask.GetMask("Enemy");
            Debug.Log("적군");
        }
    }

    private void Update()
    {
        dir = destination - gameObject.transform.position;
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, destination, 3f);

        if (dir.sqrMagnitude < 0.0001)
        {
            int count = Physics.OverlapSphereNonAlloc(gameObject.transform.position, 5, hit, layer);
            for (int i =0;i < count;i++)
            {
                Debug.Log(layer);
                hit[i].GetComponent<IDamagable>().Hit(damage);
            }

            ObjectPool.ReturnObject(objectPoolName, gameObject);
        }

    }



}
