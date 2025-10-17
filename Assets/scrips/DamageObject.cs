using UnityEngine;

public class DamageObject : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.transform.CompareTag("Player"))
        {
            Debug.Log("Pincho toco al jugador - llamado a PlayerDamage");
            collision.gameObject.GetComponent<PlayerRespawn>().PlayerDamaged();

            FindFirstObjectByType<GameManager>().PlayerDied();
        }

    }

}
