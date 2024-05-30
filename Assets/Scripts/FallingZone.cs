using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FallingZone : MonoBehaviour
{

    private Transform playerSpawn;
    private Animator fadeAnimator;
    [SerializeField] Checkpoint point;

    void Awake()
    {
        playerSpawn = GameObject.FindGameObjectWithTag("PlayerSpawn").transform;
        fadeAnimator = GameObject.FindGameObjectWithTag("FadeSystem").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(replacePlayer(collision));
        }
    }

    private IEnumerator replacePlayer(Collider2D collision)
    {
        fadeAnimator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);
        collision.transform.position = playerSpawn.position;
    }
}
