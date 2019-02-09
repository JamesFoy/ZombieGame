using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour {

    CharacterAudioManager Audio;

    PlayerShooting playerShot;

    PlayerMovement playerMove;

    [SerializeField]
    private Animator anim;

    private bool Pistol = false;
    private bool TwoHanded = false;

    [SerializeField]
    private GameObject mag;
    [SerializeField]
    private GameObject mag2;

    [SerializeField]
    private Transform magSpawn;

    // Use this for initialization
    public void Start ()
    {
        playerMove = GetComponent<PlayerMovement>();
        playerShot = GetComponent<PlayerShooting>();
        Audio = GetComponent<CharacterAudioManager>();

        if (anim != null)
        {
            anim = GetComponent<Animator>();
        } return;
    }
	
	// Update is called once per frame
	void Update ()
    {
        anim.SetFloat("Horizontal", playerMove.h);
        anim.SetFloat("Vertical", playerMove.v);
        anim.SetBool("isShooting", playerShot.isShooting);
        anim.SetBool("isAiming", playerMove.aiming);
        anim.SetBool("Reload", playerShot.reload);
        anim.SetBool("Pistol", Pistol);
        anim.SetBool("TwoHanded", TwoHanded);
    }

    public void ReloadingGun()
    {
        if (playerShot.reload == true)
        {
            Audio.PlayReloadSound();
            anim.SetTrigger("Reloading");
            playerShot.emptyGun = false;
            playerShot.shotsDone = 0;
        }
    }

    public void MagOff()
    {
        Instantiate(mag2, magSpawn);
        mag.SetActive(false);
    }

    public void MagOn()
    {
        mag.SetActive(true);
    }
}
