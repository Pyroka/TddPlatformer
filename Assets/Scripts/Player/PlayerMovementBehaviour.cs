using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovementBehaviour : MonoBehaviour
{
    public PlayerMovement PlayerMovement;

    private Rigidbody rigidbody;
    private float distToFeet;
    private const float GroundCheckExtra = 0.01f;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        distToFeet = GetComponentInChildren<Collider>().bounds.extents.y;
    }

    void FixedUpdate()
    {
        GatherWorldState();
        UpdateAndApplyVelocity();
    }

    private void GatherWorldState()
    {
        PlayerMovement.IsOnGround = IsOnGround();
    }

    private bool IsOnGround()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToFeet + GroundCheckExtra);
    }

    private void UpdateAndApplyVelocity()
    {
        PlayerMovement.UpdateCurrentVelocity();
        rigidbody.velocity = PlayerMovement.CurrentVelocity;
    }
}
