using UnityEngine;
using System.Collections.Generic;

public class AssertShouldSetIsOnGround : MonoBehaviour
{
    public PlayerMovementBehaviour Player;
    public GameObject Ground;
    [Range(1, 10)]
    public int CheckOnFixedUpdate;

    private int numFixedUpdates = 0;

    void Awake()
    {
        if (Player.PlayerMovement.IsOnGround)
        {
            IntegrationTest.Fail(Player.gameObject, "IsOnGround did not start as false");
        }
    }

    void FixedUpdate()
    {
        if (numFixedUpdates == 0)
        {
            TeleportPlayerToGround();
        }
        else if (numFixedUpdates == CheckOnFixedUpdate)
        {
            if (!Player.PlayerMovement.IsOnGround)
            {
                IntegrationTest.Fail(Player.gameObject, "IsOnGround was not set to true after " + CheckOnFixedUpdate + " frames");
            }
        }
        numFixedUpdates++;
    }

    private void TeleportPlayerToGround()
    {
        var topOfGround = Ground.GetComponent<BoxCollider>().bounds.max.y;
        var bottomOfPlayer = Player.GetComponentInChildren<Collider>().bounds.min.y;
        var centerOfPlayer = Player.transform.position.y;

        var newPlayerPos = new Vector3(0.0f, topOfGround + (centerOfPlayer - bottomOfPlayer), 0.0f);
        Player.transform.position = newPlayerPos;
    }
}
