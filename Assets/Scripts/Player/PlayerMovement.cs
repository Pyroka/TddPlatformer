using UnityEngine;
using System.Collections.Generic;

public class PlayerMovement
{
    private const float Gravity = -1.0f;

    public bool IsOnGround { get; set; }

    public Vector3 Update()
    {
        return new Vector3(0.0f, Gravity);
    }
}