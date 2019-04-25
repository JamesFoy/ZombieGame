using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author - James Foy, Brackeys (https://www.youtube.com/watch?v=Vrld13ypX_I)
//This script is used to create a wave spawning mechanic, this is also used to update certain UI elements

public class WaveSpawner : MonoBehaviour {

    [SerializeField]
    UIScript uiScript;

    public enum SpawnState { SPAWNING, WAITING, COUNTING };

    //This script simply contains specific information about what a wave contains
    [System.Serializable]
    public class Wave
    {
        //name of the wave
        public string name;
        //prefab to spawn
        public AIScript enemy;
        //amount to spawn each wave
        public int amount;
        //spawn rate
        public float rate;
    }

    public Wave[] waves;
    //index for next wave
    private int nextWave = 0;

    public Transform[] spawnPoints;

    //Time between each wave
    public float timeBetweenWaves = 5f;

    [SerializeField]
    private float waveCountDown;

    public string waveName;

    private float searchCountDown = 1f;

    private SpawnState state = SpawnState.COUNTING;

    //THis is used to create a list of spawned enemeies 

    static List<AIScript> spawnedEnemies;

    public List<AIScript> SpawnedEnemies
    {
        get
        {
            return spawnedEnemies;
        }
    }


    //This will set a spawned enemy to a new value in the list created before, containing the AIScripts on the enemy
    public WaveSpawner()
    {
        spawnedEnemies = new List<AIScript>();
    }

    //This will set the wave countdown for the wave spawner
    void Start()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No Spawn Points Referenced");
        }

        waveCountDown = timeBetweenWaves;
    }

    //This is used to control the different states of the wave spawner
    void Update()
    {
        //this controls what happens during the waiting state
        if (state == SpawnState.WAITING)
        {
            //Check if enemies are still alive
            if (!EnemyIsAlive())
            {
                //Begin new round
                WaveCompleted();
            }
            else
            {
                return;
            }
        }

        //this controls what happens during the spawning state
        if (waveCountDown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                //Start spawning wave
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountDown -= Time.deltaTime;
        }
    }

    //This method controls all of the behaviour when the wave is completed
    void WaveCompleted()
    {
        Debug.Log("Wave completed");

        state = SpawnState.COUNTING;
        waveCountDown = timeBetweenWaves;

        //This checks to see if all of the waves are completed and if not the next wave starts
        if (nextWave + 1 > waves.Length - 1)
        {
            //Game State Complete
            nextWave = 0;
            Debug.Log("Completed all waves! Looping...");
        }
        else
        {
            EventManager.TriggerEvent("WaveText");
            nextWave++;
        }
    }

    //This boolean used is to track if any enemy is still alive. This boolean will change itself 
    //based on if there is any enemy still alive
    bool EnemyIsAlive()
    {
        searchCountDown -= Time.deltaTime;

        if (searchCountDown <= 0)
        {
            searchCountDown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        } 
        return true;
    }

    //This enumerator is ued to spawn the enemies using the wave class that was created at the start of the script
    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning wave: " + _wave.name);

        waveName = _wave.name;

        state = SpawnState.SPAWNING;

        //Spawn the enemies
        for (int i = 0; i < _wave.amount; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f/_wave.rate);
        }

        //once all enemies are spawned set the wave spawner state to waiting
        state = SpawnState.WAITING;

        yield break;
    }

    //This method controls the behaviour of how an enemy is spawned e.g. where it is spawned as well as adding it 
    //to the list created earlier in the script
    public void SpawnEnemy(AIScript _enemy)
    {
        //Spawn enemy
        Debug.Log("Spawning Enemy: " + _enemy.name);

        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        var enemySpawned = Instantiate(_enemy, _sp.position, _sp.rotation);
        enemySpawned.HaveDied += OnEnemyDied;
        spawnedEnemies.Add(enemySpawned);
    }

    //This is used for cleanup of the list created before. It makes sure that when an enemy dies
    //the reference in the list is removed
    private void OnEnemyDied(AIScript obj)
    {
        obj.HaveDied -= OnEnemyDied;
        spawnedEnemies.Remove(obj);
    }

}
