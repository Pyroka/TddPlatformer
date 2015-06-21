using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class PlayerMovement
{
    private const float Gravity = -1.0f;

    public bool IsOnGround { get; set; }

    public Vector3 CurrentVelocity { get; private set; }
    public float HorizontalInput { get; set; }
    public float MaxHorizontalSpeed { get; set; }

    public void Update()
    {
        if (IsOnGround)
        {
            CurrentVelocity = Vector2.zero;
        }
        else
        {
            CurrentVelocity += new Vector3(0.0f, Gravity);
        }

        CurrentVelocity = new Vector3(MaxHorizontalSpeed * HorizontalInput, CurrentVelocity.y);
    }
}