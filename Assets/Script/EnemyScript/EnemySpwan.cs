using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpwan : MonoBehaviour
{
    public GameObject unitPrefab;
    public Transform center;
    public int destance;
    public int offsetZ;


    private void Start()
    {
        transform.position = transform.position + Vector3.right * destance;
        transform.position = transform.position + Vector3.up * offsetZ;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {


            center.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
      
            Instantiate(unitPrefab, transform.position, Quaternion.identity);


        }

    }
}
