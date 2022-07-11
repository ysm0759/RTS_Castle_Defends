﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public interface IDamagable
{
    void Hit(float damage);
    Transform GetTransform();
}