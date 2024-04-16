using System.Collections;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private bool isMoving = false;
    private bool isFacingRight = true;
    public Animator animator;
    public SpriteRenderer sprite;
    public Rigidbody2D rb;

    private bool isFreeze = false;

    private Vector2 currentDirection = Vector2.zero;
    private float decisionTimer = 0f;
    private float decisionDuration = 3f;

    public Transform pos2;

    private void Update()
    {
        if (isFreeze)
            return;

        // Make decisions based on a timer
        if (decisionTimer <= 0f)
        {
            // Randomly choose a direction (or specific direction)
            currentDirection = GetRandomDirection(); // Example: GetRandomDirection() for random or custom direction
            decisionDuration = Random.Range(2f, 4f); // Randomize duration

            // Flip sprite direction if needed
            if (currentDirection.x > 0 && !isFacingRight)
                Flip();
            else if (currentDirection.x < 0 && isFacingRight)
                Flip();

            decisionTimer = decisionDuration;
        }

        // Move in the chosen direction
        rb.velocity = currentDirection * moveSpeed;

        // Update movement state
        isMoving = currentDirection.magnitude > 0.1f;
        animator.SetBool("isMoving", isMoving);

        // Reduce decision timer
        decisionTimer -= Time.deltaTime;
    }

    // Example method to get a specific direction (e.g., left and up)
    private Vector2 GetRandomDirection()
    {
        // Example: Move left and up
        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-1f, 1f);

        return new Vector2(randomX, randomY).normalized; // Normalized to maintain consistent speed
    }

    public void FreezeNPC()
    {
        isFreeze = true;
        rb.velocity = Vector2.zero; // Stop movement when frozen
    }

    public void UnfreezeNPC()
    {
        isFreeze = false;
    }

    public void getSummoned() {
        // set animation isSummoned
        animator.SetBool("isSummoned", true);
    }

    public void getUnsummoned() {
        transform.position = pos2.position;
        animator.SetBool("isSummoned", false);
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        sprite.flipX = !sprite.flipX;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is with a specific object or tag
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetBool("isMoving", false);
            FreezeNPC();
        }
    }
}
