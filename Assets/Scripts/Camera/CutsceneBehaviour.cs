using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;
using Cinemachine;

public class CutsceneBehaviour : MonoBehaviour {

    [SerializeField]
    GameObject aimCam;
    [SerializeField]
    CameraFollow camFollow;
    [SerializeField]
    Animator anim;
    [SerializeField]
    PlayerAnimations playerAnim;
    [SerializeField]
    PlayerControl playerControl;
    [SerializeField]
    PlayerMovement playerMove;
    [SerializeField]
    PlayerShooting playerShot;
    [SerializeField]
    CharacterAudioManager Audio;
    [SerializeField]
    Collider collider;
    [SerializeField]
    HandPlacementIK handPlacementIK;
    [SerializeField]
    NavMeshAgent enemyAgent;
    [SerializeField]
    AIScript enemyBehaviour;
    [SerializeField]
    PlayableDirector director;
    [SerializeField]
    Canvas fade;
    [SerializeField]
    Rigidbody rb;
    [SerializeField]
    GameObject virtualCameras;
    [SerializeField]
    CinemachineBrain brain;

    public void Starting()
    {
        if (brain != null)
        {
            brain.enabled = true;
        }
        if (virtualCameras != null)
        {
            virtualCameras.SetActive(true);
        }
        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
        }
        if (fade != null)
        {
            fade.enabled = true;
        }
        if (director != null)
        {
            director.enabled = true;
        }
        if (anim != null)
        {
            anim.applyRootMotion = true;
        }
        if (aimCam != null)
        {
            aimCam.SetActive(false);
        }
        if (camFollow != null)
        {
            camFollow.enabled = false;
        }
        if (playerAnim != null)
        {
            playerAnim.enabled = false;
        }
        if (playerControl != null)
        {
            playerControl.enabled = false;
        }
        if (playerMove != null)
        {
            playerMove.enabled = false;
        }
        if (playerShot != null)
        {
            playerShot.enabled = false;
        }
        if (Audio != null)
        {
            Audio.enabled = false;
        }
        if (collider != null)
        {
            collider.enabled = false;
        }
        if (handPlacementIK != null)
        {
            handPlacementIK.enabled = false;
        }
        if (enemyAgent != null)
        {
            enemyAgent.enabled = false;
        }
        if (enemyBehaviour != null)
        {
            enemyBehaviour.enabled = false;
        }
    }

    public void Ending()
    {
        if (brain != null)
        {
            brain.enabled = false;
        }
        if (virtualCameras != null)
        {
            virtualCameras.SetActive(true);
        }
        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints.None;
            rb.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
        }
        if (fade != null)
        {
            fade.enabled = false;
        }
        if (director != null)
        {
            director.enabled = false;
        }
        if (anim != null)
        {
            anim.applyRootMotion = false;
        }
        if (aimCam != null)
        {
            aimCam.SetActive(true);
        }
        if (camFollow != null)
        {
            camFollow.enabled = true;
        }
        if (playerAnim != null)
        {
            playerAnim.enabled = true;
        }
        if (playerControl != null)
        {
            playerControl.enabled = true;
        }
        if (playerMove != null)
        {
            playerMove.enabled = true;
        }
        if (playerShot != null)
        {
            playerShot.enabled = true;
        }
        if (Audio != null)
        {
            Audio.enabled = true;
        }
        if (collider != null)
        {
            collider.enabled = true;
        }
        if (handPlacementIK != null)
        {
            handPlacementIK.enabled = true;
        }
        if (enemyAgent != null)
        {
            enemyAgent.enabled = true;
        }
        if (enemyBehaviour != null)
        {
            enemyBehaviour.enabled = true;
        }
    }
}
