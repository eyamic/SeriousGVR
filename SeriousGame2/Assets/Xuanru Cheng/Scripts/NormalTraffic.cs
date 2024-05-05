using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class NormalTraffic : MonoBehaviour
{
    public AudioSource TrifficSound;
    public GameObject trifficPanel;
    private bool playerInRange_Traffic = false;
    private bool trifficP = false;
    public AudioSource TeachTriffic;
    private PlayerControls controls; // 引用 PlayerControls

    private void Awake()
    {
        controls = new PlayerControls();
    }

    private void OnEnable()
    {
        controls.Enable();
        controls.Gameplay.Traffic.performed += HandleTrafficControl;
    }

    private void OnDisable()
    {
        controls.Gameplay.Traffic.performed -= HandleTrafficControl;
        controls.Disable();
    }

    private void HandleTrafficControl(InputAction.CallbackContext context)
    {
        if (playerInRange_Traffic)
        {
            Debug.Log("play sound");
            TrifficSound.PlayDelayed(5.0f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && trifficP == false)
        {
            playerInRange_Traffic = true;
            trifficPanel.SetActive(true);
            trifficP = true;
            TeachTriffic.PlayDelayed(1.0f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && trifficP == true)
        {
            playerInRange_Traffic = false;
            trifficPanel.SetActive(false);
            trifficP = false;
        }
    }
}