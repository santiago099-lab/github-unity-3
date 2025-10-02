using System;
using UnityEngine;

public class AIBasics : MonoBehaviour
{
    public Animator animator;  

    public SpriteRenderer spriteRenderer;

    private Rigidbody2D rb;

    public float speed = 0.5f;

    private float waitTime;

    public Transform[] moveSpots;  

    public float startWaitTime = 2f;

    private int i = 0;

    private Vector2 actualPos;

    void Start()
    {
        waitTime = startWaitTime;
        actualPos = transform.position;
    }

    void Update()
    {
        Vector2 targetPos = Vector2.MoveTowards(transform.position, moveSpots[i].position, speed * Time.deltaTime);
        GetComponent<Rigidbody2D>().MovePosition(targetPos);

        if (Vector2.Distance(transform.position, moveSpots[i].position) < 0.2f)
        {
            if (waitTime <= 0)
            {
               if (moveSpots[i]!= moveSpots[moveSpots.Length - 1])
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
}