using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class Pistol : MonoBehaviour {

    private Animator anim;

    GamePadState state;

    PlayerIndex one; // sets how many players are in the game using controllers. This sets 1 player as player one.  

    private bool shoot;

    // Use this for initialization
    void Start ()
    {
        shoot = false;
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        PlayerIndex player = PlayerIndex.One;

        state = GamePad.GetState(player);

        anim.SetBool("Shoot", shoot);

        if (state.Triggers.Right >= 0.1)
        {
            shoot = true;
        }
        else
        {
            shoot = false;
        }
	}
}
