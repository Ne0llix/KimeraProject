using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Transform checkpoint;
    [SerializeField] bool checkpointActive;

    void Awake()
    {
        checkpoint = GameObject.FindGameObjectWithTag("Player").transform;
        checkpointActive = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        checkpoint.position = transform.position;
        checkpointActive = true;
    }
}
