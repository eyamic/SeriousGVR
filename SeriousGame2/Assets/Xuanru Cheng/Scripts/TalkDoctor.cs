using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class TalkDoctor : MonoBehaviour
{
    public AudioSource player_dialogue1, doctor_dialogue1, player_dialogue2, doctor_dialogue2, thankYou;
    public GameObject hintPanel, hintPanel2, hintPanel3;
    
    private bool inRange, dialogue1Over, dialogue2Over, talkOver;
    private int numOfTalk;

    private PlayerControls controls; // 控制器

    void Awake()
    {
        controls = new PlayerControls(); // 实例化 Input Actions
    }

    void OnEnable()
    {
        controls.Enable();
        controls.Gameplay.Doctor.performed += HandleTalkControl;
    }

    void OnDisable()
    {
        controls.Gameplay.Doctor.performed -= HandleTalkControl;
        controls.Disable();
    }

    private void HandleTalkControl(InputAction.CallbackContext context)
    {
        if (inRange)
        {
            numOfTalk++;
            BeginTalk();
            Debug.Log("Talk increment");
            hintPanel.SetActive(false);      
        }
    }

    void Start()
    {
        inRange = false;
        dialogue1Over = false;
        dialogue2Over = false;
        talkOver = false;
        numOfTalk = 0;
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
        inRange = false;
        hintPanel.SetActive(false);
        hintPanel2.SetActive(false);
        hintPanel3.SetActive(false);
    }

    private void BeginTalk()
    {
        switch(numOfTalk)
        {
            case 1:
                player_dialogue1.Play();
                doctor_dialogue1.PlayDelayed(5.0f);
                Invoke("ShowPanel", 18.0f);
                Invoke("HidePanel", 20.0f);
                dialogue1Over = true;
                break;
            case 2:
                if (dialogue1Over)
                {
                    player_dialogue2.Play();
                    doctor_dialogue2.PlayDelayed(3.0f);
                    Invoke("ShowPanel", 9.0f);
                    Invoke("HidePanel", 10.0f);
                    dialogue2Over = true;
                }
                break;
            case 3:
                if (dialogue2Over)
                {
                    thankYou.Play();
                    talkOver = true;
                    hintPanel3.SetActive(true);
                }
                break;
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
}