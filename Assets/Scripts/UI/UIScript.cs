using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    // Update is called once per frame
    void Update()
    {
        waveInfo.text = "Wave " + wave.waveName + " Completed!!";
        churchHealth.text = "Church Health: " + churchInfo.churchHealth;
        moneyText.text = "£: " + score;
        grenadeText.text = " " + (weapon.MaxGrenades - playerShoot.grenadesThrown);
        ammoText.text = " " + (weapon.MaxShots - playerShoot.shotsDone);
    }

    public void HideDisplayPopup()
    {
        popup.SetActive(false);
        priceInfo.enabled = false;
    }

    public void DisplayPopup()
    {
        if (score >= purchace.price)
        {
            popup.SetActive(true);
        }        

        priceInfo.enabled = true;
        priceInfo.text = "This item costs £" + purchace.price + "," + " You currently have: £" + score + "." + " Do you wish to purchase this item?";
    }

    IEnumerator DisplayWave()
    {
        waveInfo.enabled = true;
        yield return new WaitForSeconds(5f);
        waveInfo.enabled = false;
    }

    public void WaveInfo()
    {
        waveCompleteSound.Play();
        StartCoroutine(DisplayWave());
    }
}
