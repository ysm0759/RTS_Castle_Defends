using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour ,IDamagable
{

    protected TowerInfo towerInfo; //소환할떄 그 데이터 세팅 해주기!
    protected ObjectAttack objectAttack;
    [SerializeField] GameObject rangeObject;


 
    public void SetTowerInfo()
    {
        
    }



    public void Hit(float damage)
    {
        
    }







}
