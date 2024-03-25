using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrafficLightButton : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI displayText;
    public AudioSource trifficSound;
    private bool playerInTriffic = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTriffic = true;
            //displayText.text = " Press E to active the Traffic Light";
            Debug.Log("Press E to active the Traffic Light");
        }
    }

    private void Update()
    {
        if (playerInTriffic && Input.GetKeyDown(KeyCode.E))
        {
            //Invoke("PlayDelayerSound", 5f);
            Debug.Log("PlaySound");
        }
    }
    private void PlayDelaySound()
    {
        trifficSound.Play();
    }

    
}
