using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabProjectile : PrefabObject
{

    private void Update()
    {
        dir = destination - gameObject.transform.position;
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, destination, 1f);

        if (dir.sqrMagnitude < 0.0001)
            ObjectPool.ReturnObject("arrow", gameObject);
    }

}
