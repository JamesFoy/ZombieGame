using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Player;
using Items;
using Enemy;
using Events;

//Author - James Foy
//This script is used to control all of the behaviour for the UI in the game. It controls all values that are displayed
//on screen, including ammo count, text information, popups and amount of money.

namespace UI
{
    public class UIScript : MonoBehaviour
    {
        [SerializeField]
        PlayerShooting playerShoot;

        Purchace purchace;

        [SerializeField]
        Church churchInfo;

        [SerializeField]
        Weapons weapon;

        [SerializeField]
        GameObject popup;

        [SerializeField]
        WaveSpawner wave;

        [SerializeField]
        public TMP_Text moneyText;

        [SerializeField]
        public TMP_Text churchHealth;

        [SerializeField]
        public TMP_Text priceInfo;

        [SerializeField]
        public TMP_Text ammoText;

        [SerializeField]
        public TMP_Text grenadeText;

        [SerializeField]
        public TMP_Text waveInfo;

        public AudioSource purchaseSound;
        public AudioSource waveCompleteSound;

        public int score;

        // Use this for initialization
        void Start()
        {
            purchace = GameObject.FindGameObjectWithTag("Purchaseable").GetComponent<Purchace>();
            HideDisplayPopup();
            score = 0;
            waveInfo.enabled = false;
        }

        //Makes the event manager listen to a new event
        void OnEnable()
        {
            EventManager.StartListening("WaveText", UpdateWaveInfo);
        }

        //Stops the event manager from listening to the event
        void OnDisable()
        {
            EventManager.StartListening("WaveText", UpdateWaveInfo);
        }

        //This is the event that is used to display the wave complete information
        public void UpdateWaveInfo()
        {
            WaveInfo();
            waveInfo.text = "Wave " + wave.waveName + " Completed!!";
        }

        // Update is called once per frame
        void Update()
        {
            churchHealth.text = "Church Health: " + churchInfo.churchHealth;
            moneyText.text = "£: " + score;
            grenadeText.text = " " + (weapon.MaxGrenades - playerShoot.grenadesThrown);
            ammoText.text = " " + (weapon.MaxShots - playerShoot.shotsDone);
        }

        //This method is used to hide the purchasing popup
        public void HideDisplayPopup()
        {
            popup.SetActive(false);
            priceInfo.enabled = false;
        }

        //This method is used to display the purchasing popup
        public void DisplayPopup()
        {
            if (score >= purchace.price)
            {
                popup.SetActive(true);
            }

            priceInfo.enabled = true;
            priceInfo.text = "This item costs £" + purchace.price + "," + " You currently have: £" + score + "." + " Do you wish to purchase this item?";
        }

        //This will display the wave info for a certain amount of time
        IEnumerator DisplayWave()
        {
            waveInfo.enabled = true;
            yield return new WaitForSeconds(5f);
            waveInfo.enabled = false;
        }

        //This will be used to display the WaveInformation
        public void WaveInfo()
        {
            waveCompleteSound.Play();
            StartCoroutine(DisplayWave());
        }
    }
}
