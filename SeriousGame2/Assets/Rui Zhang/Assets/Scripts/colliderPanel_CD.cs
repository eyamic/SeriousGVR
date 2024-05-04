using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colliderPanel_CD : MonoBehaviour
{
    public AudioSource thankYou;
    public GameObject signPanel;
    private bool panelOpen = false;
    private bool alreadyPlay = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !panelOpen )
        {
            signPanel.SetActive(true);
            panelOpen = true;

            if (!alreadyPlay)
            {
                thankYou.PlayDelayed(1.0f);
                alreadyPlay = true;
            }
            else
            {
                thankYou.enabled = false;
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && panelOpen)
        {
            signPanel.SetActive(false);
            panelOpen = false;
        }
    }
}
