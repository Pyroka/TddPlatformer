using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class PlayerMovement
{
    public const float Gravity = -1.0f;
    public float AccelerationTime { get; set; }

    public bool IsOnGround { get; set; }

    public Vector3 CurrentVelocity { get; private set; }
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

        newVelocity.x = MaxHorizontalSpeed * HorizontalInput;
        CurrentVelocity = newVelocity;
    }
}