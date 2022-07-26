using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePrefab : MonoBehaviour
{

    Vector3 destination;
    Vector3 dir;
    private void Update()
    {
        dir = destination - gameObject.transform.position;
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, destination, 1f);

        if (dir.sqrMagnitude < 0.0001)
            Destroy(gameObject);
    }

    public void SetDestination(Vector3 destination)
    {
        this.destination = destination;
        gameObject.transform.LookAt(destination);

    }
}
