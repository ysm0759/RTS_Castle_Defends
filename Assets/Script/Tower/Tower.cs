using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : MonoBehaviour ,IDamagable
{

    protected TowerScriptable towerInfo;
    protected ObjectAttack objectAttack;


    public abstract void ResetTower(); // 모두 있어야 하는것

    public abstract void Attack(); // 개개인으로 있어야 하는것 

    public abstract void Rotate(); // 

    public abstract void Hit(float damage);

}
