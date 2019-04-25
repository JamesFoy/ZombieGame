using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author - James Foy, Sharp Accent (https://www.youtube.com/watch?v=gQmU65ZHNxo&list=PL1bPKmY0c-wmfD6FzpuyFCmPPg3bwxMqe&index=4)
//This script controls all of the player audio for the game.

namespace Player
{
    public class CharacterAudioManager : MonoBehaviour
    {

        public AudioSource gunSounds;
        public AudioSource AkReload;
        public AudioSource runFoley;

        public float footStepTimer;
        public float walkThreshold;
        public float runThreshold;
        public AudioSource footStep1;
        public AudioSource footStep2;
        public AudioClip[] footStepClips;
        public AudioSource effectsSource;
        public AudioClipsList[] effectsList;
        PlayerMovement playerMove;
        PlayerAnimations playerAnim;

        float startingVolumeRun;
        float characterMovement;

        // Use this for initialization
        void Start()
        {
            playerAnim = GetComponent<PlayerAnimations>();
            playerMove = GetComponent<PlayerMovement>();
            startingVolumeRun = runFoley.volume;

            runFoley.volume = 0;
        }

        //Makes gun sound play
        public void PlayGunSound()
        {
            gunSounds.Play();
        }

        //Makes reload sound play
        public void PlayReloadSound()
        {
            AkReload.Play();
        }

        //Makes a step sound play
        public void Step()
        {
            PlayFootStep();
        }

        //Makes the run sound effect play
        public void RunStep()
        {
            if (playerAnim.isRunning == true)
            {
                runFoley.volume = 0.6f;
            }
            else
            {
                runFoley.volume = 0;
            }
            PlayFootStep();
        }

        //This method is used to control which step sound effect plays. This is done randomly through whichever audio clips are placed
        //in the list
        public void PlayFootStep()
        {
            if (!footStep1.isPlaying) // if footstep 1 isnt playing then find a random clip from within the list, assign it and play it
            {
                int ran = Random.Range(0, footStepClips.Length);
                footStep1.clip = footStepClips[ran];

                footStep1.Play();
            }
            else
            {
                if (!footStep2.isPlaying) // if footstep 2 isnt playing then find a random clip from within the list, assign it and play it
                {
                    int ran2 = Random.Range(0, footStepClips.Length);
                    footStep2.clip = footStepClips[ran2];

                    footStep2.Play();
                }
            }
        }

        //This allows the player to play specific sound effects
        public void PlayEffect(string name)
        {
            AudioClip clip = null;

            for (int i = 0; i < effectsList.Length; i++)
            {
                if (string.Equals(effectsList[i].name, name))
                {
                    clip = effectsList[i].clip;
                    break;
                }
            }

            effectsSource.clip = clip;
            effectsSource.Play();
        }

        [System.Serializable]
        public class AudioClipsList
        {
            public string name;
            public AudioClip clip;
        }
    }
}
