using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Damages : MonoBehaviour
{
    [SerializeField] float transSpeed = 5f;
    [SerializeField] Animator playerAnimator;
    [SerializeField] Animator damageAnimator;
    [SerializeField] Animator fadeAnimator;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D rb;

    public string sceneName;
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
        noMove = false;
        isDamage = false;
        PV = 4;
    }

    // Update is called once per frame
    void Update()
    {
        DamageControl();
        Damage();
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
        PV -= PVLess;
        dam += PVLess;
        StartCoroutine(Damage());
    }

    public IEnumerator Damage()
    {
        StopMoving();
        canBeDamage = false;
        isDamage = true;
        tm = Time.time;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        playerAnimator.SetTrigger("TriggerDamage");
        damageAnimator.SetInteger("IntDamage", dam);
        if (spriteRenderer.flipX == true)
        {
            rb.velocity = Vector2.left;
        }
        else
        {
            rb.velocity = Vector2.right;
        }
        if (PV == 0)
        {
            dam = 0;
            yield return new WaitForSeconds(animDamageTime);
            rb.gravityScale = originalGravity;
            isDamage = false;
            yield return new WaitForSeconds(damageCooldown);
            tm = Time.time;
            playerAnimator.SetTrigger("TriggerDeath");
            yield return new WaitForSeconds(animDeathTime);
            StartCoroutine(loadScene());
            yield return new WaitForSeconds(0.35f);
            damageAnimator.SetInteger("IntDamage", dam);
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

    public IEnumerator loadScene()
    {
        fadeAnimator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);
    }

        public void StopMoving()
    {
        noMove = true;
    }

    public void RePlayMove()
    {
        noMove = false;
    }
}
