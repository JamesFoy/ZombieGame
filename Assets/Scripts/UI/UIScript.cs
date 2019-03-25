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
    Weapons weapon;

    [SerializeField]
    GameObject popup;

    [SerializeField]
    public TMP_Text moneyText;

    [SerializeField]
    public TMP_Text priceInfo;

    [SerializeField]
    public TMP_Text ammoText;

    public AudioSource purchaseSound;

    public int score;

    // Use this for initialization
    void Start()
    {
        purchace = GameObject.FindGameObjectWithTag("Purchaseable").GetComponent<Purchace>();
        popup.SetActive(false);
        priceInfo.enabled = false;
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        moneyText.text = "£: " + score;
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
}
