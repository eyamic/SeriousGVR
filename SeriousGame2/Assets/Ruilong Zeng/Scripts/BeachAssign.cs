using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BeachAssign : MonoBehaviour
{
    public GameObject AssignPanel; // Reference to the assignment panel game object

    private void Awake()
    {
        AssignPanel.SetActive(false); // Set the assignment panel to inactive when the game object is awakened
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // If the object entering the collider is a player
        {
            AssignPanel.SetActive(true); // Activate the assignment panel
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // If the object leaving the collider is a player
        {
            AssignPanel.SetActive(false); // Set the assignment panel to inactive
        }
    }
}
