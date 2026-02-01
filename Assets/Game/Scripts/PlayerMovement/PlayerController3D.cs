using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController3D : MonoBehaviour
{
    [Header("Movimiento")]
    public float moveSpeed = 6f;
    public float rotationSpeed = 12f;

    [Header("Salto")]
    public float jumpForce = 7f;

    [Header("Dash")]
    public float dashSpeed = 14f;
    public float dashDuration = 0.25f;
    public float dashCooldown = 1f;

    [Header("Roll / Pirueta")]
    public float rollSpeed = 10f;
    public float rollDuration = 0.6f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.3f;
    public LayerMask groundMask;

    [Header("Componentes")]
    public Rigidbody rb;
    // public Animator anim; //  Descomentar cuando haya animaciones

    private Vector3 moveInput;
    private bool isGrounded;
    private bool isDashing;
    private bool isRolling;
    private bool canDash = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        // anim = GetComponent<Animator>();
    }

    void Update()
    {
        CheckGround();
        GetInput();
        HandleRotation();
    }

    void FixedUpdate()
    {
        if (!isDashing && !isRolling)
        {
            Move();
        }
    }

    void GetInput()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        moveInput = new Vector3(h, 0f, v).normalized;

        // SALTO
        if (Input.GetButtonDown("Jump") && isGrounded && !isDashing && !isRolling)
        {
            Jump();
        }

        // DASH
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash && moveInput != Vector3.zero)
        {
            StartCoroutine(Dash());
        }

        // ROLL / PIRUETA
        if (Input.GetKeyDown(KeyCode.LeftControl) && isGrounded && moveInput != Vector3.zero)
        {
            StartCoroutine(Roll());
        }


        // anim.SetFloat("Speed", moveInput.magnitude);
    }

    void Move()
    {
        Vector3 velocity = moveInput * moveSpeed;
        velocity.y = rb.linearVelocity.y;

        rb.linearVelocity = velocity;
    }

    void HandleRotation()
    {
        if (moveInput != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveInput);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }
    }

    void Jump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        // anim.SetTrigger("Jump");
    }

    void CheckGround()
    {
        isGrounded = Physics.CheckSphere(
            groundCheck.position,
            groundDistance,
            groundMask
        );

        // anim.SetBool("IsGrounded", isGrounded);
    }

    IEnumerator Dash()
    {
        isDashing = true;
        canDash = false;

        // anim.SetTrigger("Dash");

        float elapsed = 0f;
        Vector3 dashDirection = moveInput;

        while (elapsed < dashDuration)
        {
            rb.linearVelocity = dashDirection * dashSpeed;
            elapsed += Time.deltaTime;
            yield return null;
        }

        isDashing = false;
        rb.linearVelocity = Vector3.zero;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    IEnumerator Roll()
    {
        isRolling = true;

        // anim.SetTrigger("Roll");

        float elapsed = 0f;
        Vector3 rollDirection = transform.forward;

        while (elapsed < rollDuration)
        {
            rb.linearVelocity = rollDirection * rollSpeed;
            elapsed += Time.deltaTime;
            yield return null;
        }

        isRolling = false;
    }
}
