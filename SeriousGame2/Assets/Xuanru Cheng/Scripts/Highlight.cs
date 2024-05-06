using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
public class Highlight : MonoBehaviour
{
     public PlayerControls controls;
     private Vector2 move;
     private Vector2 rotationInput;
    public RectTransform cursorRectTransform;
    public Transform cameraTransform;
    public Transform handTransform;
    public CameraXuanru playerCamera;

    public GameObject talkCanvas;
    public GameObject ClickEffect;
    public GameObject ClickEffect2;

    public Button closeButton;

    private GameObject highlightedObject;
    private bool canRotateVertically = true;
    private bool haveItem;
    private bool canTriggerEffect = false;  // 标志，指示是否可以触发粒子效果
    public AudioSource[] audio;
   // private Vector2 rotationInput;
    private void Awake()
    {
        controls = new PlayerControls();
        controls.Gameplay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => move = Vector2.zero;
       // controls.Gameplay.Rotate.performed += ctx => rotationInput = ctx.ReadValue<Vector2>();
       // controls.Gameplay.Rotate.canceled += ctx => rotationInput = Vector2.zero;
        controls.Gameplay.Pickup.performed += ctx => HandleButtonWestPress();
        controls.Gameplay.Effect.performed += ctx => HandleButtonSouthPress();

        closeButton.onClick.AddListener(() => talkCanvas.SetActive(false));
    }

  
    private void Update()
    {
        // 将移动输入转换为基于相机的方向
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
       
        Ray ray = Camera.main.ScreenPointToRay(cursorRectTransform.position);
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
    {
        Outline outline = hitObject.GetComponent<Outline>();
        if (outline != null)
        {
            if (highlightedObject != hitObject)
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

    private void HandleButtonWestPress()
    {
        if (highlightedObject != null && highlightedObject.CompareTag("CanPickup"))
        {
            highlightedObject.SetActive(false);
            Debug.Log("Deactivated: " + highlightedObject.name);
            canTriggerEffect = true;  // 设置可以触发粒子效果
        }
    }

    private void HandleButtonSouthPress()
    {
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
                    TriggerVibration(0.25f, 0.55f);  // 较低的震动频率
                    audio[1].Play();
                }
                else if (hit.collider.CompareTag("Dangerous"))
                {
                    GameObject effect = Instantiate(ClickEffect2, hit.point, Quaternion.identity);
                    PlayEffect(effect);
                    Debug.Log("Dangerous effect triggered at " + hit.point);
                    TriggerVibration(0.75f, 1.25f);  // 较高的震动频率
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

// 用于播放粒子效果并设置其自动销毁
     void PlayEffect(GameObject effect)
    {
        var particleSystem = effect.GetComponent<ParticleSystem>();
        if (particleSystem != null && !particleSystem.isPlaying)
        {
            particleSystem.Play();
            Destroy(effect, particleSystem.main.duration);  // 根据粒子系统的持续时间销毁对象
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
        if (Gamepad.current != null)
        {
            Gamepad.current.SetMotorSpeeds(lowFrequency, highFrequency);
            StartCoroutine(StopVibrationAfterDelay(0.5f));
        }
    }

    private IEnumerator StopVibrationAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
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