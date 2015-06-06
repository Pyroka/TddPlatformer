using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovementBehaviour : MonoBehaviour
{
    public PlayerMovement PlayerMovement;

    private Rigidbody rigidbody;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        PlayerMovement.Update();
        rigidbody.velocity = PlayerMovement.CurrentVelocity;
    }
}
