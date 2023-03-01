using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private List<WheelsAxis> _wheelsAxles;
    [SerializeField] private float _maxMotorSpeed;
    [SerializeField] private float _maxSteeringAngle;
    [SerializeField] private float _accelerate;
    [SerializeField] private GameObject _centerOfMass;
    private PlayerInput _playerInput;
    private Vector2 _direction;

    private Rigidbody _rb;
    private float _steering;
    private float _motor;

    private void Awake()
    {
        _playerInput = new PlayerInput();
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.centerOfMass = _centerOfMass.transform.localPosition;
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }
    private void FixedUpdate()
    {
        Drive();
    }

    private void Update()
    {
        HandlerInput();
    }

    private void Drive()
    {
        if (_wheelsAxles.Count > 0)

            foreach (WheelsAxis wheelsAxis in _wheelsAxles)
            {
                wheelsAxis.Move(_motor, _steering);
            }
    }

    private void HandlerInput()
    {
        _direction = _playerInput.Player.Move.ReadValue<Vector2>();
        _motor = _maxMotorSpeed * _direction.y;
        _steering = _maxSteeringAngle * _direction.x;
    }
}