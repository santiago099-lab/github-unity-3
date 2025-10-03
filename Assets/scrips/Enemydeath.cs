using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

public class Enemydeath : MonoBehaviour
{
    public float bounceForce = 10f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Colision detectada con: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Es el jugador!");

            Vector2 normal = collision.contacts[0].normal;
            Debug.Log("Normal de colisión: " + normal);

            if (normal.y <= -0.3f)
            {
                Debug.Log("Colisión desde arriba, enemigo muere.");
                Die();
                Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
                if (playerRb != null)
                {
                    Vector2 newVelocity = playerRb.linearVelocity;
                    newVelocity.y = bounceForce;
                    playerRb.linearVelocity = newVelocity;
                }
            }
            else
            {
                Debug.Log("Jugador no golpea desde arriba. Implementar daño al jugador aqui.");
            }

        }
    }

    void Die()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (transform.position.y < -10f)
        {
            Die();
        }
    }
}
