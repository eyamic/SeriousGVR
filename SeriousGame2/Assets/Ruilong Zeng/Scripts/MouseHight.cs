using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseHight : MonoBehaviour
{
    [Header("UI Part")] public RectTransform uiTransform; // Reference to the UI transform
    public GameObject talkCanvas; // Reference to the talk canvas
    public Button closeButton; // Button to close the talk canvas

    [Header("Interaction Part")] public Transform handTransform; // Reference to the hand transform
    public PlayerCamera playerCamera; // Reference to the player camera
    public GameObject clickEffect; // Click effect game object
    public GameObject clickEffect2; // Second click effect game object
    private GameObject highlightedObject; // Currently highlighted object
    private bool canRotateVertically = true; // Flag to control vertical rotation
    private bool haveItem; // Flag indicating if the player has an item
    public AudioSource[] audio; // Array of audio sources

    private void Update()
    {
        closeButton.onClick.AddListener(Close); // Add listener to the close button
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Create a ray from the mouse position

        if (Physics.Raycast(ray, out hit)) // Cast a ray into the scene
        {
            GameObject hitObject = hit.collider.gameObject; // Get the object hit by the ray

            // Ensure the object has an Outline component
            Outline outlineComponent = hitObject.GetComponent<Outline>();
            if (outlineComponent != null)
            {
                // If the hovered object is different from the previously hovered object
                if (hitObject != highlightedObject)
                {
                    ClearHighlight(); // Clear the previous highlight
                    highlightedObject = hitObject;
                    outlineComponent.enabled = true; // Highlight the object
                }

                // Handle interaction with objects that can be picked up
                if (hitObject.CompareTag("CanPickup"))
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        PickUpItem(hitObject);
                    }
                }

                // Handle interaction with objects that can be talked to
                if (hitObject.CompareTag("CanTalk"))
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        talkCanvas.SetActive(true);
                    }
                }
            }
            else
            {
                ClearHighlight(); // Clear the highlight if the object doesn't have an Outline component
            }
        }
        else
        {
            ClearHighlight(); // Clear the highlight if nothing is hit by the ray
            uiTransform.gameObject.SetActive(false); // Hide UI if nothing is hit
        }

        // Handle clicking on the environment to trigger effects
        if (Input.GetMouseButtonDown(0))
        {
            if (haveItem)
            {
                RaycastHit floorHit;
                if (Physics.Raycast(ray, out floorHit))
                {
                    if (floorHit.collider.CompareTag("Floor"))
                    {
                        ClickEffectControl(floorHit.point); // Trigger the click effect
                        audio[1].Play(); // Play the corresponding audio
                    }
                    else if (floorHit.collider.CompareTag("Dangerous"))
                    {
                        ClickEffectControl2(floorHit.point); // Trigger the second click effect
                        audio[0].Play(); // Play the corresponding audio
                    }
                }
            }
        }
    }

// Method to close the talk canvas
    void Close()
    {
        talkCanvas.SetActive(false);
    }

// Method to pick up an item
    private void PickUpItem(GameObject gameObject)
    {
        Collider itemCollider = gameObject.GetComponent<Collider>();
        if (itemCollider != null)
        {
            itemCollider.enabled = false; // Disable collider to prevent interaction with the item
        }

        gameObject.transform.position = handTransform.position;
        gameObject.transform.rotation = handTransform.rotation;
        gameObject.gameObject.transform.SetParent(handTransform);
        haveItem = true; // Set the flag indicating that the player has an item

        playerCamera.SetMoveUpDownEnabled(false); // Disable vertical camera movement
    }

// Method to trigger the click effect
    private void ClickEffectControl(Vector3 position)
    {
        GameObject ripple = Instantiate(clickEffect, position, Quaternion.identity);
        Destroy(ripple, 1.5f); // Destroy the effect after a certain duration
    }

// Method to trigger the second click effect
    private void ClickEffectControl2(Vector3 position)
    {
        GameObject ripple2 = Instantiate(clickEffect2, position, Quaternion.identity);
        Destroy(ripple2, 1.5f); // Destroy the effect after a certain duration
    }

// Method to clear the object highlight
    private void ClearHighlight()
    {
        if (highlightedObject != null)
        {
            Outline outlineComponent = highlightedObject.GetComponent<Outline>();
            if (outlineComponent != null)
            {
                outlineComponent.enabled = false; // Disable the outline
            }

            highlightedObject = null;
        }
    }
}