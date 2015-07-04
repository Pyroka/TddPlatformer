using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class PlayerMovement
{
    public const float Gravity = -1.0f;
    public float AccelerationTime { get; set; }

    public bool IsOnGround { get; set; }

    public Vector3 CurrentVelocity { get; set; }
    public float HorizontalInput { get; set; }
    public float MaxHorizontalSpeed { get; set; }

    public void UpdateCurrentVelocity(float deltaTime)
    {
        var newVelocity = CurrentVelocity;
        if (IsOnGround)
        {
            newVelocity.y = 0.0f;
        }
        else
        {
            newVelocity.y += (Gravity * deltaTime);
        }

        var desiredXVelocity = MaxHorizontalSpeed * HorizontalInput;
        var useInstantAcceleration = AccelerationTime == 0.0f;
        if (useInstantAcceleration)
        {
            newVelocity.x = desiredXVelocity;
        }
        else
        {
            var desiredAcceleration = desiredXVelocity - CurrentVelocity.x;
            var maxAccelerationPerSecond = MaxHorizontalSpeed / AccelerationTime;
            var acceleration = desiredAcceleration;
            if (Mathf.Abs(acceleration) > Mathf.Abs(maxAccelerationPerSecond))
            {
                acceleration = maxAccelerationPerSecond * Mathf.Sign(acceleration);
            }
            newVelocity.x = Mathf.Clamp(newVelocity.x + (acceleration * deltaTime), -MaxHorizontalSpeed, MaxHorizontalSpeed);
        }
        CurrentVelocity = newVelocity;
    }
}