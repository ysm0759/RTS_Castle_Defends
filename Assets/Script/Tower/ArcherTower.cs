using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherTower : Tower
{

    Collider[] hit;
    bool canAttack;
    private void Start()
    {
        towerInfo = GetComponent<TowerScriptable>();
    }



    public override void Attack()
    {

    }

    public override void Hit(float damage)
    {
    }

    public override void ResetTower()
    {
    }

    public override void Rotate()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if(CanAttack)
        {

        }
    }

}
