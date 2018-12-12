using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

/// <summary>
/// Moves character using transform only
/// </summary>


public class PlayerMovementTransform : MonoBehaviour {

    PlayerIndex one; // sets how many players are in the game using controllers. This sets 1 player as player one.  
    GamePadState state;

    public string moveInputAxis = "Vertical"; // sets the vertical W and S keys to moveInputAxis
    public string turnInputAxis = "Horizontal"; // set the horizontal A and D keys to turnInputAxis

    private Animator anim;

    [SerializeField]
    float rotationRate = 40; // sets the rotation speed when using the horizontal keys

    public float moveSpeed; //sets the moveSpeed of our character to 2


    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update ()
    {
        PlayerIndex player = PlayerIndex.One;

        state = GamePad.GetState(player);

        //store floats
        float h = state.ThumbSticks.Left.X;
        float v = state.ThumbSticks.Left.Y;
        float moveAxis = Input.GetAxis(moveInputAxis);
        float turnAxis = Input.GetAxis(turnInputAxis);
        h = Input.GetAxis(moveInputAxis);
        v = Input.GetAxis(turnInputAxis);// grabbing a value between -1 and 1 for the keys pressed

        ApplyInput(moveAxis, turnAxis); // every frame update our rotation and postion based of the turn and move functions

        anim.SetFloat("Horizontal", moveAxis);
        anim.SetFloat("Vertical", turnAxis);

        if (Input.GetMouseButton(1) || state.Triggers.Left == -1)
        {
            transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);
        }
    }

    private void ApplyInput(float moveInput, float turnInput)
    {
        Move(moveInput);
        Turn(turnInput);
    }

    private void Move(float input)
    {
        transform.Translate(Vector3.forward * input * moveSpeed * Time.deltaTime); //makes the character move (due to it using local space it requires global variables which is why translate is used)
    }

    private void Turn(float input)
    {
        transform.Rotate(0, input * rotationRate * Time.deltaTime, 0);
    }
}
