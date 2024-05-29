using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemy : MonoBehaviour
{
    [SerializeField] FovEnnemy fovE;
    [SerializeField] Animator EnnemyAnimator;
    [SerializeField] private Rigidbody2D rb;

    public float speedRun;
    float speed = 0;
    public Transform[] waypoint;

    public int damageOnCollision;

    public SpriteRenderer spriteEnnemy;
    private Transform target;
    private int destPoint = 0;

    // Start is called before the first frame update
    void Start()
    {
        target = waypoint[1];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = target.position - transform.position;

        if (fovE.alert == true){
            if (spriteEnnemy.flipX = false){
                spriteEnnemy.flipX = true;
                spriteEnnemy.flipX = !spriteEnnemy.flipX;
            }
            speed = speedRun;
            destPoint = 0;
            target = waypoint[0];
            EnnemyAnimator.SetBool("BoolRun", true);
            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        }

        //Si Player n'est plus dans le FOV de l'Ennemy, alors celui-ci retourne à sa position
        if (fovE.alert == false){
            if (spriteEnnemy.flipX = true){
                spriteEnnemy.flipX = false;
                spriteEnnemy.flipX = !spriteEnnemy.flipX;
            }
            destPoint = 1;
            target = waypoint[destPoint];
            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

            if (Vector3.Distance(transform.position, target.position) < 0.1f){
                speed = 0;
                spriteEnnemy.flipX = true;
                spriteEnnemy.flipX = !spriteEnnemy.flipX;
                EnnemyAnimator.SetBool("BoolRun", false);
            }
        }
    }
}
