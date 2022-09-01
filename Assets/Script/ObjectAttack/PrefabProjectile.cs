using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabProjectile : PrefabObject
{
    public float speed = 8;

    public TrailRenderer trailRenderer;



    private void OnEnable()
    {
        trailRenderer.gameObject.SetActive(true);

    }

    private void OnDisable()
    {

        trailRenderer.Clear();
        trailRenderer.gameObject.SetActive(false);
    }

    private void Update()
    {

        if (target != null)
        {
            dir = target.transform.position - gameObject.transform.position;
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, target.transform.position, speed);
            gameObject.transform.LookAt(target.transform);
        }
        else
        {
            ObjectPool.ReturnObject(objectPoolName, gameObject);
            return;
        }


        if (dir.sqrMagnitude < 3)
        {
            if (target.enabled == true)
            {
                target?.GetComponent<IDamagable>().Hit(damage);
            }

            ObjectPool.ReturnObject(objectPoolName, gameObject);
        }
    }

}
