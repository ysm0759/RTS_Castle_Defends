using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour ,IDamagable
{

    protected TowerInfo towerInfo; //소환할떄 그 데이터 세팅 해주기!


    
    public void SetTowerInfo()
    {
        this.gameObject.SetActive(true);
        towerInfo.hp = towerInfo.maxHp;
    }



    public void Hit(float damage) // 쳐 맞기 구현
    {
        
    }


    public TowerInfo GetTowerInfo()
    {
        return towerInfo;
    }




}
