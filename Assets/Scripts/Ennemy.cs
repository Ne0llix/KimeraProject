using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemy : MonoBehaviour
{
    [SerializeField] FovEnnemy fovE;
    [SerializeField] Animator EnnemyAnimator;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] public float wait;
    [SerializeField] public bool isWaiting;

    public float speedRun;
    public float speed = 0;
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
        Movement();
    }

    void Movement()
    {
        if (isWaiting == true)
        {
            speed = 0;
            EnnemyAnimator.SetBool("BoolRun", false);
            spriteEnnemy.flipX = false;
        }
        else
        {
            speed = speedRun;
            EnnemyAnimator.SetBool("BoolRun", true);
            spriteEnnemy.flipX = true;
        }

        Vector3 dir = target.position - transform.position;

        if (fovE.alert == true)
        {
            spriteEnnemy.flipX = false;
            speed = speedRun;
            destPoint = 0;
            target = waypoint[0];
            EnnemyAnimator.SetBool("BoolRun", true);
            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        }

        //Si Player n'est plus dans le FOV de l'Ennemy, alors celui-ci retourne à sa position
        if (fovE.alert == false)
        {
            destPoint = 1;
            target = waypoint[destPoint];
            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

            if (Vector3.Distance(transform.position, target.position) < 0.1f)
            {
                speed = 0;
                spriteEnnemy.flipX = false;
                EnnemyAnimator.SetBool("BoolRun", false);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.transform.CompareTag("Player"))
        {
            Damages damages = collision.transform.GetComponent<Damages>();
            damages.TakeDamage(damageOnCollision);
            if (spriteEnnemy.flipX == false)
            {
                rb.velocity = Vector2.right;
            }
            else if (spriteEnnemy.flipX == true)
            {
                rb.velocity = Vector2.left;
            }
            EnnemyAnimator.SetBool("BoolRun", false);
        }
    }
}
