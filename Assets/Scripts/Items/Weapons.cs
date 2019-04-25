using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//Author - James Foy
//This script is used for all of the weapons stats that are used within the game. This will be inclulde how much ammo and grenades
//the player has and how much damage weapons do.

[Serializable]
public class Weapons
{
    [SerializeField]
    private int maxGrenades = 3;

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

    public int MaxGrenades
    {
        get
        {
            return maxGrenades;
        }
    }
}
