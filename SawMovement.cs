using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawMovement : MonoBehaviour
{
    private bool groundDetected;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform spriteChild;
    [SerializeField] private float movementSpeed, groundCheckDistance;
    [SerializeField] private LayerMask whatIsGround;

    private int facingDirection;
    private Vector2 movement;

    private void Start()
    {
        facingDirection = 1;
    }

    // Update is called once per frame
    void Update()
    {
        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        spriteChild.transform.RotateAround(gameObject.GetComponent<CircleCollider2D>().bounds.center, new Vector3(0.0f, 0.0f, 1f), movementSpeed * Time.deltaTime * -facingDirection * 50f);

        if (!groundDetected)
        {
            Flip();
        }
        else
        {
            movement.Set(movementSpeed * facingDirection, gameObject.GetComponent<Rigidbody2D>().velocity.y);
            gameObject.GetComponent<Rigidbody2D>().velocity = movement;
        }
    }

    private void Flip()
    {
        facingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }
}
