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



    [SerializeField]Animator[] anim;
    private float animAttackTime;
    private float animAttackSpeed;

    Vector3 targetPosition;
    int towerIndex;

    [SerializeField]GameObject parentObject;

    private void Awake()
    {
        enemyList = new List<Collider>();
        objectAttack = GetComponent<ObjectAttack>();
        
    }

    private void Start()
    {
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
        towerIndex = (int)tower.GetTowerInfo().modelIndex;
        enemyList.Clear();
        canAttack = true;
        //GetComponent<Collider>().transform.localScale = attackRange; //콜라이더 크기 바꿔주기 TODO::
        if(anim.Length  != 0)
        {
            SetAnimAttackTime();

        }
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
        if(anim.Length != 0)
        {
            anim[towerIndex]?.SetFloat("AttackSpeed", animAttackSpeed);
        }
        while (true)
        {

            if (enemyList.Count <= 0)
               break;
            if(anim.Length != 0)
            {
                targetPosition = new Vector3(enemyList[0].transform.position.x, parentObject.transform.position.y, enemyList[0].transform.position.z);
                anim[towerIndex]?.SetBool("Attack", true);
                parentObject.transform.LookAt(targetPosition);
            }

            for (int i = 0; i < multiAttack && i < enemyList.Count; i++)
            {

                objectAttack.Attack(enemyList[i], attackDamage, attackName);
            }
            
            yield return new WaitForSeconds(attackSpeed);
        }


        canAttack = true;
        if (anim.Length != 0)
        {
            anim[towerIndex]?.SetBool("Attack", false);

            anim[towerIndex]?.SetFloat("AttackSpeed", animAttackSpeed);
        }

    }



    public void RemoveInListOfTower(Collider enemy)
    {
        enemyList.Remove(enemy);
    }

    public void SetAnimAttackTime()
    {
        RuntimeAnimatorController ac = anim[towerIndex].runtimeAnimatorController;
        for (int i = 0; i < ac.animationClips.Length; i++)
        {
            if (ac.animationClips[i].name.ToUpper().Contains("ATTACK"))
            {
                animAttackTime = ac.animationClips[i].length;
                break;
            }
        }
        SetAnimAttackSpeed();

    }
    private void SetAnimAttackSpeed()
    {
        animAttackSpeed = animAttackTime / attackSpeed;

    }
}
