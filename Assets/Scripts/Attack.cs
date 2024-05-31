using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] MoveChara MC;
    private void OnTriggerEnter2D(Collider2D Lcollision)
    {
        if (Lcollision.CompareTag("Ennemy"))
        {
            MC.isEnnemyTouch = true;
        }
    }
}
