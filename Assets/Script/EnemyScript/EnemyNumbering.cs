using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNumbering : MonoBehaviour
{
    string enemyNumbering;

    public void SetEnemyNumbering(int number)
    {
        enemyNumbering = number.ToString();
    }

    public string GetEnemyNumbering()
    {
        return enemyNumbering;
    }
}
