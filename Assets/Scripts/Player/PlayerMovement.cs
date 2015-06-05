using UnityEngine;
using System.Collections.Generic;

public class PlayerMovement
{
    private const float Gravity = -1.0f;

    public bool IsOnGround { get; set; }

    public Vector3 Update()
    {
        if (IsOnGround)
        {
            return Vector2.zero;
        }
        return new Vector3(0.0f, Gravity);
    }
}