using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerStats
{
    [SerializeField]
    protected int playerHealth = 3;

    [SerializeField]
    private int money;

    private int Money 
    {
        get
        {
            return money;
        }
    }

    public int Health
    {
        get
        {
            return playerHealth;
        }

        set
        {
            this.playerHealth = value;
        }
    }
}
