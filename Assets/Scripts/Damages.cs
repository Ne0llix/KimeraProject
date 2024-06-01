using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Damages : MonoBehaviour
{
    [SerializeField] public bool tpEnnemy;

    [SerializeField] Animator playerAnimator;
    [SerializeField] Animator damageAnimator;
    [SerializeField] Animator fadeAnimator;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] public bool isLCol = false;
    [SerializeField] public bool isDead = false;
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] FallingZone FZ;

    [SerializeField] int PV;
    [SerializeField] int dam = 0;
    [SerializeField] bool canBeDamage = true;
    [SerializeField] public bool isDamage;
    [SerializeField] float animDamageTime = 0.4f;
    [SerializeField] float damageCooldown = 0.7f;

    [SerializeField] float animDeathTime = 1.2f;

    [SerializeField] float tm;

    [SerializeField] public bool noMove;

    void Awake()
    {
        tpEnnemy = false;
        noMove = false;
        isDamage = false;
        PV = 4;
    }

    // Update is called once per frame
    void Update()
    {
        DamageControl();
        if (isDead == true)
        {
            return;
        }
    }

    void DamageControl()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1) && canBeDamage)
        {
            TakeDamage(1);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            PV += 1;
            dam -= 1;
            damageAnimator.SetInteger("IntDamage", dam);
        }
    }

    public void TakeDamage(int PVLess)
    {
        if(canBeDamage == true)
        {
            PV -= PVLess;
            dam += PVLess;
            StartCoroutine(Damage());
        }
    }

    public IEnumerator Damage()
    {
        StopMoving();
        playerAnimator.SetBool("BoolRun", false);
        canBeDamage = false;
        isDamage = true;
        tm = Time.time;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        playerAnimator.SetTrigger("TriggerDamage");
        damageAnimator.SetInteger("IntDamage", dam);
        if (isLCol == true)
        {
            if (spriteRenderer.flipX == true)
            {
                spriteRenderer.flipX = false;
                rb.velocity = Vector2.right;
            }
            else if (spriteRenderer.flipX == false)
            {
                rb.velocity = Vector2.right;
            }
        }
        else if (isLCol == false)
        {
            if (spriteRenderer.flipX == true)
            {
                rb.velocity = Vector2.left;
            }
            else if (spriteRenderer.flipX == false)
            {
                spriteRenderer.flipX = true;
                rb.velocity = Vector2.left;
            }
        }
            if (PV <= 0)
        {
            rb.gravityScale = originalGravity;
            StartCoroutine(Death());
            MenuManager.instance.EndMenu();
        }
        else if (PV > 0)
        {
            yield return new WaitForSeconds(animDamageTime);
            rb.gravityScale = originalGravity;
            isDamage = false;
            RePlayMove();
            yield return new WaitForSeconds(damageCooldown);
            canBeDamage = true;
        } 
    }

    public void StopMoving()
    {
        noMove = true;
    }

    public void RePlayMove()
    {
        noMove = false;
    }

    public IEnumerator Death()
    {
        canBeDamage = false;
        dam = 0;
        yield return new WaitForSeconds(animDamageTime);
        rb.velocity = Vector2.zero;
        isDamage = false;
        yield return new WaitForSeconds(damageCooldown);
        playerAnimator.SetTrigger("TriggerDeath");
        yield return new WaitForSeconds(animDeathTime);
        isDead = true;
        StartCoroutine(replacePlayer());
        yield return new WaitForSeconds(0.35f);
        tpEnnemy = false;
        damageAnimator.SetInteger("IntDamage", dam);
    }

    private IEnumerator replacePlayer()
    {
        fadeAnimator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);
        tpEnnemy = true;
        isDead = false;
        playerAnimator.SetTrigger("Respawn");
        yield return new WaitForSeconds(1f);
        RePlayMove();
        canBeDamage = true;
        PV = 4;
        if (spriteRenderer.flipX == false)
        {
            spriteRenderer.flipX = true;
        }
        transform.position = FZ.playerSpawn.position;
    }
}
