using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIScript : MonoBehaviour {

    [SerializeField]
    PlayerShooting playerShoot;

    [SerializeField]
    Weapons weapon;

    [SerializeField]
    public TMP_Text moneyText;

    [SerializeField]
    public TMP_Text ammoText;

    public int score;

    // Use this for initialization
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        moneyText.text = "£: " + score;
        ammoText.text = " " + (weapon.MaxShots - playerShoot.shotsDone);
	}
}
