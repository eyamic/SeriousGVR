using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class CameraXuanru : MonoBehaviour
{
    public float SensX = 400;
    public float SensY = 400;

    public Transform Orientation;
    public Transform playerTransform;

    private float xRotation = 0f;
    private float yRotation = 0f;

    private PlayerControls controls;

    private void Awake() {
        controls = new PlayerControls();
        controls.Gameplay.Rotate.performed += ctx => {
            Vector2 input = ctx.ReadValue<Vector2>();
            xRotation = Mathf.Clamp(xRotation - input.y * SensY * Time.deltaTime, -90f, 90f);  // Limit vertical rotation
            yRotation += input.x * SensX * Time.deltaTime;  // Horizontal rotation
        };
    }

    private void Update()
    {
        Quaternion horizontalRotation = Quaternion.Euler(0, yRotation, 0);
        Quaternion verticalRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, horizontalRotation * verticalRotation, Time.deltaTime * 10);
    }


    private void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    private void OnDisable()
    {
        controls.Gameplay.Disable();
    }
}