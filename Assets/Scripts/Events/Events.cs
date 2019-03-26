using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Events : MonoBehaviour {

    [SerializeField]
    public Wave wave;

    [SerializeField]
    UnityEvent nextwave;

    private void Update()
    {
        if (wave.enemyCount == 0)
        {
            Debug.Log("Go to next event");
            NextWave();
        }
    }

    void NextWave()
    {
        nextwave.Invoke();
    }
}
