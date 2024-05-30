using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Transform checkpoint;
    [SerializeField] bool checkpointActive;

    void Awake()
    {
        checkpoint = GameObject.FindGameObjectWithTag("PlayerSpawn").transform;
        checkpointActive = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            checkpoint.position = transform.position;
            checkpointActive = true;
        }
    }
}
