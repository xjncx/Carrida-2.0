using UnityEngine;

public class WheelsAxis : MonoBehaviour
{
    [SerializeField] private WheelCollider _leftWheel;
    [SerializeField] private WheelCollider _rightWheel;
    [SerializeField] private bool _isMotor;
    [SerializeField] private bool _isSteering;

    public void Move(float motor, float steering)
    {
        if (_isSteering)
        {
            _leftWheel.steerAngle = steering;
            _rightWheel.steerAngle = steering;
        }
        if (_isMotor)
        {
            _leftWheel.motorTorque = motor;
            _rightWheel.motorTorque = motor;
        }

        ApplyLocalPositionToVisuals(_rightWheel);
        ApplyLocalPositionToVisuals(_leftWheel);

    }

    private void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        {
            if (collider.transform.childCount == 0)
            {
                return;
            }

            Transform visualWheel = collider.transform.GetChild(0);
            Vector3 position;
            Quaternion rotation;
            collider.GetWorldPose(out position, out rotation);

            visualWheel.transform.position = position;
            visualWheel.transform.rotation = rotation;
        }
    }
}