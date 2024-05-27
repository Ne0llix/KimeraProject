using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FovEnnemy : MonoBehaviour
{
    [SerializeField] public float fov;
    [Range(0, 360)] public float fovAngle; //in degrees
    public bool alert;

    private void Awake()
    {
        alert = false;
    }

    private void Update()
    {
        bool playerInFOV = false;
        Collider2D targetsInFOV = Physics2D.OverlapCircle(transform.position, fov);

        if (targetsInFOV.CompareTag("Player"))
        {
            float signedAngle = Vector3.Angle(
                -transform.right,
                targetsInFOV.transform.position - transform.position);
            if (Mathf.Abs(signedAngle) < fovAngle / 2)
            {
                playerInFOV = true;
                alert = true;
            }
        }
    }
}
