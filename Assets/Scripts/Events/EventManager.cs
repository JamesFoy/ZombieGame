using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author - James Foy, Simon Hunt
//This script is used to create a event manager system for the project
namespace Events
{
    public class EventManager : MonoBehaviour
    {

        //This is creating the dictionary that is used to hold all of the references to events
        private Dictionary<string, Action> eventDictionary;
        private static EventManager eventManager;

        public static EventManager instance
        {
            get
            {
                if (!eventManager)
                {
                    //If there isnt a reference added find it
                    eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                    // If there is no event manager attached to a game object and no reference can be found
                    if (!eventManager)
                    {
                        Debug.LogError("There needs to be one active EventManager script on a GameObject in your scene");
                    }
                    else
                    {
                        eventManager.Init();
                    }
                }

                return eventManager;

            }
        }

        //Initialize the event dictonary
        void Init()
        {
            if (eventDictionary == null)
            {
                eventDictionary = new Dictionary<string, Action>();
            }
        }

        public static void StartListening(string eventName, Action listener)
        {
            Action thisEvent;
            if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                //Add more event to the existing one
                thisEvent += listener;

                //Update the Dictionary
                instance.eventDictionary[eventName] = thisEvent;
            }
            else
            {
                //Add event to the Dictionary for the first time
                thisEvent += listener;
                instance.eventDictionary.Add(eventName, thisEvent);
            }
        }

        public static void StopListening(string eventName, Action listener)
        {
            if (eventManager == null) return;
            Action thisEvent;
            if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                //Remove event from existing one
                thisEvent -= listener;

                //Update the Dictionary
                instance.eventDictionary[eventName] = thisEvent;
            }
        }

        //This method is used to trigger an event
        public static void TriggerEvent(string eventName)
        {
            Action thisEvent = null;
            if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.Invoke();
            }
        }
    }
}

