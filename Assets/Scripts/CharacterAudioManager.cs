using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAudioManager : MonoBehaviour {

    public AudioSource gunSounds;
    public AudioSource AkReload;
    //public AudioSource runFoley;

    public float footStepTimer;
    public float walkThreshold;
    //public float runThreshold;
    public AudioSource footStep1;
    public AudioSource footStep2;
    public AudioClip[] footStepClips;
    public AudioSource effectsSource;
    public AudioClipsList[] effectsList;
    PlayerMovement player;

    float startingVolumeRun;
    float characterMovement;

	// Use this for initialization
	void Start ()
    {
        player = GetComponent<PlayerMovement>();
        //startingVolumeRun = runFoley.volume;

        //runFoley.volume = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        float targetThreshold = 0.6f;
        if (player.moving == true)
        {
            footStepTimer += Time.deltaTime;

            if (footStepTimer > targetThreshold)
            {
                PlayFootStep(); // calls the play footstep function
                footStepTimer = 0;
            }
        }
        else
        {
            footStep1.Stop();
            footStep2.Stop();
        }
	}

    public void PlayGunSound()
    {
        gunSounds.Play();
    }

    public void PlayReloadSound()
    {
        AkReload.Play();
    }

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
