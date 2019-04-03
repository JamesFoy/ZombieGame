using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Base class for all enemies
public class Enemy : MonoBehaviour
{
    public int health;

    public int speed;

    public int damage;

    [SerializeField]
    PlayerStats player;

    void Start()
    {

    }

    void Update()
    {

    }
}
