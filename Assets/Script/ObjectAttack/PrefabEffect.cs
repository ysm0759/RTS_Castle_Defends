using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabEffect : PrefabObject
{

    [SerializeField] string effectName;
    [SerializeField] float returnTime;
    private void OnEnable()
    {
        StartCoroutine(OffEffect());
    }

    IEnumerator OffEffect()
    {
        yield return new WaitForSeconds(returnTime);
        ObjectPool.ReturnObject(effectName, gameObject);
    }

}
