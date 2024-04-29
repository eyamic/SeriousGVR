using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkArea : MonoBehaviour
{
    public AudioSource busArrive;
    public AudioSource busPass;
    public AudioSource driverAnswer;
    public AudioSource driverAnswer_ok;

    public GameObject hintPanel;
    public GameObject askPanel;
    public GameObject hintPanel2;

    public bool inRange;
    public bool talkOver;
    public int numOfTalk = 0;


    public void Start()
    {
        inRange = false;
        talkOver = false;
    }
    public void Update()
    {
         if(Input.GetKeyDown(KeyCode.E) && inRange)
        {  
            numOfTalk++;   
            TalkToBus();
            hintPanel.SetActive(false);
        }
  

    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !inRange)
        {
            inRange = true;
            hintPanel.SetActive(true);
        }
    }


    public void TalkToBus()
    {
        // busArrive.PlayDelayed(1.0f);
        if (numOfTalk == 1)
        {
            Invoke("ShowAskPanel", 1.5f);
            driverAnswer.PlayDelayed(2.5f);
            Invoke("HideAskPanel", 4.0f);

            busPass.PlayDelayed(4.0f);

            Invoke("ShowHintPanel2",5f);
        }

        if (numOfTalk == 2)
        {
            Invoke("HideHintPanel2",1.0f);
            Invoke("ShowAskPanel", 2.0f);
            Invoke("HideAskPanel", 3.0f);
            Invoke("ShowHintPanel2", 4f);

        }

        if (numOfTalk == 3 )
        {
            Invoke("HideHintPanel2",1f);
            Invoke("ShowAskPanel", 2f);
            driverAnswer_ok.PlayDelayed(3.0f);
            Invoke("HideAskPanel", 5.0f);
            talkOver = true;

        }
    }
    

    public void ShowAskPanel()
    {
        askPanel.SetActive(true);
    }
    

    public void HideAskPanel()
    {
        askPanel.SetActive(false);
    }

    public void ShowHintPanel2()
    {
        hintPanel2.SetActive(true);

    }

    public void HideHintPanel2()
    {
        hintPanel2.SetActive(false);
    }
}
