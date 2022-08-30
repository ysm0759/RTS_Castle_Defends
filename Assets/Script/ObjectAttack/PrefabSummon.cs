using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSummon : PrefabObject
{

    private void OnEnable()
    {
        StartCoroutine(OffEffect());
    }

    IEnumerator OffEffect()
    {
        yield return new WaitForSeconds(2.0f);
        ObjectPool.ReturnObject("summon", gameObject);
        

    }

}
