using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float SensX = 400;
    public float SensY = 400;

    public Transform Orientation;
    public Transform playerTransform;

    private float LockXRotaion = 14f;
    private float xRotation;
    private float yRotation;
    
    public bool canRotate = true;
    public bool canMoveDown = true;
    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * SensX;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * SensY;

        yRotation += mouseX;
        if (canMoveDown)
        {
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -5f, 20f);
        }
        else
        {
            xRotation = LockXRotaion;
        }
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        Orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        
        playerTransform.rotation=Quaternion.Euler(0,yRotation,0);
    }
    public void SetRotationEnabled(bool enabled)
    {
        canRotate = enabled;
    }

    public void SetMoveUpDownEnabled(bool enabled)
    {
        canMoveDown = enabled;
    }
}