using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLvlSpawn : MonoBehaviour
{
    void OnTriggerEnter2D()
    {
        GameObject.FindGameObjectWithTag("Player").transform.position = transform.position;
    }
}
