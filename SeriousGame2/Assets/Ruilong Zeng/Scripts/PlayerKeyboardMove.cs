using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKeyboardMove : MonoBehaviour
{
    public float Movespeed;
    public float GroundDrag;

    [Header("Ground Check")] 
    public float playerHight;
    public LayerMask WhatIsground;
    private bool Isgroung;

    public Transform orientation;
    private float horizontaInput;
    private float verticalInput;
    private Vector3 moveDirection;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        MyInput();
        SpeedControl();
        Isgroung = Physics.Raycast(transform.position, Vector3.down, playerHight * 0.5f + 0.2f, WhatIsground);
        if (Isgroung)
        {
            rb.drag = GroundDrag;
        }
        else
        {
            rb.drag = 0;
        }
    }
    private void FixedUpdate()
    {
        Moveplayer();
    }
    private void MyInput()
    {
        horizontaInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }
    private void Moveplayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontaInput;

        rb.AddForce(Movespeed * moveDirection.normalized * 10f, ForceMode.Force);
    }
    private void SpeedControl()
    {
        Vector3 flatV = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatV.magnitude > Movespeed)
        {
            Vector3 LimitedVel = flatV.normalized * Movespeed;
            rb.velocity = new Vector3(LimitedVel.x, rb.velocity.y, LimitedVel.z);
        }
    }
}