using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public interface IDamagable
{
    InGameUnitHP inGameUnitHP
    {
        get;
        set;
    }

    void Hit(float damage);
}   