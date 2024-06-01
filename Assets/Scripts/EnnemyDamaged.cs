using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyDamaged : MonoBehaviour
{
    public int PV;
    [SerializeField] MoveChara MC;
    [SerializeField] Ennemy ennemy;
    public GameObject objectToDestroy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (MC.isAttacking && collision.transform.CompareTag("Hitbox"))
        {
            MC.isEnnemyTouch = true;
            StartCoroutine(ennemy.FeedbackCollision());
            PV -= 1;
            if (PV <= 0)
            {
                Destroy(objectToDestroy);
            }
        }
    }
}
