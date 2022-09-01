using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTower : MonoBehaviour
{

    List<Collider> enemyList;
    ObjectAttack objectAttack;
    Tower tower;
    bool canAttack;


    float  attackDamage;
    float  attackSpeed;
    int    multiAttack;
    string attackName;


    

    
    private void Start()
    {
        enemyList = new List<Collider>();
        objectAttack = GetComponent<ObjectAttack>();
        canAttack = true;

        SetData();
    }
    
    public void SetData()
    {
        StartCoroutine(InitTowerData());
    }
 

    IEnumerator InitTowerData()
    {
        yield return null;

        tower = GetComponentInParent<Tower>();

        attackDamage = tower.GetTowerInfo().damage;
        attackSpeed = tower.GetTowerInfo().attackSpeed;
        multiAttack = tower.GetTowerInfo().multiAttack;
        attackName = tower.GetTowerInfo().attackName;
        enemyList.Clear();
        canAttack = true;
        //GetComponent<Collider>().transform.localScale = attackRange; //콜라이더 크기 바꿔주기 TODO::


    }


    private void OnTriggerEnter(Collider other)
    {
        enemyList.Add(other);

        other.GetComponent<Enemy>().onDead += RemoveInListOfTower;

        if (canAttack)
        {
            StartCoroutine(Attack());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        enemyList.Remove(other);
        other.GetComponent<Enemy>().onDead -= RemoveInListOfTower;
    }


    //public void RemoveEnemyList(Collider other)
    //{
    //    enemyList.Remove(other);
    //}

    IEnumerator Attack()
    {
        canAttack = false;
        while(true)
        {
            if(enemyList.Count <= 0)
               break;
            for(int i = 0; i < multiAttack && i < enemyList.Count; i++)
            {
                objectAttack.Attack(enemyList[i], attackDamage, attackName);
            }
            
            yield return new WaitForSeconds(attackSpeed);
        }


        canAttack = true;
    
    }

    public void RemoveInListOfTower(Collider enemy)
    {
        enemyList.Remove(enemy);
    }


}
