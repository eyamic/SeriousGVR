using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
public class SpecialTraffic : MonoBehaviour
{
    public GameObject hintPanel; 
    public AudioSource answerSound;
    public AudioSource playerHint;
    public AudioSource playerAsk;

    private int triggerTime = 0;
    private bool playerInTriffic = false;
    private bool talkOver = false;
    public Collider invisibleWall;

    private PlayerControls controls; // 控制器

    void Awake()
    {
        controls = new PlayerControls();
    }

    void OnEnable()
    {
        controls.Enable();
        controls.Gameplay.Traffic.performed += HandleTrafficControl;
    }

    void OnDisable()
    {
        controls.Gameplay.Traffic.performed -= HandleTrafficControl;
        controls.Disable();
    }

    private void HandleTrafficControl(InputAction.CallbackContext context)
    {
        if (playerInTriffic)
        {
            triggerTime++;
            AskToCross();
            hintPanel.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerHint.PlayDelayed(1.0f);
            Invoke("OpenHintPanel", 7.0f);
            playerInTriffic = true;
        }
    }

    public void AskToCross()
    {
        if (triggerTime == 1 || triggerTime == 2)
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

    void Update()
    {
        if(talkOver)
        {
            invisibleWall.enabled = false;
        }
    }
}