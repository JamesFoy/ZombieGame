using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Church : MonoBehaviour {

    [SerializeField]
    public int churchHealth;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<AIScript>().DiePlease();
            churchHealth--;

            if (churchHealth <= 0)
            {
                //GAME OVER!!
            }
        }
    }
}
