using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyDamaged : MonoBehaviour
{
    public int PV;
    [SerializeField] MoveChara MC;
    [SerializeField] Damages damages;
    [SerializeField] Ennemy ennemy;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] public float wait;
    public GameObject objectToDestroy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (MC.isAttacking && collision.transform.CompareTag("Hitbox"))
        {
            MC.isEnnemyTouch = true;
            StartCoroutine(FeedbackCollision());
            PV -= 1;
            if (PV <= 0)
            {
                Destroy(objectToDestroy);
            }
        }
    }

    public IEnumerator FeedbackCollision()
    {
        if (damages.isLCol == false)
        {
            ennemy.speed = 0;
            rb.velocity = Vector2.right * ennemy.speedRun * 2;
            yield return new WaitForSeconds(wait);
            ennemy.speed = ennemy.speedRun;
        }
        else if (damages.isLCol == true)
        {
            ennemy.speed = 0;
            rb.velocity = Vector2.left * ennemy.speedRun * 2;
            yield return new WaitForSeconds(wait);
            ennemy.speed = ennemy.speedRun;
        }
    }
}
