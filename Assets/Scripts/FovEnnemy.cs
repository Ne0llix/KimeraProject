using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FovEnnemy : MonoBehaviour
{
    [SerializeField] public bool alert;
    [SerializeField] Animator EnnemyAnimator;

    private void Awake()
    {
        alert = false;
    }

    private void Update()
    {
        if (alert == true)
        {
            EnnemyAnimator.SetBool("BoolRun", true);
        } else if (alert == false)
        {
            EnnemyAnimator.SetBool("BoolRun", false);
        }
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
            alert = false;
        }
    }
}
