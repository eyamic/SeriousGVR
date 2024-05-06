using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoctorDialogue : MonoBehaviour
{
    public AudioSource player_dialogue1;
    public AudioSource doctor_dialogue1;
    public AudioSource player_dialogue2;
    public AudioSource doctor_dialogue2;
    public AudioSource thankYou;

    public GameObject hintPanel;
    public GameObject hintPanel2;
    public GameObject hintPanel3;

    private bool inRange;
    private bool dialogue1Over;
    private bool dialogue2Over;
    public bool talkOver;
   
    private int numOfTalk;

    // Start is called before the first frame update
    void Start()
    {
        inRange = false;
        dialogue1Over = false;
        dialogue2Over = false;
        talkOver = false;
        numOfTalk = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.E))
        {
            numOfTalk++;
            BeginTalk();
            Debug.Log("+1");
            hintPanel.SetActive(false);      
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = true;
            hintPanel.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        hintPanel.SetActive(false);
        hintPanel2.SetActive(false);
        hintPanel3.SetActive(false);
    }

    private void BeginTalk()
    {
        if (numOfTalk ==1 && inRange)
        {
            Debug.Log("Begin");
            player_dialogue1.Play();
            doctor_dialogue1.PlayDelayed(5.0f);
            dialogue1Over = true;
            Invoke("ShowPanel", 18.0f);
            Invoke("HidePanel", 20.0f);
        }
        if (dialogue1Over && numOfTalk ==2 )
        {
            player_dialogue2.Play();
            doctor_dialogue2.PlayDelayed(3.0f);
            dialogue2Over = true;
            Invoke("ShowPanel", 9.0f);
            Invoke("HidePanel", 10.0f);

        }
        if (dialogue2Over && numOfTalk ==3)
        {
            thankYou.Play();
            talkOver = true;
        }

        if (talkOver)
        {
            hintPanel3.SetActive(true);
            Invoke("HidePanel", 2.0f);
        }
    }
    
    private void ShowPanel()
    {
        hintPanel2.SetActive(true);
    }
    private void HidePanel()
    {
        hintPanel2.SetActive(false);
        hintPanel3.SetActive(false);
    }
 
}

