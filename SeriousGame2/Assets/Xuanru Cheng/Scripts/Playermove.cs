using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityCharacterController;
using UnityEngine;
using UnityEngine.InputSystem;
public class Playermove : MonoBehaviour
{
     PlayerControls controls;//Access Control
     private Vector2 move;
    // private Vector2 rotate;
     public GameObject targetObject;
     private bool isCollidingWithTarget = false;
     public Transform cameraTransform;
     public RectTransform CursorRectTransform; 
     public float cursorDistance = 0.5f;  // 光标距物体前方的距离Distance of the cursor from the front of the object
     private float raycastCooldown = 0.1f; // 每0.1秒检测一次Detection every 0.1 seconds
     private float timeSinceLastRaycast = 0.0f;
     public float detectionRadius = 1.0f;
     
   /*  private void Awake()
     {
         controls = new PlayerControls();
         controls.Gameplay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
         controls.Gameplay.Move.canceled += ctx => move = Vector2.zero;
         controls.Gameplay.Pickup.performed += ctx => HandleButtonWestPress();
         
     }
*/
     private void HandleButtonWestPress()
     {
         Debug.Log("Button West Pressed");
         if (isCollidingWithTarget && targetObject != null)
         {
             Debug.Log("Target object will be deactivated");
             targetObject.SetActive(false);
         }
         else
         {
             Debug.Log("Button West Pressed, but no target to deactivate or not colliding with target");
         }
     }
     void Update()
     {
         Vector3 direction = new Vector3(move.x, 0, move.y).normalized;
         Vector3 right = cameraTransform.right;
         Vector3 forward = cameraTransform.forward;
         forward.y = 0; // 忽略摄像机在垂直方向的倾斜Ignore the camera tilt in the vertical direction
         right.y = 0;

         Vector3 moveDirection = (forward * direction.z + right * direction.x) * Time.deltaTime * 10f;
         transform.Translate(moveDirection, Space.World);
       
          
        /*Vector2 cursorPosition = CursorRectTransform.anchoredPosition;
        Vector3 screenPosition = RectTransformUtility.WorldToScreenPoint(Camera.main, CursorRectTransform.position);
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
       
        float sphereRadius = 10.0f;  // 你希望的球体半径大小
        float maxDistance = 20.0f; // 最大检测距离
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.red); // 用于可视化射线路径

        if (Physics.SphereCast(ray, sphereRadius, out hit, maxDistance))
        { 
            Debug.Log("Hit " + hit.transform.name);
            if (hit.transform.CompareTag("CanPickup"))
            {
                Debug.Log("Hit object tagged as CanPickup: " + hit.transform.name);
                targetObject = hit.transform.gameObject;
                isCollidingWithTarget = true;
            }
            else
            {
                isCollidingWithTarget = false;
            }
        }
        
        else
        {
            Debug.Log("No hit detected by the SphereCast.");
            isCollidingWithTarget = false;
        }*/
       
        //UpdateCursor();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            Debug.Log("Hit " + hit.transform.name);  // 日志输出被击中的对象名The log outputs the name of the object that was hit

            // 如果按下鼠标左键或特定按键，可以对物体进行交互Objects can be interacted with if the left mouse button or a specific key is pressed
            if (Input.GetMouseButtonDown(0))
            {
                SelectObject(hit.transform.gameObject);
            }
        }

       
     }
    
     void SelectObject(GameObject selectedObject)
     {
         // 这里可以添加选择对象后的逻辑，例如高亮显示或显示信息Here you can add logic after selecting an object, such as highlighting or displaying a message
         Debug.Log("Object selected: " + selectedObject.name);
     }

     private void OnEnable()
     {
         controls.Gameplay.Enable();
     }

     private void OnDisable()
     {
         controls.Gameplay.Disable();
     }
   private void OnTriggerEnter(Collider other)
     {
         if (other.gameObject == targetObject)
         {
             isCollidingWithTarget = true;
             Debug.Log("Started colliding with target");
         }
     }

     private void OnTriggerExit(Collider other)
     {
         if (other.gameObject == targetObject)
         {
             isCollidingWithTarget = false;
             Debug.Log("Stopped colliding with target");
         }
     }
}
