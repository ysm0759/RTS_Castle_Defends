using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabObject : MonoBehaviour
{
    protected Vector3 destination;
    protected Vector3 dir;
    protected float damage;
    public void SetDestinationDamage(Vector3 destination, float damage)
    {
        this.destination = destination;
        gameObject.transform.LookAt(destination);
        this.damage = damage;
    }
}
