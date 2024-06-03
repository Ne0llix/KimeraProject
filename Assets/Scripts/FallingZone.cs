using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FallingZone : MonoBehaviour
{

    [SerializeField] public Transform playerSpawn;
    public Animator fadeAnimator;
    public Animator playerAnimator;
    public Animator damageAnimator;
    [SerializeField] Damages damages;
    [SerializeField] SpriteRenderer spriteRenderer;

    void Awake()
    {
        playerSpawn = GameObject.FindGameObjectWithTag("PlayerSpawn").transform;
        fadeAnimator = GameObject.FindGameObjectWithTag("FadeSystem").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(replacePlayer(collision));
            MenuManager.instance.EndMenu();
        }
    }

    private IEnumerator replacePlayer(Collider2D collision)
    {
        damages.StopMoving();
        damages.isDead = true;
        damages.canBeDamage = false;
        fadeAnimator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);
        damages.tpEnnemy = true;
        damages.isDead = false;
        playerAnimator.SetBool("BoolRun", false);
        playerAnimator.SetTrigger("Respawn");
        if (spriteRenderer.flipX == false)
        {
            spriteRenderer.flipX = true;
        }
        collision.transform.position = playerSpawn.position;
        yield return new WaitForSeconds(1f);
        damages.isDead = false;
        damages.RePlayMove();
        damages.canBeDamage = true;
    }
}
