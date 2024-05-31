using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LCollision : MonoBehaviour
{
    [SerializeField] Damages damages;
    private void OnTriggerEnter2D(Collider2D Lcollision)
    {
        if (Lcollision.CompareTag("LCol"))
        {
            damages.isLCol = true;
        }
    }
    private void OnTriggerExit2D(Collider2D Lcollision)
    {
        if (Lcollision.CompareTag("LCol"))
        {
            damages.isLCol = false;
        }
    }
}
