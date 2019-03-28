using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {

    [SerializeField]
    UIScript uiScript;

    public enum SpawnState { SPAWNING, WAITING, COUNTING };

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

    static List<AIScript> spawnedEnemies;

    public List<AIScript> SpawnedEnemies
    {
        get
        {
            return spawnedEnemies;
        }
    }

    public WaveSpawner()
    {
        spawnedEnemies = new List<AIScript>();
    }


    void Start()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No Spawn Points Referenced");
        }

        waveCountDown = timeBetweenWaves;
    }

    void Update()
    {
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

    void WaveCompleted()
    {
        Debug.Log("Wave completed");

        state = SpawnState.COUNTING;
        waveCountDown = timeBetweenWaves;

        if (nextWave + 1 > waves.Length - 1)
        {
            //Game State Complete
            nextWave = 0;
            Debug.Log("Completed all waves! Looping...");
        }
        else
        {
            uiScript.WaveInfo();
            nextWave++;
        }
    }

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

    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning wave: " + _wave.name);

        waveName = _wave.name;

        state = SpawnState.SPAWNING;

        //Spawn
        for (int i = 0; i < _wave.amount; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f/_wave.rate);
        }

        state = SpawnState.WAITING;

        yield break;
    }

    public void SpawnEnemy(AIScript _enemy)
    {
        //Spawn enemy
        Debug.Log("Spawning Enemy: " + _enemy.name);

        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        var enemySpawned = Instantiate(_enemy, _sp.position, _sp.rotation);
        enemySpawned.HaveDied += OnEnemyDied;
        spawnedEnemies.Add(enemySpawned);
    }

    private void OnEnemyDied(AIScript obj)
    {
        obj.HaveDied -= OnEnemyDied;
        spawnedEnemies.Remove(obj);
    }

}
