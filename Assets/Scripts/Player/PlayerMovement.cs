using UnityEngine;
using System.Collections.Generic;

public class PlayerMovement
{
    public bool IsOnGround { get; set; }

    public Vector3 Update()
    {
        return new Vector3(0.0f, -1.0f);
    }
}