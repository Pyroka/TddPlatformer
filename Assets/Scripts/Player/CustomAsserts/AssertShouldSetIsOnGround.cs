using UnityEngine;

public class AssertShouldSetIsOnGround : MonoBehaviour
{
    public PlayerMovementBehaviour Player;
    public GameObject Ground;

    private int numFixedUpdates = 0;

    void Awake()
    {
        TeleportPlayerToGround();
    }

    void FixedUpdate()
    {
        if (numFixedUpdates == 2)
        {
            if (!Player.PlayerMovement.IsOnGround)
            {
                IntegrationTest.Fail(Player.gameObject, "IsOnGround was not true when on ground");
            }
            MovePlayerOffGround();

        }
        else if (numFixedUpdates == 4)
        {
            if (!Player.PlayerMovement.IsOnGround)
            {
                IntegrationTest.Pass(Player.gameObject);
            }
            else
            {
                IntegrationTest.Fail(Player.gameObject, "IsOnGround was not set to false after moving off ground");
            }
        }
        numFixedUpdates++;
    }

    private void MovePlayerOffGround()
    {
        Player.transform.Translate(Vector3.up * 0.1f);
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
