
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [Header("Jump Settings")]
    public float jumpForce = 7f;
    [Header("Sound Settings")]
    public AudioSource audioSource;
    public AudioClip jumpSound;

    [Header("Dash Settings")]
    public float dashDistance = 2f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;

    [Header("Animation Settings")]
    public Animator dinoAnimator;

    private Rigidbody2D rb;
    private bool isGrounded;
    private int jumpCount = 0;
    private int maxJumps = 2;
    private bool isDashing;
    private bool canDash = true;
    private float startingX;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        startingX = transform.position.x;

        if (dinoAnimator == null)
        {
            dinoAnimator =
                GetComponentInChildren<Animator>();
        }
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Time.timeScale == 0f)
            return;

        if (Keyboard.current == null)
            return;

        if (Keyboard.current.spaceKey.wasPressedThisFrame
            && jumpCount < maxJumps && !isDashing)
        {
            Jump();
        }

        if ((Keyboard.current.leftShiftKey.wasPressedThisFrame
            || Keyboard.current.rightShiftKey.wasPressedThisFrame)
            && canDash && isGrounded)
        {
            StartCoroutine(Dash());
        }
    }

    
    void Jump()
    {
        jumpCount++;
        isGrounded = false;

        if (dinoAnimator != null)
        {
            dinoAnimator.SetBool("IsJumping", true);
        }

        // Reset vertical speed for double jump
        rb.linearVelocity = new Vector2(
            rb.linearVelocity.x, 0f
        );

        rb.AddForce(
            Vector2.up * jumpForce,
            ForceMode2D.Impulse
        );

        // Play jump sound
        if (audioSource != null && jumpSound != null)
        {
            audioSource.PlayOneShot(jumpSound);
        }
    }


    IEnumerator Dash()
    {
        isDashing = true;
        canDash = false;

        float originalX = startingX;
        float targetX = originalX + dashDistance;
        float elapsed = 0f;

        while (elapsed < dashDuration)
        {
            elapsed += Time.fixedDeltaTime;

            float progress =
                Mathf.Clamp01(elapsed / dashDuration);

            float newX =
                Mathf.Lerp(originalX, targetX, progress);

            rb.MovePosition(
                new Vector2(newX, rb.position.y)
            );

            yield return new WaitForFixedUpdate();
        }

        elapsed = 0f;

        while (elapsed < dashDuration)
        {
            elapsed += Time.fixedDeltaTime;

            float progress =
                Mathf.Clamp01(elapsed / dashDuration);

            float newX =
                Mathf.Lerp(targetX, originalX, progress);

            rb.MovePosition(
                new Vector2(newX, rb.position.y)
            );

            yield return new WaitForFixedUpdate();
        }

        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);

        canDash = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Land();
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")
            && rb.linearVelocity.y <= 0.01f)
        {
            Land();
        }
    }

    void Land()
    {
        jumpCount = 0;

        if (!isGrounded)
        {
            isGrounded = true;

            if (dinoAnimator != null)
            {
                dinoAnimator.SetBool("IsJumping", false);
            }
        }
    }
}
