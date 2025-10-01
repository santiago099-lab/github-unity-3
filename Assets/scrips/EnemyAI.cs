using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 2f;
    public float detectionRange = 5f;

    [Header("Patrol Settings")]
    public Transform pointA;
    public Transform pointB;

    private Transform target;
    private Transform player;
    private Rigidbody2D rb2d;
    private SpriteRenderer spriteRenderer;
    private bool movingToB = true;

    void Start()
    {
       rb2d = GetComponent<Rigidbody2D>();
       spriteRenderer = GetComponent<SpriteRenderer>();
       GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        target = pointB;
    }

    void Update()
    {
        if (player == null) return;
        
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer < detectionRange)
        {
            ChacePlayer();
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        if (pointA == null || pointB == null) return;

        transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, target.position) < 0.1f)
        {
            movingToB = !movingToB;
            target = movingToB ? pointB : pointA;
            if (spriteRenderer != null)
            {
                spriteRenderer.flipX = !movingToB;
            }
        }
    }
    void ChacePlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * 1.5f * Time.deltaTime);
        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = direction.x < 0;
        }
    }

   void OnCollisionEnter2D(Collision2D collision)
   {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.PlayerDied();
            }
        }
   }
}
