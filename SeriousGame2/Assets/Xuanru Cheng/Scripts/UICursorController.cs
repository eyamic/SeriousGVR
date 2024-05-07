using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UICursorController : MonoBehaviour
{
    public RectTransform cursorRectTransform; // Public reference to the RectTransform of the UI cursor, which will be moved.
    public float cursorSpeed = 100.0f; // Speed at which the cursor moves.

    private PlayerControls controls; // Variable to hold the custom PlayerControls object that manages input.
    private Vector2 navigationInput; // Vector2 to store the current navigation input from the D-pad.



    private void Awake() {
        controls = new PlayerControls(); // Instantiate the PlayerControls object upon the script's initialization.
        // Subscribe to the Navigate action's performed event to capture continuous input values.
        controls.UI.Navigate.performed += ctx => navigationInput = ctx.ReadValue<Vector2>();
        // Subscribe to the Navigate action's canceled event to reset the navigation input when there is no input.
        controls.UI.Navigate.canceled += ctx => navigationInput = Vector2.zero;
    }

    private void Update() {
        // Calculate the cursor's movement based on the navigation input, cursor speed, and frame time.
        Vector2 movement = navigationInput * cursorSpeed * Time.deltaTime;
        // Calculate the new position of the cursor by adding the movement vector to the current anchored position.
        Vector2 newPos = cursorRectTransform.anchoredPosition + movement;
        // Clamp the x position of the cursor to prevent it from moving off-screen horizontally.
        newPos.x = Mathf.Clamp(newPos.x, -Screen.width / 2, Screen.width / 2);
        // Clamp the y position of the cursor to prevent it from moving off-screen vertically.
        newPos.y = Mathf.Clamp(newPos.y, -Screen.height / 2, Screen.height / 2);

        // Set the cursor's anchored position to the newly calculated position.
        cursorRectTransform.anchoredPosition = newPos;
    }
    private void OnEnable()
    {
        controls.Enable();// Enable the PlayerControls when the GameObject becomes active.
    }

    private void OnDisable()
    {
        controls.Disable();
    }
}