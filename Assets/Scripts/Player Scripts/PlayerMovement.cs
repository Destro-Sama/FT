using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //GameObject is the variable type of physical objects on screen
    public GameObject SkillsCanvas;
    public GameObject SettingsCanvas;

    //The Header "Header" allows me to group and name variables so it is easier to use in the unity editor
    [Header("Components")]
    //Rigidbody2D is a componet that controls the physics behind mass and gravity
    private Rigidbody2D rb;

    [Header("Layer Masks")]
    //SerializeField is a header that allows me to view and edit private variables in the unity editor
    //LayerMask is the layer type of 2D objects, allows to group them
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

    //Start is a function called by unity at the start of the runtime
    //Void is the return type, void means no return
    private void Start()
    {
        //Getting the Rigidbody2D component of the obejct
        rb = GetComponent<Rigidbody2D>();
    }

    //Update is a function called by unity every frame
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
        //GetButtonDown is called when the button labeled "Jump" and can not be called again until it is released
        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferLength;
        }
        //GetKeyDown is called when the keyCode "Q" is called and can not be called again until it is released
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //SetActive allows me to control wether an object is interactable and visible or not
            //The == true? false : true is a ternary operator. It checks a bool value against a statement, if true returns false, else return true.
            SkillsCanvas.SetActive(SkillsCanvas.activeInHierarchy == true ? false : true);
            //timeScale is the physics runtime of unity, at 0 nothing is calculated or moves, and at 1 it runs at normal speed
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

    //FixedUpdate is a function called by unity every physics frame
    private void FixedUpdate()
    {
        CheckCollisions();
        if (canDash)
        {
            //StartCoroutine Starts a Coroutine
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
        //GetAxisRaw allows me to get the input data (between -1 and 1) of the values horizontally and vertically
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

    //This part is very hard to imagine
    //Raycasts are invisible lines of light, that send information back if they touch something. They can do ALOT
    //In this situation I am sending 2 rays out the player's feet, and 4 out the player's head
    //The 2 at the feet check to see if you are standing on something solid, to know you are grounded
    //The 4 at the head are split into 2 catagories.
    //The 2 on the outside check to see if there is anything above you
    //While the 2 on the inside check to see if you are too far out of an object
    //This means I can check wether it is acceptable to correct corner collisions or not
    private void CheckCollisions()
    {
        onGround = Physics2D.Raycast(transform.position + groundRaycastOffset, Vector2.down, groundRaycastLength, groundLayer) ||
            Physics2D.Raycast(transform.position - groundRaycastOffset, Vector2.down, groundRaycastLength, groundLayer);

        canCornerCorrect = Physics2D.Raycast(transform.position + edgeRaycastOffset, Vector2.up, topRaycastLength, cornerCorrectLayer) &&
            !Physics2D.Raycast(transform.position + innerRaycastOffset, Vector2.up, topRaycastLength, cornerCorrectLayer) ||
            Physics2D.Raycast(transform.position - edgeRaycastOffset, Vector2.up, topRaycastLength, cornerCorrectLayer) &&
            !Physics2D.Raycast(transform.position - innerRaycastOffset, Vector2.up, topRaycastLength, cornerCorrectLayer);
    }

    //CornerCorrect works by checking the 4 head rays to see if you are in the correct position to be corner corrected
    //If you are, then it will move you around the edge of an obstacle above you, while maintaining momentum,
    //to make jumping near obstacles feel better and smoother
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

    //IEnumerator is the type return of Coroutines
    //Coroutines are functions that run alongside regular functions, and can avoid time changes and special systems
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
