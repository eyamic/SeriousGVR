using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpeicalTrafficLightButton : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject speiclTrafficLightPanel_1;
    public GameObject speiclTrafficLightPanel_2;
    public GameObject speiclTrafficLightPanel_3;
    public GameObject hintPanel; 
    public AudioSource trifficSound;
    private int triggerTime = 0;
    private bool playerInTriffic = false;

   

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTriffic = true;
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
                speiclTrafficLightPanel_1.SetActive(true);
                Invoke("ClosePanel", 2.0f);
                Invoke("OpenHintPanel", 3f);
                Invoke("ClosePanel", 4f);


            }
            else if (triggerTime == 2)
            {
                speiclTrafficLightPanel_2.SetActive(true);
                Invoke("ClosePanel", 2.0f);
                Invoke("OpenHintPanel", 3f);
                Invoke("ClosePanel", 4f);
            }
            else if (triggerTime == 3)
            {
                speiclTrafficLightPanel_3.SetActive(true);
                Invoke("ClosePanel", 2.0f);
                Invoke("PlayDelaySound", 2.5f);
            }
        }
    }

    private void ClosePanel()
    {
        speiclTrafficLightPanel_1.SetActive(false);
        speiclTrafficLightPanel_2.SetActive(false);
        speiclTrafficLightPanel_3.SetActive(false);
        hintPanel.SetActive(false);
    }

    private void OpenHintPanel()
    {
        hintPanel.SetActive(true);
    }
  

    private void PlayDelaySound()
    {
        trifficSound.Play();
    }

   
    
}
