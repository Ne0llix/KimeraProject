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

    // Start is called before the first frame update
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
        fadeAnimator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);
        playerAnimator.SetBool("BoolRun", false);
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
