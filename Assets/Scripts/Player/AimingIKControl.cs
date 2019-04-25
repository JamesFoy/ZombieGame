using UnityEngine;
using System;
using System.Collections;

//Author - James Foy
//This script is used to allow the player to correct aim towards the centre of the screen.
//This script ends up not being fully implemented into the game and therefore doesnt get used

namespace Player
{
    [RequireComponent(typeof(Animator))]

    public class AimingIKControl : MonoBehaviour
    {
        private Animator anim;
        public Transform bspine;
        public bool activeIK;
        public Transform target;

        // Use this for initialization
        void Start()
        {
            anim = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        //This is what allows the animator to correctly position the model of the character to aim where the player looks
        private void OnAnimatorIK(int layerIndex)
        {
            if (false) return;

            bspine = anim.GetBoneTransform(HumanBodyBones.Spine);
            Vector3 dir = target.position - transform.position;

            if (activeIK)
            {
                anim.SetLookAtWeight(1, 0.18f, 1, 1, 1);
                anim.SetLookAtPosition(target.position);
                bspine.transform.rotation = Quaternion.LookRotation(dir, transform.up);
                anim.SetBoneLocalRotation(HumanBodyBones.Spine, Quaternion.LookRotation(target.transform.forward));
            }
            else
            {
                anim.SetLookAtWeight(0, 0, 0, 0, 0);
            }
        }
    }
}
