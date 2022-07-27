using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum OBJECT_NAME
{
    User_Warrior,
    User_Archer,
    User_Megician,
    
    User_Projectile,
    User_Meteor,


    Enemy_Melee,
}

public class ObjectPool : MonoBehaviour
{

    public static ObjectPool Instance;

    [SerializeField]
    private GameObject poolingObjectPrefab;
    // 내가 원하는건 단품이 아님 세트메뉴임
    // 아처 , 뭐 , 뭐시기 
    Queue<UnitController> poolingObjectQueue = new Queue<UnitController>();

    Hashtable hashtable;


    private void Awake()
    {
        Instance = this;
        hashtable = new Hashtable();
        Initialize(10);
    }

    private void Initialize(int initCount)
    {
        for (int i = 0; i < initCount; i++)
        {
            poolingObjectQueue.Enqueue(CreateNewObject());
        }
    }

    private UnitController CreateNewObject()
    {
        var newObj = Instantiate(poolingObjectPrefab).GetComponent<UnitController>();
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(transform);
        return newObj;
    }

    public static UnitController GetObject()
    {
        if (Instance.poolingObjectQueue.Count > 0)
        {
            var obj = Instance.poolingObjectQueue.Dequeue();
            obj.transform.SetParent(null);
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            var newObj = Instance.CreateNewObject();
            newObj.gameObject.SetActive(true);
            newObj.transform.SetParent(null);
            return newObj;
        }
    }

    public static void ReturnObject(UnitController obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(Instance.transform);
        Instance.poolingObjectQueue.Enqueue(obj);
    }

}
