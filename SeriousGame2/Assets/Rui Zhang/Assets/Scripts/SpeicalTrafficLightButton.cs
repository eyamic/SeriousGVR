using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpeicalTrafficLightButton : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI displayText;
    public AudioSource trifficSound;
    private int triggerTime = 0;
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
        if (playerInTriffic == true && Input.GetKeyDown(KeyCode.E))
        {
            triggerTime++;
            if (triggerTime == 1)
            {
                Debug.Log("first");
            }
            else if (triggerTime == 2)
            {
                Debug.Log("second");
            }
            else if (triggerTime == 3)
            {
                //Invoke("PlayerDelaySound", 5.0f);
                Debug.Log("PlaySound");
            }
        }
    }
    private void PlayDelaySound()
    {
        trifficSound.Play();
    }

    
}
