using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class EnemyStats
{
    [SerializeField]
    private int enemyHealth;

    public int Health
    {
        get
        {
            return enemyHealth;
        }

        set
        {
            this.enemyHealth = value;
        }
    }
}
