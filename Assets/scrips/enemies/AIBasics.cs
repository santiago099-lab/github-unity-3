using System;
using UnityEngine;

public class AIBasics : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 2f;
    public float detectionRange = 5f;

    [Header("Patrol Settings")]
    public Transform pointA;
    public Transform pointB;

    private Transform target;
    private Transform player;
    private Rigidbody2D rb2D;
    private SpriteRenderer spriteRenderer;
    private bool movingToB = true;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = pointB;
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRange)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    { 
        transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, target.position) < 0.1f)
        {
            movingToB = !movingToB;
            target = movingToB ? pointB : pointA;
            spriteRenderer.flipX = !movingToB;
        }
    }

    void ChasePlayer()
    {
       Vector2 direction = (player.position - transform.position).normalized;
       transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * 1.5f * Time.deltaTime);
       spriteRenderer.flipX = direction.x < 0;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.PlayerDied();
        }
    }
}