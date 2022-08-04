using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTower : MonoBehaviour
{

    List<Collider> hit;
    ObjectAttack objectAttack;
    bool canAttack;


    float attackDamage;
    int attackSpeed;
    int attackRange;
    int multiAttack;
    string attackName;


    private void Start()
    {

        objectAttack = GetComponent<ObjectAttack>();
    }


    public void Attack(Collider hit)
    {
        objectAttack.Attack(hit, attackDamage, attackName);
    }


    public void AddHitTaget(Collider target)
    {
        if(hit.Count < multiAttack)
        {
            hit.Add(target);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if(canAttack)
        {
            AddHitTaget(other);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(canAttack)
        {
            Attack(other);
        }
    }
}
