using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointLvl : MonoBehaviour
{
    public Transform checkpointLvl;

    void Awake()
    {
        checkpointLvl = GameObject.FindGameObjectWithTag("PlayerLvlSpawn").transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            checkpointLvl.position = transform.position;
        }
    }
}
