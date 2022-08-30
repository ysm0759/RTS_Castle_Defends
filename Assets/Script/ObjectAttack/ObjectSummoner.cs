using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSummoner : ObjectAttack
{
    [SerializeField] UnitDataScriptableObject data;
    [SerializeField] EnemyControl enemyControl;


    [SerializeField] float radius = 0;

    GameObject gameObjectEffect;

    private void Awake()
    {
        enemyControl = GetComponent<EnemyControl>();
    }

    public override void Attack(Collider hit, float damage, string name = null)
    {
        for(int i = 0; i < data.multiAttack; i ++)
        {
            GameObject clone = ObjectPool.GetObject(name);
            clone.SetActive(true);
            clone.GetComponent<UnitInfo>().SetData(data);
            EnemySpawnManager.instance.EnemyListAdd(clone.GetComponent<Enemy>());


            Vector3 dir = new Vector3(0, 0, 0);
            float x = Random.Range(-radius, radius);
            float y = Mathf.Sqrt(radius * radius - x * x);
            dir += Vector3.right * x + Vector3.forward * y;
            clone.transform.position = gameObject.transform.position + dir;


            OnEffect(clone.transform.position);

            clone.GetComponent<EnemyControl>().SetNodeIndex(enemyControl.GetNodeIndex());

            AddHPBar(clone.transform);



        }
    }



    void OnEffect(Vector3 pos)
    {
        gameObjectEffect = ObjectPool.GetObject("summon");
        gameObjectEffect.transform.position = pos;
        gameObjectEffect.SetActive(true);

    }


    void AddHPBar(Transform transform)
    {
        GameObject hpBar = ObjectPool.GetObject("hpBar");
        hpBar.transform.SetParent(transform);
        hpBar.GetComponent<InGameUnitHpBar>().SetData(data.maxHp , data.hp);
        hpBar.transform.localPosition = Vector3.up * 2;

    }
}

