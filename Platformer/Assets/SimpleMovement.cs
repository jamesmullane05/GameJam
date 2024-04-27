using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public float jumpBufferDuration = 0.2f; // Jump buffer duration in seconds
    private float move;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private float jumpBufferTimer; // Timer to store when the jump button was pressed
    private bool IsJumping;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(move * speed, rb.velocity.y);
        animator.SetFloat("Speed", Mathf.Abs(move * speed));
        
        if (move < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (move > 0)
        {
            spriteRenderer.flipX = false;
        }

        animator.SetBool("IsJumping", rb.velocity.y > 0.01);

        // Apply gravity modifications for a more realistic jump
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        // Jumping buffer timer
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpBufferTimer = jumpBufferDuration; // Start the timer when the jump button is pressed
        }

        if (jumpBufferTimer > 0 && Mathf.Abs(rb.velocity.y) < 0.001f)
        {
            // If the player pressed jump within the buffer duration and is grounded, then jump
            Jump();
        }

        // Decrease the jump buffer timer
        jumpBufferTimer -= Time.deltaTime;
    }

    void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        jumpBufferTimer = 0f; // Reset the jump buffer timer
    }

    public bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f);
        return hit.collider != null;
    }
}
