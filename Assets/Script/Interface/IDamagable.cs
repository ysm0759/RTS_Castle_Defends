using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public interface IDamagable
{
    InGameUnitHpBar inGameUnitHpBar
    {
        get;
        set;
    }

    void Hit(float damage);
}