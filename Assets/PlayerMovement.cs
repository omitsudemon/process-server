using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private bool isMoving = false;
    private bool isFacingRight = true;
    public Animator animator;
    public SpriteRenderer sprite;
    public Rigidbody2D rb;

    // Script reference
    private PlayerInteraction playerInteraction;

    private bool isFreeze = false;

    private void Start()
    {
        // Initialize PlayerInteraction script
        playerInteraction = GetComponent<PlayerInteraction>();
    }

    private void Update()
    {
        if (isFreeze)
        {
            animator.SetBool("isMoving", false);
            return;
        }

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if (Mathf.Abs(moveX) > 0) 
        {
            rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);
        }

        if (Mathf.Abs(moveY) > 0) 
        {
            rb.velocity = new Vector2(rb.velocity.x, moveY * moveSpeed);
        }

        if (Mathf.Abs(moveX) == 0 && Mathf.Abs(moveY) == 0)
        {
            rb.velocity = new Vector2(0, 0);
        }

        if (moveX > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (moveX < 0 && isFacingRight)
        {
            Flip();
        }

        if (moveX != 0 || moveY != 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        animator.SetBool("isMoving", isMoving);

        HandleActionButton();
    }

    // This method is when the dialogue is opened
    public void FreezePlayer () {
        isFreeze = true;
    }

    public void UnfreezePlayer () {
        isFreeze = false;
    }

    private void HandleActionButton()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, sprite.flipX ? Vector2.left : Vector2.right, 3f);
            
            Debug.DrawRay(transform.position, sprite.flipX ? Vector2.left : Vector2.right, Color.red, 3f);

            if (hit.collider != null)
            {
                playerInteraction.Interact(hit.collider.gameObject);
            }
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        sprite.flipX = !sprite.flipX;
    }
}