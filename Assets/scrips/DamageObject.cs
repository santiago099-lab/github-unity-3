using UnityEngine;

public class DamageObject : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.transform.CompareTag("Player"))
        {
            Debug.Log("player Damaged");
            Destroy(collision.gameObject);

            FindFirstObjectByType<GameManager>().PlayerDied();
        }

    }

}
