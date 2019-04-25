using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//Author - James Foy
//This script is used to give the enemies specific stats like health

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
