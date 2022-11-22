using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.Windows;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _groundAcceleration = 5f;
    [SerializeField] private float _airAcceleration = 3f;
    [SerializeField] private float _jumpSpeed = 2f;
    private PlayerInputAction _inputAction;
    private ContactCheck _contactCheck;
    private Rigidbody _rigidBody;
    private Vector2 _direction;
    private Transform _inputSpace;

    private bool _isGrounded => _contactCheck.IsGrounded;
    private bool _isJumping = false;

    List<Interactable> interactableList;

    private void Awake()
    {
        _inputSpace = Camera.main.transform;
        _rigidBody = GetComponent<Rigidbody>();
        _contactCheck = GetComponent<ContactCheck>();
        _inputAction = new PlayerInputAction();
        RegisterCallbackFunc();
        interactableList = new List<Interactable>();
    }

    private void FixedUpdate()
    {
        var accelerate = _isGrounded ? _groundAcceleration : _airAcceleration;

        var forward = Vector3.ProjectOnPlane(_inputSpace.forward, Vector3.up);
        var right = Vector3.ProjectOnPlane(_inputSpace.right, Vector3.up);

        Vector3 force = (right.normalized * _direction.x + forward.normalized * _direction.y) * accelerate;

        _rigidBody.AddForce(force, ForceMode.Acceleration);

        if (_isJumping && _isGrounded)
        {
            _rigidBody.AddForce(Vector3.up * _jumpSpeed, ForceMode.VelocityChange);
            _isJumping = false;
        }
    }

    private void OnEnable()
    {
        _inputAction.Enable();
    }

    private void OnDisable()
    {
        _inputAction.Disable();
    }

    private void RegisterCallbackFunc()
    {
        _inputAction.Player.Move.performed += OnMove;
        _inputAction.Player.Move.canceled += OnMoveCenceled;
        _inputAction.Player.Interact.started += OnInteract;
        _inputAction.Player.Jump.started += OnJump;
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        _direction = context.ReadValue<Vector2>();
    }

    private void OnMoveCenceled(InputAction.CallbackContext context)
    {
        _direction = Vector2.zero;
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (_isGrounded)
            _isJumping = true;
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        foreach (Interactable item in interactableList)
        {
            item.Activate();
        }
    }

    public void RegisterTriggerInteract(Interactable func)
    {
        interactableList.Add(func);
    }

    public void UnRegisterTriggerInteract(Interactable func)
    {
        interactableList.Remove(func);
    }
}
