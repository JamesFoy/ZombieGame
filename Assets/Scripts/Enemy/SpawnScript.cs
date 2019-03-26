using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour {

    [SerializeField]
    Transform spawnPoint;

    public Wave wave;

    AIScript enemy;

    AIScript enemyspawn;

    static int spawnCount = 0;

    [SerializeField]
    float delayBetweenSpawns = 2.0f;
    [SerializeField]
    float timeOfNextSpawn = 1f;

    static List<AIScript> spawnedEnemies;

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
        foreach (AIScript enemy in wave.enemyTypes)
        {
            enemyspawn = enemy;
        }

        if (spawnCount == 0)
        {
            Debug.Log("NextWave");
            EventManager.TriggerEvent("NextWave");
        }

        if (Time.time >= timeOfNextSpawn && wave.enemyCount > spawnCount)
        {
            var enemySpawned = Instantiate(enemyspawn, transform.position, Quaternion.identity, spawnPoint);
            enemySpawned.HaveDied += OnEnemyDied;
            spawnedEnemies.Add(enemySpawned);
            timeOfNextSpawn = Time.time + delayBetweenSpawns;
            spawnCount++;
        }
	}

    private void OnEnemyDied(AIScript obj)
    {
        obj.HaveDied -= OnEnemyDied;
        spawnedEnemies.Remove(obj);
    }
}
