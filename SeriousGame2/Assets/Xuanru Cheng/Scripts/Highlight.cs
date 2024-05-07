using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
public class Highlight : MonoBehaviour
{
    public PlayerControls controls; // Holds a reference to the custom input actions defined in the PlayerControls.
    private Vector2 move; // Stores the movement vector input from the player.
    private Vector2 rotationInput; // Stores the rotation input from the player.
    public RectTransform cursorRectTransform; // A reference to the RectTransform of the cursor, used for raycasting.
    public Transform cameraTransform; // Reference to the camera's transform, used for directional calculations.
    public Transform handTransform; // Reference to the transform representing the player's hand, not used in the provided snippet.
    public CameraXuanru playerCamera; // Reference to a custom camera control script, not used in the provided snippet.

    public GameObject talkCanvas; // UI canvas for dialogues or interactions.
    public GameObject ClickEffect; // The particle effect prefab to instantiate on normal surfaces.
    public GameObject ClickEffect2; // The particle effect prefab to instantiate on dangerous surfaces.

    public Button closeButton; // A UI button to close the talk canvas.

    private GameObject highlightedObject; // Stores the currently highlighted object.
    private bool canRotateVertically = true; // Flag to control vertical rotation, not used in the provided snippet.
    private bool haveItem; // Flag indicating if the player has picked up an item, not used in the provided snippet.
    private bool canTriggerEffect = false; // Flag to indicate whether a particle effect can be triggered.

    public AudioSource[] audio; // Array of AudioSources to play different sounds.
   // private Vector2 rotationInput;
    private void Awake()
    {
        controls = new PlayerControls();// Initializes the PlayerControls.
        controls.Gameplay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();// Sets up movement input handling.
        controls.Gameplay.Move.canceled += ctx => move = Vector2.zero;// Resets movement input when the action is canceled.
       // controls.Gameplay.Rotate.performed += ctx => rotationInput = ctx.ReadValue<Vector2>();
       // controls.Gameplay.Rotate.canceled += ctx => rotationInput = Vector2.zero;
        controls.Gameplay.Pickup.performed += ctx => HandleButtonWestPress();
        controls.Gameplay.Effect.performed += ctx => HandleButtonSouthPress();

        closeButton.onClick.AddListener(() => talkCanvas.SetActive(false));
    }

  
    private void Update()
    {
        // 将移动输入转换为基于相机的方向 Translates movement input into world coordinates based on camera orientation, ignoring vertical movement.
        Vector3 forward = cameraTransform.forward;
        forward.y = 0; // 保持在水平平面上
        Vector3 right = cameraTransform.right;
        right.y = 0; // 同样保持在水平平面上

        Vector3 direction = (forward * move.y + right * move.x).normalized; // 根据相机方向计算新的方向向量
        transform.Translate(direction * Time.deltaTime * 10f, Space.World); // 使用世界坐标系移动
       
    }
    
    private void LateUpdate()
    {
        RaycastHighlight();
    }
    private void RaycastHighlight()
    {
        // Performs a raycast from the cursor's position to detect interactable objects.
        Ray ray = Camera.main.ScreenPointToRay(cursorRectTransform.position);// Converts cursor position to a ray.
        RaycastHit hit;
        

        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObject = hit.collider.gameObject;
            Outline outline = hitObject.GetComponent<Outline>();
            if (outline != null && hitObject != highlightedObject)
            {
                ClearHighlight();
                highlightedObject = hitObject;
                outline.enabled = true;
            }
        }
        else
        {
            ClearHighlight();
        }
    }

    private void ProcessHighlight(GameObject hitObject)
    {// A helper method to process highlighting of objects.
        Outline outline = hitObject.GetComponent<Outline>();
        if (outline != null)
        {
            if (highlightedObject != hitObject)// Checks if it is a new object to highlight.
            {
                ClearHighlight();
                highlightedObject = hitObject;
                outline.enabled = true;// Enables the outline.
            }
        }
        else
        {
            ClearHighlight();
        }
    }

    private void HandleButtonWestPress()
    { // Handles the logic when the Pickup button is pressed.
        if (highlightedObject != null && highlightedObject.CompareTag("CanPickup"))
        {
            highlightedObject.SetActive(false);
            Debug.Log("Deactivated: " + highlightedObject.name);
            canTriggerEffect = true;  // 设置可以触发粒子效果
        }
    }

    private void HandleButtonSouthPress()
    {// Handles the logic when the Effect button is pressed.
        if (canTriggerEffect)
        {
            Ray ray = Camera.main.ScreenPointToRay(cursorRectTransform.position);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            {
                if (hit.collider.CompareTag("Floor"))
                {
                    GameObject effect = Instantiate(ClickEffect, hit.point, Quaternion.identity);
                    PlayEffect(effect);
                    Debug.Log("Floor effect triggered at " + hit.point);
                    TriggerVibration(0.25f, 0.55f);  // 较低的震动频率 // Lower vibration frequency
                    audio[1].Play();
                }
                else if (hit.collider.CompareTag("Dangerous"))
                {
                    GameObject effect = Instantiate(ClickEffect2, hit.point, Quaternion.identity);
                    PlayEffect(effect);
                    Debug.Log("Dangerous effect triggered at " + hit.point);
                    TriggerVibration(0.75f, 1.25f);  // 较高的震动频率 // Higher vibration frequency
                    audio[0].Play();
                }
                else
                {
                    Debug.Log("No appropriate tag found for effect triggering.");
                }
            }
            else
            {
                Debug.Log("No hit: Cannot place effect accurately.");
            }
        }
    }

    // Method to play a particle effect and schedule its automatic destruction.
     void PlayEffect(GameObject effect)
    {
        var particleSystem = effect.GetComponent<ParticleSystem>();
        if (particleSystem != null && !particleSystem.isPlaying)
        {
            particleSystem.Play();
            Destroy(effect, particleSystem.main.duration);  // Schedules the destruction of the effect object based on the duration of the particles.
        }
        else
        {
            Debug.Log("Failed to play effect: Particle system not found or already playing.");
        }
      //  canTriggerEffect = false;  // 重置触发标志，避免重复触发
    }
    private void ClearHighlight()
    {
        if (highlightedObject != null)
        {
            Outline outline = highlightedObject.GetComponent<Outline>();
            if (outline != null)
                outline.enabled = false;

            highlightedObject = null;
        }
    }

    private void TriggerVibration(float lowFrequency, float highFrequency)
    {
        // Triggers haptic feedback on the gamepad.
        if (Gamepad.current != null)
        {
            Gamepad.current.SetMotorSpeeds(lowFrequency, highFrequency);// Sets the vibration speeds.
            StartCoroutine(StopVibrationAfterDelay(0.5f));
        }
    }

    private IEnumerator StopVibrationAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);// Waits for the specified delay.
        if (Gamepad.current != null)
        {
            Gamepad.current.SetMotorSpeeds(0, 0);
        }
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