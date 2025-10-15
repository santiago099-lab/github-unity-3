using UnityEngine;

public class DamageObject : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.transform.CompareTag("Player"))
        {
           collision.gameObject.GetComponent<PlayerRespawn>().PlayerDamaged();

            FindFirstObjectByType<GameManager>().PlayerDied();
        }

    }

}
