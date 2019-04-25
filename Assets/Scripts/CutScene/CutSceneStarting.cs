using UnityEngine;

//Author - James Foy
//This script was used to for the cinemachine method calling so that the correct setting on the cutscene behaviour was started

namespace CutScenes
{
    public class CutSceneStarting : MonoBehaviour
    {
        [SerializeField]
        CutsceneBehaviour cutsceneBehaviour;

        private void OnEnable()
        {
            cutsceneBehaviour.Starting();
        }
    }
}
