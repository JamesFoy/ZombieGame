using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Moves character using a rigidbody
/// </summary>


public class PlayerMovementRigidbody : MonoBehaviour {

    public string moveInputAxis = "Vertical"; // sets the vertical W and S keys to moveInputAxis
    public string turnInputAxis = "Horizontal"; // set the horizontal A and D keys to turnInputAxis

    public float rotationRate = 360; // sets the rotation speed when using the horizontal keys to 360' per second

    public float moveSpeed; //sets the moveSpeed of our character to 2

    private Rigidbody rb; //creats a variable for the characters rigidbody

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); //sets the rigidbody
    }

    // Update is called once per frame
    void Update ()
    {
        //store floats
        float moveAxis = Input.GetAxis(moveInputAxis); // grabbing a value between -1 and 1 for the keys pressed
        float turnAxis = Input.GetAxis(turnInputAxis);

        ApplyInput(moveAxis, turnAxis); // every frame update our rotation and postion based of the turn and move functions
	}

    private void ApplyInput(float moveInput, float turnInput)
    {
        Move(moveInput);
        Turn(turnInput);
    }

    private void Move(float input)
    {
        rb.AddForce(transform.forward * input * moveSpeed, ForceMode.Force); //moves character by applying a force to the forward postition
    }

    private void Turn(float input)
    {
        transform.Rotate(0, input * rotationRate * Time.deltaTime, 0);
    }
}
