using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBasic : MonoBehaviour
{
    public float speed = 5f;

    private Rigidbody2D rb2D;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Movement vector
        Vector2 movement = new Vector2(horizontalInput, verticalInput);

        // Normalize to prevent faster movement diagonally
        movement.Normalize();

        // Move the player
        MovePlayer(movement);
    }

    void MovePlayer(Vector2 direction)
    {
        // Apply movement to the rigidbody
        rb2D.velocity = new Vector2(direction.x * speed, direction.y * speed);
    }
}
