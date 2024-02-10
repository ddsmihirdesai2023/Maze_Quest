using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiltTheBoard : MonoBehaviour
{
    public float tiltSpeed = .2f;
    public DynamicJoystick dynamicJoystick;

    void Update()
    {
        // Get joystick input
        float horizontalInput = dynamicJoystick.Horizontal;  // X-axis
        float verticalInput = dynamicJoystick.Vertical;      // Y-axis

        // Calculate tilt angles
        float targetTiltX = verticalInput * tiltSpeed;
        float targetTiltZ = -horizontalInput * tiltSpeed;

        // Smoothly tilt the plane towards the target angles
        Quaternion targetRotation = Quaternion.Euler(targetTiltX, 0f, targetTiltZ);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * tiltSpeed);
    }
}
