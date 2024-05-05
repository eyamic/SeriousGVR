using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class TalkBus : MonoBehaviour
{
     public AudioSource busArrive;
    public AudioSource busPass;
    public AudioSource driverAnswer;
    public AudioSource driverAnswer_ok;
    public AudioSource playerAsk;
    public AudioSource playerThankU;

    public GameObject hintPanel;
    public GameObject hintPanel2;

    public bool inRange;
    public bool talkOver;
    public int numOfTalk = 0;

    private PlayerControls controls; // 控制器

    void Awake()
    {
        controls = new PlayerControls(); // 实例化 Input Actions
    }

    void OnEnable()
    {
        controls.Enable();
        controls.Gameplay.Bus.performed += HandleTalkControl;
    }

    void OnDisable()
    {
        controls.Gameplay.Bus.performed -= HandleTalkControl;
        controls.Disable();
    }

    private void HandleTalkControl(InputAction.CallbackContext context)
    {
        if (inRange)
        {
            numOfTalk++;
            TalkToBus();
            hintPanel.SetActive(false);
        }
    }

    public void Start()
    {
        inRange = false;
        talkOver = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !inRange)
        {
            inRange = true;
            hintPanel.SetActive(true);
        }
    }

    public void TalkToBus()
    {
        if (numOfTalk == 1)
        {
            playerAsk.PlayDelayed(1.0f);
            driverAnswer.PlayDelayed(4.5f);
            busPass.PlayDelayed(4.0f);
            Invoke("ShowHintPanel2", 6f);
        }
        else if (numOfTalk == 2)
        {
            Invoke("HideHintPanel2", 1.0f);
            playerAsk.PlayDelayed(2.0f);
            Invoke("ShowHintPanel2", 5f);
        }
        else if (numOfTalk == 3)
        {
            Invoke("HideHintPanel2", 1f);
            playerAsk.PlayDelayed(2.0f);
            driverAnswer_ok.PlayDelayed(4.5f);
            playerThankU.PlayDelayed(7.0f);
            talkOver = true;
        }
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
