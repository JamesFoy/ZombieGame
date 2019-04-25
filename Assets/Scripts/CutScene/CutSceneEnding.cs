using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author - James Foy
//This script was used to for the cinemachine method calling so that the correct setting on the cutscene behaviour was ended

namespace CutScenes
{
    public class CutSceneEnding : MonoBehaviour
    {

        [SerializeField]
        CutsceneBehaviour cutsceneBehaviour;

        private void OnEnable()
        {
            cutsceneBehaviour.Ending();
        }
    }
}

