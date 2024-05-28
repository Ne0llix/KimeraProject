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
    [SerializeField] bool isDamage;
    [SerializeField] float animDamageTime = 0.4f;
    [SerializeField] float damageCooldown = 0.7f;

    [SerializeField] float animDeathTime = 0.4f;

    [SerializeField] float tm;

    [SerializeField] bool noMove = false;

    void Awake()
    {
        PV = 4;
    }

    // Update is called once per frame
    void Update()
    {
        DamageControl();
        Damage();
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Ennemy"))
        {
            StartCoroutine(Damage());
        }
    }

    void DamageControl()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1) && canBeDamage && noMove == false)
        {
            StartCoroutine(Damage());
        }
    }

    IEnumerator Damage()
    {
        canBeDamage = false;
        isDamage = true;
        tm = Time.time;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        playerAnimator.SetTrigger("TriggerDamage");
        PV -= 1;
        dam += 1;
        damageAnimator.SetInteger("IntDamage", dam);
        if (spriteRenderer.flipX == true)
        {
            rb.velocity = Vector2.left;
        }
        else
        {
            rb.velocity = Vector2.right;
        }
        yield return new WaitForSeconds(animDamageTime);
        rb.gravityScale = originalGravity;
        isDamage = false;
        yield return new WaitForSeconds(damageCooldown);
        canBeDamage = true;
        if (PV <= 0)
        {
            canBeDamage = false;
            isDamage = true;
            tm = Time.time;
            playerAnimator.SetTrigger("TriggerDeath");
            yield return new WaitForSeconds(animDeathTime);
            StartCoroutine(loadScene());
        }
    }

    public IEnumerator loadScene()
    {
        fadeAnimator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);
    }
}
