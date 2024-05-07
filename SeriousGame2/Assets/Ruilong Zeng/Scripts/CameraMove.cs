using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform CameraPosition; // Reference to the camera position transform

    private void Update()
    {
        transform.position = CameraPosition.position; // Update the position of this object to match the camera position
    }
}
