using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpeicalTrafficLightButton : MonoBehaviour
{
    // Start is called before the first frame update
    //public GameObject speiclTrafficLightPanel_1;
    //public GameObject speiclTrafficLightPanel_2;
    //public GameObject speiclTrafficLightPanel_3;

    public GameObject hintPanel; 
    public AudioSource answerSound;
    public AudioSource playerHint;
    public AudioSource playerAsk;

    private int triggerTime = 0;
    private bool playerInTriffic = false;
    private bool talkOver = false;

    public Collider invisibleWall;
    
   

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerHint.PlayDelayed(1.0f);
            Invoke("OpenHintPanel", 7.0f);
            playerInTriffic = true;
        }
    }

    private void Update()
    {
        if (playerInTriffic == true && Input.GetKeyDown(KeyCode.E))
        {
            triggerTime++;
            AskToCross();
            hintPanel.SetActive(false);
        }

        if(talkOver)
        {
            invisibleWall.enabled = false;
        }
    }
    public void AskToCross()
    {
        if (triggerTime == 1)
        {
            Invoke("HideHintPanel", 1.0f);
            playerAsk.PlayDelayed(1.0f);
            Invoke("OpenHintPanel", 3.0f);
        }

        if (triggerTime == 2)
        {
            Invoke("HideHintPanel", 1.0f);
            playerAsk.PlayDelayed(1.0f);
            Invoke("OpenHintPanel", 3.0f);
        }
        if (triggerTime == 3)
        {
            Invoke("HideHintPanel", 1.0f);
            playerAsk.PlayDelayed(1.0f);
            answerSound.PlayDelayed(5.0f);
            talkOver = true;

        }
    }
    private void OpenHintPanel()
    {
        hintPanel.SetActive(true);
    }

    private void HideHintPanel()
    {
        hintPanel.SetActive(false);
    }
  

   
    
}
