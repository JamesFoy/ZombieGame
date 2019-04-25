using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//Author - James Foy
//This script is used to give the player specific stats like health and ammount of money

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
