using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author - James Foy
//This script was used as a test to make sure that the evenet system was working

namespace Events
{
    public class EventTriggers : MonoBehaviour
    {

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown("g"))
            {
                EventManager.TriggerEvent("WaveText");
            }
        }
    }
}
