using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UIElements;
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

    [SerializeField] private Transform _cameraParentTransform;
    [SerializeField] private float lookSpeed = 2.0f;
    [SerializeField] private float minlookXLimit = -30.0f;
    [SerializeField] private float maxLookXLimit = 60.0f;
    Vector2 rotation = Vector2.zero;
    [SerializeField] private float _distance = -5f;
    [SerializeField] private float _height = 3f;

    private Vector3 _offset;


    private bool _isGrounded => _contactCheck.IsGrounded;
    private bool _isJumping = false;

    Action<PlayerController> _cbInteractableObjects;
    Action<int> _cbInteractableAddOrRemove;
    Action _cbPlayerDead;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _contactCheck = GetComponent<ContactCheck>();
        _inputAction = new PlayerInputAction();
        RegisterCallbackFunc();
    }

    private void FixedUpdate()
    {
        var accelerate = _isGrounded ? _groundAcceleration : _airAcceleration;

        var forward = Vector3.ProjectOnPlane(_cameraParentTransform.forward, Vector3.up);
        var right = Vector3.ProjectOnPlane(_cameraParentTransform.right, Vector3.up);

        Vector3 force = (right.normalized * _direction.x + forward.normalized * _direction.y) * accelerate;

        _rigidBody.AddForce(force, ForceMode.Acceleration);

        if (_isJumping && _isGrounded)
        {
            _rigidBody.AddForce(Vector3.up * _jumpSpeed, ForceMode.VelocityChange);
            _isJumping = false;
        }
    }


    private void Start()
    {
        _offset = new Vector3(0, _height, _distance);
    }
    private void Update()
    {
        rotation.y += UnityEngine.Input.GetAxis("Mouse X") * lookSpeed;
        rotation.x += -UnityEngine.Input.GetAxis("Mouse Y") * lookSpeed;
        rotation.x = Mathf.Clamp(rotation.x, minlookXLimit, maxLookXLimit);
        _cameraParentTransform.localRotation = Quaternion.Euler(rotation.x, rotation.y, 0);
        _cameraParentTransform.position = transform.position;
        
    }

    public void PlayerKill()
    {
        if (_cbPlayerDead == null)
            return;
        _cbPlayerDead();
    }

    public void RegisterInteractableObjectsCallback(Action<PlayerController> callback)
    {
        _cbInteractableObjects += callback;
        InteractableAddOrRemove(1);
    }
    public void UnRegisterInteractableObjectsCallback(Action<PlayerController> callback)
    {
        _cbInteractableObjects -= callback;
        InteractableAddOrRemove(-1);
    }
    public void RegisterPlayerDeadedCallback(Action callback)
    {
        _cbPlayerDead += callback;
    }
    public void UnRegisterPlayerDeadedCallback(Action callback)
    {
        _cbPlayerDead -= callback;
    }
    public void RegisterInteractableObjectsAddorRemCallback(Action<int> callback)
    {
        _cbInteractableAddOrRemove += callback;
    }
    public void UnRegisterInteractableObjectsAddorRemCallback(Action<int> callback)
    {
        _cbInteractableAddOrRemove -= callback;
    }

    private void OnEnable()
    {
        _inputAction.Enable();
    }

    private void OnDisable()
    {
        _inputAction.Disable();
    }

    private void InteractableAddOrRemove(int addOrRemove)
    {
        if (_cbInteractableAddOrRemove == null)
            return;
        _cbInteractableAddOrRemove(addOrRemove);
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
        if (_cbInteractableObjects == null)
            return;
        _cbInteractableObjects(this);
    }
}
