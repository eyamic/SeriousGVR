using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkArea : MonoBehaviour
{
    public AudioSource busArrive;
    
    public AudioSource busPass;
    public AudioSource busArrivePark;
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
         if(Input.GetKeyDown(KeyCode.E)&& inRange)
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

            Invoke("ShowHintPanel2",1f);
            driverAnswer.PlayDelayed(2.0f);
            busPass.Play();
        }

        if (numOfTalk == 2)
        {
            Invoke("ShowHintPanel2",1.0f);
            Invoke("HideHintPanel2", 2.0f);
            busPass.Play();
        }

        if (numOfTalk == 3 )
        {
            Invoke("ShowHintPanel2",1f);
            Invoke("HideHintPanel2", 3f);
            talkOver = true;
            driverAnswer_ok.PlayDelayed(3.0f);
        }
    }
    

    // public void HideHintPanel()
    // {
    //     hintPanel.SetActive(false);
    // }

    public void ShowAskPanel()
    {

    }
    

    public void HideAskPanel()
    {

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
