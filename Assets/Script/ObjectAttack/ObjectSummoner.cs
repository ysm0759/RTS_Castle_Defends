using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSummoner : ObjectAttack
{
    [SerializeField] int EA = 3;
    public override void Attack(Collider[] hit, float damage, string name = null)
    {
        for (int i = 0; i < EA; i++)
        {

            GameObject clone = ObjectPool.GetObject(name);
            clone.transform.position = transform.position;
        }
        // 아쳐가 10마리  초에 20번 호출된다.. 이걸 오브젝풀링 사용 
    }
}
