﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Weapons
{
    [SerializeField]
    private int akDamage;

    [SerializeField]
    private int maxShots = 30; 

    public int AkDamge
    {
        get
        {
            return akDamage;
        }
    }

    public int MaxShots
    {
        get
        {
            return maxShots;
        }
    }
}
