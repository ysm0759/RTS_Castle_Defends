using System;
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



    [Serializable]
    public struct SerializeFieldDictionary
    {
        public string name;
        public GameObject PrefabObject;
    }
    public SerializeFieldDictionary[] poolingObjectPrefab;


    Dictionary<string, GameObject> prefabDic;
    Dictionary<string, Queue<GameObject>> poolingObjectQueues;


    private void Awake()
    {
        Instance = this;
        prefabDic = new Dictionary<string, GameObject>();
        poolingObjectQueues = new Dictionary<string, Queue<GameObject>>();



        for (int i =0;i < poolingObjectPrefab.Length;i++)
        {
            prefabDic.Add(poolingObjectPrefab[i].name, poolingObjectPrefab[i].PrefabObject);
            poolingObjectQueues.Add(poolingObjectPrefab[i].name, new Queue<GameObject>());
        }
    }

    private GameObject CreateNewObject(string name)
    {
        var newObj = Instantiate(prefabDic[name]);
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(transform);
        return newObj;
    }


    public static GameObject GetObject(string name)
    {
        if (Instance.poolingObjectQueues[name]?.Count > 0)
        {
            var obj = Instance.poolingObjectQueues[name].Dequeue();
            obj.transform.SetParent(null);
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            var newObj = Instance.CreateNewObject(name);
            newObj.gameObject.SetActive(true);
            newObj.transform.SetParent(null);
            return newObj;
        }
    }

    public static void ReturnObject(string name ,GameObject obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(Instance.transform);
        Instance.poolingObjectQueues[name].Enqueue(obj);
    }

}
