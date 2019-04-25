using UnityEngine;
using System;
using System.Collections;

//Author - James Foy
//This script is used to control the animation for the players model hands.
//this allows the players character to correctly hold the gun.

[RequireComponent(typeof(Animator))]

public class HandPlacementIK : MonoBehaviour
{
    protected Animator animator;

    public bool ikActive = false;
    public Transform rightHandObj = null;
    public Transform lookObj = null;


    public Transform left = null;


    void Start()
    {
        animator = GetComponent<Animator>();
        ikActive = true;
    }


    //a callback for calculating IK
    void OnAnimatorIK()
    {
        if (animator)
        {

            //if the IK is active, set the position and rotation directly to the goal. 
            if (ikActive)
            {


                // Set the look target position, if one has been assigned
                if (lookObj != null)
                {
                    animator.SetLookAtWeight(1, 1);
                    animator.SetLookAtPosition(lookObj.position);
                }

                // Set the right hand target position and rotation, if one has been assigned
                if (rightHandObj != null)
                {
                    animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                    animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
                    animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandObj.position);
                    animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandObj.rotation);
                }

                if (left != null)
                {
                    animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
                    animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
                    animator.SetIKPosition(AvatarIKGoal.LeftHand, left.position);
                    animator.SetIKRotation(AvatarIKGoal.LeftHand, left.rotation);
                }

            }

            //if the IK is not active, set the position and rotation of the hand and head back to the original position
            else
            {
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
                animator.SetLookAtWeight(0, 0);
                animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0);
            }
        }
    }
}