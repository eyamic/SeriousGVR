using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Pickupphone : MonoBehaviour
{
    public GameObject panel1;
    public GameObject panel2;
    public GameObject panel3;

    public AudioSource phone;
    public AudioSource npc1;
    public AudioSource player1;
    public AudioSource npc2;

    private bool inRange = false;
    private int numOfTalk = 0;
    private bool talkOver = false;
    private bool dialogue1Over= false;
    private PlayerControls controls; // 控制器
    // Update is called once per frame
    void Awake()
    {
        controls = new PlayerControls(); // 实例化 Input Actions
    }

    void OnEnable()
    {
        controls.Enable();
        controls.Gameplay.PickupPhone.performed += HandlePickupControl;
    }

    void OnDisable()
    {
        controls.Gameplay.PickupPhone.performed -= HandlePickupControl;
        controls.Disable();
    }
    private void HandlePickupControl(InputAction.CallbackContext context)
    {
        if (inRange)
        {
            numOfTalk++;
            panel1.SetActive(false);
            BeginPhone();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        phone.Play();
        inRange = true;
        Invoke("ShowPanel2", 2.0f);
    }

    private void OnTriggerExit(Collider other)
    {
        inRange = false;
        panel3.SetActive(false);
    }

    private void BeginPhone()
    {
        if (numOfTalk == 1 && inRange)
        {
            phone.enabled = false;
            npc1.Play();
            player1.PlayDelayed(4.0f);
            dialogue1Over = true;
            Invoke("ShowPanel", 5.0f);
            Invoke("HidePanel", 6.0f);
        }
        if (numOfTalk == 2 && inRange && dialogue1Over)
        {
            npc2.Play();
            talkOver = true;
        }
        if (talkOver && inRange)
        {
            panel3.SetActive(true);
        }
    }

    private void ShowPanel()
    {
        panel2.SetActive(true);
    }
    private void HidePanel()
    {
        panel2.SetActive(false);
    }
    private void ShowPanel2()
    {
        panel1.SetActive(true);
    }
}