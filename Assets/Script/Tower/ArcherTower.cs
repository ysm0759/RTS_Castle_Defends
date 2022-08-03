using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherTower : Tower
{

    Collider[] hit;
    bool canAttack;
    private void Start()
    {
        hit = new Collider[10];
    } 



}
