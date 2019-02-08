using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class PlayerControl : MonoBehaviour {

    PlayerIndex one; // sets how many players are in the game using controllers. This sets 1 player as player one.

    public GamePadState state;

    PlayerAnimations playerAnim;

    PlayerShooting playerShot;

    CharacterAudioManager Audio;

    // Use this for initialization
    void Start ()
    {
        playerAnim = GetComponent<PlayerAnimations>();
        Audio = GetComponent<CharacterAudioManager>();
        playerShot = GetComponent<PlayerShooting>();
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        PlayerIndex player = PlayerIndex.One;

        state = GamePad.GetState(player);

        if (playerShot.isShooting == true)
        {
            GamePad.SetVibration(player, 1, state.Triggers.Right);
            Audio.PlayGunSound();
        }
        else
        {
            GamePad.SetVibration(player, 0, 0);
        }
    }
}
