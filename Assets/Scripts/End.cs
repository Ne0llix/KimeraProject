using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    public Animator fadeAnimator;
    public Animator playerAnimator;
    public Animator damageAnimator;
    [SerializeField] Damages damages;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(LoadNextScene());
        }
    }

    public IEnumerator LoadNextScene()
    {
        damages.StopMoving();
        damages.canBeDamage = false;
        playerAnimator.SetBool("BoolRun", false);
        fadeAnimator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("END");
    }
}
