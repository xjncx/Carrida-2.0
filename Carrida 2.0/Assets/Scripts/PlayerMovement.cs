using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private List<WheelsAxis> _wheelsAxles;
    //[SerializeField] private float _maxMotorSpeed;
    [SerializeField] private float _maxSteeringAngle;
    [SerializeField] private float _accelerate;
    [SerializeField] private GameObject _centerOfMass;
    [SerializeField] private Motor _motor;

    private PlayerInput _playerInput;
    private Vector2 _direction;
    private Rigidbody _carRb;
    private float _steering;
    private float _motorSpeed;

    private void Awake()
    {
        _playerInput = new PlayerInput();
    }

    private void Start()
    {
        _carRb = GetComponent<Rigidbody>();
        _carRb.centerOfMass = _centerOfMass.transform.localPosition;
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
    public void ChangeMotor(Motor motor)
    {
        _motor = motor;
    }

    private void Drive()
    {
        if (_wheelsAxles.Count > 0)

            foreach (WheelsAxis wheelsAxis in _wheelsAxles)
            {
                wheelsAxis.Move(_motorSpeed, _steering);
            }
    }

    private void HandlerInput()
    {
        _direction = _playerInput.Player.Move.ReadValue<Vector2>();
        _motorSpeed = _motor.MaxPower * _direction.y;
        _steering = _maxSteeringAngle * _direction.x;
    }


}