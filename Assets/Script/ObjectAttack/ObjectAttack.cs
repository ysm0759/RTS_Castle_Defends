using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectAttack : MonoBehaviour
{
    public abstract void Attack(Collider[] hit);
}
