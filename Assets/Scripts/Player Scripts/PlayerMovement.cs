using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject SkillsCanvas;
    public GameObject SettingsCanvas;

    [Header("Components")]
    private Rigidbody2D rb;

    [Header("Layer Masks")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask cornerCorrectLayer;

    [Header("Movement Variables")]
    [SerializeField] private float movementAcceleration;
    [SerializeField] private float maxMoveSpeed;
    [SerializeField] private float groundLinearDrag;
    private float horizontalDirection;
    private float verticalDirection;
    private bool changingDirection => (rb.velocity.x > 0f && horizontalDirection < 0f) || (rb.velocity.x < 0f && horizontalDirection > 0f);

    [Header("Jump Variables")]
    [SerializeField] private float Jumpforce;
    [SerializeField] private float airLinearDrag;
    [SerializeField] private float fallMultiplier;
    [SerializeField] private float lowJumpFallMultiplier;
    [SerializeField] private int extraJumps;
    [SerializeField] private float jumpDelay;
    [SerializeField] private float hangTime;
    [SerializeField] private float jumpBufferLength;
    [SerializeField] private float jumpTime;
    private bool isJumping;
    private float jumpTimeCounter;
    private float hangTimeCounter;
    private float jumpBufferCounter;
    private float jumpCooldown;
    private int extraJumpVal;
    private bool canJump => jumpBufferCounter > 0f && (hangTimeCounter > 0f || (extraJumpVal > 0f && jumpCooldown < 0f));

    [Header("Dash Variables")]
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashLength;
    [SerializeField] private float dashBufferLength;
    private float dashBufferCounter;
    private bool isDashing = false;
    private bool hasDashed = false;
    private bool canDash => dashBufferCounter > 0f && !hasDashed;

    [Header("Collision Variables")]
    [SerializeField] private float groundRaycastLength;
    [SerializeField] private Vector3 groundRaycastOffset;
    private bool onGround;

    [Header("Corner Correction Variables")]
    [SerializeField] private float topRaycastLength;
    [SerializeField] private Vector3 edgeRaycastOffset;
    [SerializeField] private Vector3 innerRaycastOffset;
    private bool canCornerCorrect;

    [Header("Animation")]
    private bool facingRight;
    //private bool idle;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        horizontalDirection = GetInput().x;
        verticalDirection = GetInput().y;
        if (horizontalDirection > 0f)
        {
            facingRight = true;
            //idle = false;
        }
        else if (horizontalDirection < 0f)
        {
            facingRight = false;
            //idle = false;
        }
        else
        {
            //idle = true;
        }
        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferLength;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SkillsCanvas.SetActive(SkillsCanvas.activeInHierarchy == true ? false : true);
            Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SettingsCanvas.SetActive(SettingsCanvas.activeInHierarchy == true ? false : true);
            Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if (Input.GetButton("Jump") && isJumping)
        {
            if (jumpTimeCounter > 0f)
            {
                rb.velocity = new Vector2(rb.velocity.x, Jumpforce);
                jumpTimeCounter -= Time.deltaTime;
            }
            else
                isJumping = false;
        }
        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }
        if (Input.GetButtonDown("Dash"))
        {
            dashBufferCounter = dashBufferLength;
        }
        else
            dashBufferCounter -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        CheckCollisions();
        if (canDash)
        {
            StartCoroutine(Dash(horizontalDirection, verticalDirection));
        }
        if (!isDashing)
        {
            MoveCharacter();
            if (onGround)
            {
                extraJumpVal = extraJumps;
                jumpCooldown = jumpDelay;
                hangTimeCounter = hangTime;
                ApplyGroundLinearDrag();
                hasDashed = false;
            }
            else
            {
                ApplyAirLinearDrag();
                FallMultiplier();
                jumpCooldown -= Time.fixedDeltaTime;
                hangTimeCounter -= Time.fixedDeltaTime;
            }
            if (canJump)
                Jump();
        }
        if (canCornerCorrect)
            CornerCorrect(rb.velocity.y);

    }

    private Vector2 GetInput()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void MoveCharacter()
    {
        rb.AddForce(new Vector2(horizontalDirection, 0f) * movementAcceleration);

        if (Mathf.Abs(rb.velocity.x) > maxMoveSpeed)
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxMoveSpeed, rb.velocity.y);
    }

    private void ApplyGroundLinearDrag()
    {
        if (Mathf.Abs(horizontalDirection) < 0.4f || changingDirection)
        {
            rb.drag = groundLinearDrag;
        }
        else
        {
            rb.drag = 0f;
        }
    }

    private void ApplyAirLinearDrag()
    {
        rb.drag = airLinearDrag;
    }

    private void Jump()
    {
        if (!onGround)
        {
            extraJumpVal--;
            jumpCooldown = jumpDelay;
        }

        ApplyAirLinearDrag();
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(Vector2.up * Jumpforce, ForceMode2D.Impulse);
        hangTimeCounter = 0f;
        jumpBufferCounter = 0f;
        jumpTimeCounter = jumpTime;
        isJumping = true;
    }

    private void FallMultiplier()
    {
        if (rb.velocity.y < 0)
        {
            rb.gravityScale = fallMultiplier;
        }
        else if (rb.velocity.y > 0 && !Input.GetButtonDown("Jump"))
        {
            rb.gravityScale = lowJumpFallMultiplier;
        }
        else
            rb.gravityScale = 1f;
    }

    private void CheckCollisions()
    {
        onGround = Physics2D.Raycast(transform.position + groundRaycastOffset, Vector2.down, groundRaycastLength, groundLayer) ||
            Physics2D.Raycast(transform.position - groundRaycastOffset, Vector2.down, groundRaycastLength, groundLayer);

        canCornerCorrect = Physics2D.Raycast(transform.position + edgeRaycastOffset, Vector2.up, topRaycastLength, cornerCorrectLayer) &&
            !Physics2D.Raycast(transform.position + innerRaycastOffset, Vector2.up, topRaycastLength, cornerCorrectLayer) ||
            Physics2D.Raycast(transform.position - edgeRaycastOffset, Vector2.up, topRaycastLength, cornerCorrectLayer) &&
            !Physics2D.Raycast(transform.position - innerRaycastOffset, Vector2.up, topRaycastLength, cornerCorrectLayer);
    }

    private void CornerCorrect(float Yvelocty)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position - innerRaycastOffset + Vector3.up * topRaycastLength, Vector3.left, topRaycastLength, cornerCorrectLayer);
        if (hit.collider != null)
        {
            float newPos = Vector3.Distance(new Vector3(hit.point.x, transform.position.y, 0f) + Vector3.up * topRaycastLength, transform.position - edgeRaycastOffset + Vector3.up * topRaycastLength);
            transform.position = new Vector3(transform.position.x + newPos, transform.position.y, transform.position.z);
            rb.velocity = new Vector2(rb.velocity.x, Yvelocty);
            return;
        }

        hit = Physics2D.Raycast(transform.position + innerRaycastOffset + Vector3.up * topRaycastLength, Vector3.right, topRaycastLength, cornerCorrectLayer);
        if (hit.collider != null)
        {
            float newPos = Vector3.Distance(new Vector3(hit.point.x, transform.position.y, 0f) + Vector3.up * topRaycastLength, transform.position + edgeRaycastOffset + Vector3.up * topRaycastLength);
            transform.position = new Vector3(transform.position.x - newPos, transform.position.y, transform.position.z);
            rb.velocity = new Vector2(rb.velocity.x, Yvelocty);
            return;
        }
    }

    public void ChangeSpeedMultiplier(int speed)
    {
        maxMoveSpeed += speed;
        movementAcceleration += (speed * 2);
    }

    public void ChangeJumpHeight(int jump)
    {
        Jumpforce += jump;
    }

    public void ChangeJumpAmount(int jumps)
    {
        extraJumps += jumps;
    }

    private IEnumerator Dash(float x, float y)
    {
        float dashStartTime = Time.time;
        hasDashed = true;
        isDashing = true;
        isJumping = false;

        rb.velocity = Vector2.zero;
        rb.gravityScale = 0f;
        rb.drag = 0f;

        Vector2 dir;
        if (x != 0f || y != 0f)
            dir = new Vector2(x, y);
        else
        {
            if (facingRight)
                dir = new Vector2(1f, 0f);
            else
                dir = new Vector2(-1, 0f);
        }

        while (Time.time < dashStartTime + dashLength)
        {
            rb.velocity = dir.normalized * dashSpeed;
            yield return null;
        }

        isDashing = false;
    }
}
