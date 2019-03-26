using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Events : MonoBehaviour {

    private void OnEnable()
    {
        EventManager.StartListening("CurrentWave", CurrentWave);
        EventManager.StartListening("NextWave", NextWave);
    }

    void CurrentWave()
    {

    }

    void NextWave()
    {

    }
}
