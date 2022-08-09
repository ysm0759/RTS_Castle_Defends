﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabParabola : PrefabObject
{

    public float m_Speed = 10;
    public float m_HeightArc = 1;
    private Vector3 m_StartPosition;


    void Start()
    {
        m_StartPosition = transform.position;
    }

    void Update()
    {



        float x0 = m_StartPosition.x;
        float x1 = destination.x;
        float distance = x1 - x0;
        float nextX = Mathf.MoveTowards(transform.position.x, x1, m_Speed * Time.deltaTime);
        float baseY = Mathf.Lerp(m_StartPosition.y, destination.y, (nextX - x0) / distance);
        float arc = m_HeightArc * (nextX - x0) * (nextX - x1) / (-0.25f * distance * distance);
        Vector3 nextPosition = new Vector3(nextX, baseY + arc, transform.position.z);

        transform.rotation = LookAt2D(nextPosition - transform.position);
        transform.position = nextPosition;

        if (nextPosition == destination)
            Arrived();

    }

    void Arrived()
    {
        Debug.Log("도착");
        //Destroy(gameObject);
    }

    Quaternion LookAt2D(Vector2 forward)
    {
        return Quaternion.Euler(0, 0, Mathf.Atan2(forward.y, forward.x) * Mathf.Rad2Deg);
    }

}
