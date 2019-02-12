using UnityEngine;

public class CutSceneStarting : MonoBehaviour
{
    [SerializeField]
    CutsceneBehaviour cutsceneBehaviour;

    private void OnEnable()
    {
        cutsceneBehaviour.Starting();
    }
}
