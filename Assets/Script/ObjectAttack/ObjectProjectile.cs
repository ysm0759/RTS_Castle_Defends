using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectProjectile : ObjectAttack
{
    [SerializeField] int multiAttack = 1;

    public override void Attack(Collider[] hit, float damage, string name = null)
    {
        for(int i =0; i < multiAttack; i++)
        {

            GameObject clone = ObjectPool.GetObject(name);
            clone.transform.position = transform.position;
            clone.GetComponent<PrefabObject>().SetDestinationDamage(hit[i].transform.position, damage);
            hit[i].GetComponent<IDamagable>().Hit(damage);
        }
        // 아쳐가 10마리  초에 20번 호출된다.. 이걸 오브젝풀링 사용 
    }
}
