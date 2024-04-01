using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colliderPanel_CD : MonoBehaviour
{
    public GameObject signPanel;
    private bool panelOpen = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")&& panelOpen == false)
        {
            signPanel.SetActive(true);
            panelOpen = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")&& panelOpen == true)
        {
            signPanel.SetActive(false);
            panelOpen = false;
        }
    }



}
