using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Player Settings")]
    public float jumpForce = 10f;
    public float gravityScale = 1f;

    [Header("Ground Check Settings")]
    public Transform groundChecker;
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;

    [Header("Input Actions")]
    public InputActionReference jumpAction;

    private Rigidbody2D rb;
    private bool isGrounded;
    private Animator animator;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = gravityScale;

        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundChecker.position, groundCheckRadius, groundLayer);

        if (isGrounded && jumpAction.action.IsPressed())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;
        }

        animator.SetBool("Jump",!isGrounded);
    }

    private void OnDrawGizmos()
    {
        if (groundChecker != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundChecker.position,groundCheckRadius);
        }
    }
}
