using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKeyboardMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 500f;
    private Animator animator;

    private bool isMoving = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // 计算移动方向
        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;

        // 移动
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);

        // 如果有移动输入，进行转向，并触发Run动画
        if (moveDirection != Vector3.zero)
        {
            RotateTowardsMoveDirection(moveDirection);
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        // 更新动画状态
        animator.SetBool("Run", isMoving);
    }

    // 使角色转向移动方向
    private void RotateTowardsMoveDirection(Vector3 moveDirection)
    {
        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
