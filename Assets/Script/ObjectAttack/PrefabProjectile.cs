using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabProjectile : PrefabObject
{
    public float speed = 8;

    public TrailRenderer trailRenderer;

    private void Awake()
    {
        trailRenderer = GetComponent<TrailRenderer>();
    }

    private void OnEnable()
    {
        trailRenderer.enabled = true;
        trailRenderer.Clear();
    }

    private void OnDisable()
    {
        trailRenderer.Clear();
        trailRenderer.enabled = false;

    }

    private void Update()
    {
        if (target != null)
        {
            dir = target.transform.position - gameObject.transform.position;
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, target.transform.position, speed);
        }
        else
        {
            ObjectPool.ReturnObject("arrow", gameObject);
            return;
        }


        if (dir.sqrMagnitude < 3)
        {
            target?.GetComponent<IDamagable>().Hit(damage);
            ObjectPool.ReturnObject("arrow", gameObject);
        }
    }

}
