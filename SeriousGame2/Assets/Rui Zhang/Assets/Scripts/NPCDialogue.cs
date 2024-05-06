using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    public AudioSource player_dialogue1;
    public AudioSource npc_dialogue1;
    public AudioSource player_dialogue2;
    public AudioSource npc_dialogue2;

    public GameObject hintPanel;
    public GameObject hintPanel2;
    public GameObject hintPanel3;

    private bool inRange;
    private bool dialogue1Over;
    public bool talkOver;

    private int numOfTalk;

    void Start()
    {
        inRange = false;
        dialogue1Over = false;
        talkOver = false;
        numOfTalk = 0;
    }

    private void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.E))
        {
            numOfTalk++;
            hintPanel.SetActive(false);
            BeginTalkToNpc();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = true;
            hintPanel.SetActive(true);
        }
        if (other.CompareTag("Player")&& talkOver)
        {
            hintPanel3.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        inRange = false;
        hintPanel.SetActive(false);
        hintPanel2.SetActive(false);
        hintPanel3.SetActive(false);
    }
    private void BeginTalkToNpc()
    {
        if (numOfTalk ==1 && inRange)
        {
            player_dialogue1.Play();
            npc_dialogue1.PlayDelayed(4.0f);
            dialogue1Over = true;
            Invoke("ShowPanel", 6.0f);
            Invoke("HidePanel", 8.0f);

        }
        if (numOfTalk ==2 && dialogue1Over)
        {
            player_dialogue2.Play();
            npc_dialogue2.PlayDelayed(5.5f);
            talkOver = true;
        }
        if (talkOver&& inRange)
        {
            Invoke("ShowPanel2", 9.5f);
        }
    }
    private void ShowPanel()
    {
        hintPanel2.SetActive(true);
    }
    private void HidePanel()
    {
        hintPanel2.SetActive(false);
    }
    private void ShowPanel2()
    {
        hintPanel3.SetActive(true);
    }


}
