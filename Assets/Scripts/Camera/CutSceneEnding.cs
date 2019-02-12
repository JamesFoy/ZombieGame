using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneEnding : MonoBehaviour {

    [SerializeField]
    CutsceneBehaviour cutsceneBehaviour;

    private void OnEnable()
    {
        cutsceneBehaviour.Ending();
    }
}
