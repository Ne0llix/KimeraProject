using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour
{
    [SerializeField] public Transform playerLvlSpawn;
    public Animator fadeAnimator;
    public Animator playerAnimator;
    public Animator damageAnimator;
    [SerializeField] Damages damages;
    [SerializeField] SpriteRenderer spriteRenderer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(replacePlayer(collision));
        }
    }

    private IEnumerator replacePlayer(Collider2D collision)
    {
        damages.StopMoving();
        damages.canBeDamage = false;
        playerAnimator.SetBool("BoolRun", false);
        fadeAnimator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);
        playerAnimator.SetTrigger("Respawn");
        if (spriteRenderer.flipX == false)
        {
            spriteRenderer.flipX = true;
        }
        collision.transform.position = playerLvlSpawn.position;
        yield return new WaitForSeconds(1f);
        damages.RePlayMove();
        damages.canBeDamage = true;
    }
}
