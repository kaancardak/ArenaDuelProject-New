using System.Collections;
using UnityEngine;

public class KnightController : MonoBehaviour
{
    [Header("Hareket Ayarları")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float rollSpeed = 10f;
    public float rollDuration = 0.5f;

    [Header("Ses Efektleri")]
    public AudioClip attackSound;
    public AudioClip shieldSound;

    [Header("Zemin Kontrolü")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private Animator anim;
    private AudioSource audioSource;
    private bool isGrounded;
    private bool isRolling = false;
    private bool isBlocking = false;
    private bool isAttacking = false;
    private float facingDirection = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isRolling) return;
        HandleInput();
        UpdateAnimations();
    }

    void FixedUpdate()
    {
        if (!isRolling && !isAttacking && !isBlocking)
        {
            Move();
        }
        CheckGround();
    }

    void HandleInput()
    {
        if (Input.GetButtonDown("Jump") && isGrounded && !isBlocking)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        if (Input.GetButtonDown("Fire1") && !isAttacking && !isBlocking)
        {
            StartCoroutine(PerformAttack());
        }

        if (Input.GetMouseButtonDown(1))
        {
            if(shieldSound != null && audioSource != null) 
                audioSource.PlayOneShot(shieldSound);
        }

        if (Input.GetMouseButton(1))
        {
            isBlocking = true;
            rb.linearVelocity = Vector2.zero;
        }
        else
        {
            isBlocking = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded && !isRolling && !isBlocking)
        {
            StartCoroutine(PerformRoll());
        }
    }

    void Move()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        if (moveInput > 0 && facingDirection == -1) Flip();
        else if (moveInput < 0 && facingDirection == 1) Flip();
    }

    void Flip()
    {
        facingDirection *= -1;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    void UpdateAnimations()
    {
        anim.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));
        anim.SetBool("IsGrounded", isGrounded);
        anim.SetFloat("VerticalVelocity", rb.linearVelocity.y);
        anim.SetBool("IsBlocking", isBlocking);
    }

    IEnumerator PerformAttack()
    {
        isAttacking = true;
        anim.SetTrigger("Attack");
        rb.linearVelocity = Vector2.zero;
        yield return new WaitForSeconds(0.4f); 
        isAttacking = false;
    }

    IEnumerator PerformRoll()
    {
        isRolling = true;
        anim.SetTrigger("Roll");
        rb.linearVelocity = new Vector2(facingDirection * rollSpeed, 0);
        yield return new WaitForSeconds(rollDuration);
        isRolling = false;
    }

    public void PlayAttackSound()
    {
        if(attackSound != null && audioSource != null) 
            audioSource.PlayOneShot(attackSound);
    }
}