using System;
using System.Collections;
using UnityEngine;

public class AIBasics : MonoBehaviour
{
    public Animator animator;

    public SpriteRenderer spriteRenderer;

    public float speed = 0.5f;

    private float waitTime;

    public Transform[] moveSpots;

    public float startWaitTime = 2f;

    private int i = 0;

    private Vector2 actualPos;

    private Rigidbody2D rb2D;

    void Start()
    {
        waitTime = startWaitTime;
        rb2D = GetComponent<Rigidbody2D>();
        StartCoroutine(CheckEnemyMoving());
    }

    void Update()
    {
       
       Vector2 targetPos = moveSpots[i].transform.position;
       Vector2 newPos = Vector2.MoveTowards(rb2D.position, targetPos, speed * Time.deltaTime);

       rb2D.MovePosition(newPos);

        if (Vector2.Distance(transform.position, moveSpots[i].transform.position) < 0.2f)
        {
            if (waitTime <= 0)
            {
                if (moveSpots[i] != moveSpots[moveSpots.Length - 1])
                {
                    i++;
                }
                else
                {
                    i = 0;
                }
                waitTime = startWaitTime;
            }
            else
            {
             waitTime -= Time.deltaTime;
            }
            
        }

    }

    IEnumerator CheckEnemyMoving()
    {
        while (true)
        {
            actualPos = rb2D.position;

            yield return new WaitForSeconds(0.5f);

            if (transform.position.x > actualPos.x)
            {
                spriteRenderer.flipX = true;
                animator.SetBool("idle", false);
            }
            else if (transform.position.x < actualPos.x)
            {
                spriteRenderer.flipX = false;
                animator.SetBool("idle", false);
            }
            else if (transform.position.x == actualPos.x)
            {
                animator.SetBool("idle", true);
            }

        }
    }

}