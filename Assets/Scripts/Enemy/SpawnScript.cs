using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour {

    [SerializeField]
    AIScript enemy;
    [SerializeField]
    float delayBetweenSpawns = 2.0f;
    [SerializeField]
    float timeOfNextSpawn = 1f;

    List<AIScript> spawnedEnemies;

    public List<AIScript> SpawnedEnemies
    {
        get
        {
            return spawnedEnemies;
        }
    }

    public SpawnScript()
    {
        spawnedEnemies = new List<AIScript>();
    }

    // Update is called once per frame
    void Update ()
    {
        if (Time.time >= timeOfNextSpawn)
        {
            var enemySpawned = Instantiate(enemy, transform.position, Quaternion.identity);
            enemySpawned.HaveDied += OnEnemyDied;
            spawnedEnemies.Add(enemySpawned);
            timeOfNextSpawn = Time.time + delayBetweenSpawns;
        }
	}

    private void OnEnemyDied(AIScript obj)
    {
        obj.HaveDied -= OnEnemyDied;
        spawnedEnemies.Remove(obj);
    }
}
