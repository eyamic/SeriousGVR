using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Animator _animator;
    private PlayerController _playerController;
    private float _moveSpeed = 5f;
    private Vector2 _movementInput;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _playerController = new PlayerController();
        _playerController.Move.Enable();
        _playerController.Move.Interaction.performed += ctx => Interaction();
        _playerController.Move.Movement.performed += ctx => MovementPerformed(ctx.ReadValue<Vector2>());
        _playerController.Move.Movement.canceled += ctx => MovementCanceled();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        Vector2 inputVector = _playerController.Move.Movement.ReadValue<Vector2>();
        _movementInput = inputVector.normalized;

        Vector3 moveDirection = new Vector3(_movementInput.x, 0, _movementInput.y);
        _rigidbody.MovePosition(transform.position + moveDirection * _moveSpeed * Time.fixedDeltaTime);

        if (inputVector.magnitude > 0.1f)
        {
            RotateTowardsMoveDirection();
            _animator.SetBool("Run", true);
        }
        else
        {
            _animator.SetBool("Run", false);
        }
    }

    private void Interaction()
    {
        // 在交互时执行的操作
    }

    private void MovementPerformed(Vector2 movement)
    {
        // 在移动时执行的操作
    }

    private void MovementCanceled()
    {
        // 在移动取消时执行的操作
    }

    private void RotateTowardsMoveDirection()
    {
        Quaternion targetRotation = Quaternion.LookRotation(new Vector3(_movementInput.x, 0, _movementInput.y));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * 500f);
    }

    private void OnEnable()
    {
        _playerController.Move.Enable();
    }

    private void OnDisable()
    {
        _playerController.Move.Disable();
    }
}