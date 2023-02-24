using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody _sphereRb;
    [SerializeField] private Rigidbody _carRb;
    [SerializeField] private float _forwardSpeed;
    [SerializeField] private float _reverseSpeed;
    [SerializeField] private float _turnSpeed;
    [SerializeField] private LayerMask _ground;
    [SerializeField] private float _alignToGroundTime;
    [SerializeField] private float _modifiedDrag;

    private PlayerInput _playerInput;
    private Vector2 _direction;
    private float _moveInput;
    private float _turnInput;
    private float _normalDrag;
    private bool _isGrounded;

    private void Awake()
    {
        _playerInput = new PlayerInput();
    }

    private void Start()
    {
        _sphereRb.transform.parent = null;
        _carRb.transform.parent = null;
        _normalDrag = _sphereRb.drag;
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    private void Update()
    {
        HandleInput();
        Turn();
        AlignToGroundTime();
        Move();
    }

    private void FixedUpdate()
    {
        Debug.Log("isGrounded: " + _isGrounded);
        if (_isGrounded)
        {
            _sphereRb.AddForce(transform.forward * _moveInput, ForceMode.Acceleration);
        }
        else
        {
            _sphereRb.AddForce(transform.up * -200f);
        }
        Debug.Log("isGrounded: " + _isGrounded);
        _carRb.MoveRotation(transform.rotation);
    }

    private void HandleInput()
    {
        _direction = _playerInput.Player.Move.ReadValue<Vector2>();
        _moveInput = _direction.y;
        _turnInput = _direction.x;
    }

    private void Turn()
    {
        float newRotation = _turnInput * _turnSpeed * Time.deltaTime * _moveInput;
        if (_isGrounded)
            transform.Rotate(0, newRotation, 0, Space.World);

        transform.position = _sphereRb.transform.position;
    }

    private void Move()
    {
        _moveInput *= _moveInput > 0 ? _forwardSpeed : _reverseSpeed;
        _sphereRb.drag = _isGrounded ? _normalDrag : _modifiedDrag;
    }

    private void AlignToGroundTime()
    {
        RaycastHit hit;
        _isGrounded = Physics.Raycast(transform.position, -transform.up, out hit, 1f, _ground);
        Quaternion toRotateTo = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, toRotateTo, _alignToGroundTime * Time.deltaTime);
    }
}
