using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSummoner : ObjectAttack
{
    [SerializeField] int EA;
    [SerializeField] UnitDataScriptableObject data;
    [SerializeField] EnemyControl enemyControl;

    private void Awake()
    {
        enemyControl = GetComponent<EnemyControl>();
    }

    public override void Attack(Collider hit, float damage, string name = null)
    {
        for(int i = 0; i < EA; i ++)
        {
            GameObject clone = ObjectPool.GetObject(name);
            clone.SetActive(true);
            clone.transform.position = transform.position;
            clone.GetComponent<UnitInfo>().SetData(data);


            clone.GetComponent<EnemyControl>().SetNodeIndex(enemyControl.GetNodeIndex());


            GameObject hpBar = ObjectPool.GetObject("hpBar");
            hpBar.transform.SetParent(clone.transform);
            hpBar.GetComponent<InGameUnitHpBar>().SetData(data);

            hpBar.transform.localPosition = Vector3.up * 2;




        }
    }
}

