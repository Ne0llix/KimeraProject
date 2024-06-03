using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveChara : MonoBehaviour
{
    [SerializeField] Damages damages;

    [SerializeField] float transSpeed = 5f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] Animator playerAnimator;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] Vector2 targetPosition;
    [SerializeField] private float groundCheckRadius = 0.25f;

    [SerializeField] bool canJump = true;
    [SerializeField] bool isJumping = true;

    [SerializeField] bool canDash = true;
    [SerializeField] bool isDashing;
    [SerializeField] float dashSpeed = 15f;
    [SerializeField] float dashingTime = 0.2f;
    [SerializeField] float animDashTime = 0.4f;
    [SerializeField] float dashingCooldown = 0.7f;

    [SerializeField] public bool isAttacking;
    [SerializeField] public bool isEnnemyTouch;
    [SerializeField] bool isCombo;
    [SerializeField] bool canAttack = true;
    [SerializeField] float attackSpeed = 7f;
    [SerializeField] float attack1Time = 0.5f;
    [SerializeField] float attack2Time = 0.3f;
    [SerializeField] float animAttackTime = 0.2f;
    [SerializeField] float animJumpAttackTime = 0.3f;
    [SerializeField] float attackCooldown = 0.1f;

    public GameObject attack;

    [SerializeField] float tm;

    [SerializeField] public bool isMenuOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        damages.noMove = false;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        SpecialMove();
        Jump();
        Dash();
    }

    //This is a fonction to put all the translations and rotations of the cube
    void Movement()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isMenuOpen == false)
        {
            MenuManager.instance.Menu();
            isMenuOpen = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isMenuOpen == true)
        {
            MenuManager.instance.EndMenu();
            isMenuOpen = false;
        }

        if (isDashing || damages.noMove || isAttacking)
        {
            return;
        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) //Here, we ask if the Right Arrow key is push, if it is true, then, the cube go up on X axis for 0.05 per frame
        {
            transform.Translate(transSpeed * Time.deltaTime, 0, 0);
            playerAnimator.SetBool("BoolRun", true);
            spriteRenderer.flipX = true;
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
        {
            playerAnimator.SetBool("BoolRun", false);
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) //Here, we ask if the Left Arrow key is push, if it is true, then, the cube go down on X axis for 0.05 per frame
        {
            transform.Translate(-transSpeed * Time.deltaTime, 0, 0);
            playerAnimator.SetBool("BoolRun", true);
            spriteRenderer.flipX = false;
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A))
        {
            playerAnimator.SetBool("BoolRun", false);
        }

        if (Input.GetKeyDown(KeyCode.Space) && canJump == true)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isJumping = true;
        }
    }

    void SpecialMove()
    {
        if (Input.GetKey(KeyCode.Q) && damages.noMove == false && canAttack && isJumping == false && !isAttacking)
        {
            playerAnimator.SetBool("BoolAttack", true);
            playerAnimator.SetTrigger("TriggerAttack");
            StartCoroutine(Attack1());
        }
        if (Input.GetKey(KeyCode.Q) && damages.noMove == false && canAttack && isEnnemyTouch && isAttacking)
        {
            isCombo = true;
            playerAnimator.SetTrigger("TriggerAttack");
            StartCoroutine(Attack2());
        }
        if (Input.GetKey(KeyCode.Q) && damages.noMove == false && canAttack && isEnnemyTouch && isCombo && isAttacking)
        {
            playerAnimator.SetTrigger("TriggerAttack");
            StartCoroutine(Attack3());
        }
        if (Input.GetKey(KeyCode.Q) && damages.noMove == false && canAttack && canJump == false)
        {
            playerAnimator.SetBool("BoolAttack", true);
            playerAnimator.SetTrigger("TriggerAttack");
            StartCoroutine(JumpAttack());
            playerAnimator.SetBool("BoolAttack", false);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash && canJump)
        {
            StartCoroutine(Dash());
            playerAnimator.SetBool("BoolDash", true);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            playerAnimator.SetBool("BoolDash", false);
        }


    }

    void Jump()
    {
        bool IsGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        if (IsGrounded)
        {
            isJumping = false;
            canJump = true;
            transSpeed = 5f;
            playerAnimator.SetBool("BoolJump", false);
        }
        else if (canJump && Input.GetKey(KeyCode.Space))
        {
            isJumping = true;
            canJump = false;
            transSpeed = 3f;
            playerAnimator.SetBool("BoolJump", true);
        }
        else if (canJump = false || Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = true;
            canJump = false;
            transSpeed = 3f;
            playerAnimator.SetBool("BoolJump", false);
        }
    }

    IEnumerator Dash()
    {
        playerAnimator.SetBool("BoolRun", false);
        canDash = false;
        isDashing = true;
        tm = Time.time;
        yield return new WaitForSeconds(animDashTime);
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        if (spriteRenderer.flipX == true)
        {
            rb.velocity = Vector2.right * dashSpeed;
        }
        else
        {
            rb.velocity = Vector2.left * dashSpeed;
        }
        yield return new WaitForSeconds(dashingTime);
        rb.gravityScale = originalGravity;
        isDashing = false;
        rb.velocity = new Vector2(transform.localScale.x * 0, 0f);
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    IEnumerator Attack1()
    {
        playerAnimator.SetBool("BoolRun", false);
        canAttack = false;
        tm = Time.time;
        yield return new WaitForSeconds(animAttackTime);
        isAttacking = true;
        rb.gravityScale = 0f;
        if (spriteRenderer.flipX == true)
        {
            rb.velocity = Vector2.right * attackSpeed;
        }
        else
        {
            rb.velocity = Vector2.left * attackSpeed;
        }
        canAttack = true;
        yield return new WaitForSeconds(attack1Time);
        rb.gravityScale = 1f;
        isAttacking = false;
        rb.velocity = new Vector2(transform.localScale.x * 0, 0f);
        playerAnimator.SetBool("BoolAttack", false);
    }

    IEnumerator Attack2()
    {
        isAttacking = true;
        isEnnemyTouch = false;
        playerAnimator.SetBool("BoolRun", false);
        canAttack = false;
        isCombo = true;
        tm = Time.time;
        rb.gravityScale = 0f;
        canAttack = true;
        yield return new WaitForSeconds(attack2Time);
        rb.gravityScale = 1f;
        isAttacking = false;
        isCombo = false;
        playerAnimator.SetBool("BoolAttack", false);
    }

    IEnumerator Attack3()
    {
        isAttacking = true;
        isEnnemyTouch = false;
        playerAnimator.SetBool("BoolRun", false);
        canAttack = false;
        isCombo = true;
        tm = Time.time;
        rb.gravityScale = 0f;
        yield return new WaitForSeconds(attack2Time);
        rb.gravityScale = 1f;
        yield return new WaitForSeconds(attackCooldown);
        isAttacking = false;
        isCombo = false;
        canAttack = true;
    }

    IEnumerator JumpAttack()
    {
        isAttacking = true;
        playerAnimator.SetBool("BoolRun", false);
        canAttack = false;
        tm = Time.time;
        rb.gravityScale = 0f;
        yield return new WaitForSeconds(animJumpAttackTime);
        rb.gravityScale = 1f;
        rb.velocity = new Vector2(transform.localScale.x * 0, 0f);
        isAttacking = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}