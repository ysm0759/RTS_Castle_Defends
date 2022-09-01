using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabObject : MonoBehaviour
{
    protected Vector3 destination;
    protected Vector3 dir;
    protected float damage;
    protected Collider target;
    protected string objectPoolName;
    public void SetDestinationDamage(Vector3 destination, float damage ,Collider target = null)
    {
        this.destination = destination;
        gameObject.transform.LookAt(destination);
        this.damage = damage;
        this.target = target;

    }
    public void SetName(string name)
    {
        this.objectPoolName = name; 
    }
}
