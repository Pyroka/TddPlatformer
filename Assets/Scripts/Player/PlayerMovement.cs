using UnityEngine;
using System.Collections.Generic;

public class PlayerMovement
{
    private const float Gravity = -1.0f;

    public bool IsOnGround { get; set; }

    public Vector3 CurrentVelocity { get; private set; }

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
    }
}