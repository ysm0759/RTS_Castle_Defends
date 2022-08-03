using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherTower : Tower
{

    Collider[] hit;
    bool canAttack;
    private void Start()
    {

        objectAttack = GetComponent<ObjectProjectile>();
    } 

    public void Attack()
    {
        objectAttack.Attack(hit[0], towerInfo.damage, towerInfo.attackName);
    }


}
