using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Opendoor : MonoBehaviour
{
      private Animator anim;
    private bool inRange = false; // 是否在感应范围内
    private bool isOpen = false; // 门是否打开
    private PlayerControls controls; // 控制器
    public GameObject doorP; // 门的提示面板或其它对象
    private bool isAnimating = false;  // 用于控制动画状态
    private float delayTime = 2f;
    void Awake()
    {
        anim = GetComponent<Animator>();
        controls = new PlayerControls(); // 实例化 Input Actions
    }

    void OnEnable()
    {
        controls.Gameplay.Enable();
        controls.Gameplay.DoorControl.performed += HandleDoorControl;
    }

    void OnDisable()
    {
        controls.Gameplay.DoorControl.performed -= HandleDoorControl;
        controls.Gameplay.Disable();
    }
    
    private void HandleDoorControl(InputAction.CallbackContext context)
    {
        if (inRange && !isOpen && !isAnimating)
        {
            Debug.Log("Open door");
            anim.SetTrigger("Open");
            isOpen = true;
            isAnimating = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = true; // 标记玩家进入感应范围
            doorP.SetActive(true); // 显示门的UI提示
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited the trigger area.");
            inRange = false;
            if (isOpen && !inRange) {
                Invoke("CloseDoor", delayTime); // 延时关闭门
            }
        }
    }

  
    void CloseDoor()
    {
        
            anim.SetTrigger("Close");
        
    }
 
}

