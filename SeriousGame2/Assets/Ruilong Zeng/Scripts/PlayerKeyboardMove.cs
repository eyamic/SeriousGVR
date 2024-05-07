using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKeyboardMove : MonoBehaviour
{
    public float MoveSpeed; // Movement speed
    public float GroundDrag; // Ground drag coefficient

    [Header("Ground Check")] public float playerHeight; // Height of the player
    public LayerMask WhatIsGround; // Layer mask for ground detection
    private bool IsGrounded; // Flag indicating whether the player is grounded

    public Transform orientation; // Reference to the orientation transform
    private float horizontalInput; // Horizontal input
    private float verticalInput; // Vertical input
    private Vector3 moveDirection; // Movement direction
    private Rigidbody rb; // Reference to the Rigidbody component
    public AudioSource[] audio; // Array of audio sources

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody component
        rb.freezeRotation = true; // Freeze rotation
    }

    private void Update()
    {
        MyInput(); // Handle player input
        SpeedControl(); // Control movement speed
        IsGrounded =
            Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f,
                WhatIsGround); // Check if the player is grounded
        if (IsGrounded)
        {
            rb.drag = GroundDrag; // Apply ground drag
        }
        else
        {
            rb.drag = 0; // Remove ground drag
        }
    }

    private void FixedUpdate()
    {
        MovePlayer(); // Move the player
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal"); // Get horizontal input
        verticalInput = Input.GetAxisRaw("Vertical"); // Get vertical input
    }

    private void MovePlayer()
    {
        moveDirection =
            orientation.forward * verticalInput + orientation.right * horizontalInput; // Calculate movement direction

        rb.AddForce(MoveSpeed * moveDirection.normalized * 10f, ForceMode.Force); // Apply movement force
        if (verticalInput != 0)
        {
            if (!audio[0].isPlaying)
            {
                audio[0].Play(); // Play footstep audio if moving
            }
        }
        else
        {
            audio[0].Stop(); // Stop footstep audio if not moving
        }
    }

    private void SpeedControl()
    {
        Vector3 flatVelocity =
            new Vector3(rb.velocity.x, 0f, rb.velocity.z); // Get the horizontal component of velocity

        if (flatVelocity.magnitude > MoveSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * MoveSpeed; // Limit the magnitude of velocity
            rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z); // Update the velocity
        }
    }
}