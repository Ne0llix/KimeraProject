using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FovEnnemy : MonoBehaviour
{
    [SerializeField] public bool alert;
    [SerializeField] Ennemy ennemy;

    private void Awake()
    {
        alert = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            alert = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(waiting());
            alert = false;
        }
    }

        public IEnumerator waiting()
    {
        ennemy.isWaiting = true;
        yield return new WaitForSeconds(ennemy.wait);
        ennemy.isWaiting = false;
    }
}
