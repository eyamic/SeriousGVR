using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float SensX = 400; // Sensitivity for horizontal rotation
    public float SensY = 400; // Sensitivity for vertical rotation

    public Transform Orientation; // Reference to the orientation transform
    public Transform playerTransform; // Reference to the player's transform

    private float LockXRotaion = 14f; // Locked vertical rotation angle
    private float xRotation; // Current vertical rotation angle
    private float yRotation; // Current horizontal rotation angle

    public bool canRotate = true; // Flag to enable/disable camera rotation
    public bool canMoveDown = true; // Flag to enable/disable downward camera movement

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * SensX; // Get horizontal mouse movement
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * SensY; // Get vertical mouse movement

        yRotation += mouseX; // Update horizontal rotation
        if (canMoveDown)
        {
            xRotation -= mouseY; // Update vertical rotation
            xRotation = Mathf.Clamp(xRotation, -5f, 20f); // Clamp vertical rotation angle
        }
        else
        {
            xRotation = LockXRotaion; // Lock vertical rotation angle
        }

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0); // Set camera rotation
        Orientation.rotation = Quaternion.Euler(0, yRotation, 0); // Set orientation rotation

        playerTransform.rotation = Quaternion.Euler(0, yRotation, 0); // Set player rotation
    }

    // Method to enable/disable camera rotation
    public void SetRotationEnabled(bool enabled)
    {
        canRotate = enabled;
    }

    // Method to enable/disable downward camera movement
    public void SetMoveUpDownEnabled(bool enabled)
    {
        canMoveDown = enabled;
    }
}