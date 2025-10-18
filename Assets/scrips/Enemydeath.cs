using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

public class Enemydeath : MonoBehaviour
{
    public Collider2D enemyCollider;

    public Animator animator;

    public SpriteRenderer spriteRenderer;

    public GameObject destroyPacticle;

    public float jumpForce = 2.5f;

    public int lifes = 2;


    private void Awake()
    {
       
    }

    private void OnEnable()
    {
        lifes = 2;
        spriteRenderer.enabled = true;
        if (destroyPacticle != null)
        {
            destroyPacticle.SetActive(false);
        }

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Colision con: " + collision.gameObject.name + " | Tag: " + collision.gameObject.tag + " | ¿Es Player?: " + collision.transform.CompareTag("Player"));

        if (collision.transform.CompareTag("Player"))
        {
            Debug.Log(">>> CONFIRMADO: Es el jugador - Aplicando Hit <<<");
            collision.gameObject.GetComponent<Rigidbody2D>().linearVelocity = Vector2.up * jumpForce;
            LosseLifeAndHit();
            CheckLife();
        }
        else
        {
          Debug.Log(">>> No es el jugador - Ignorando colision <<<");
        }
    }

    public void LosseLifeAndHit()
    {
        lifes--;
        animator.Play("Hit");
    }

    public void CheckLife()
    {
        if (lifes <= 0)
        {
           destroyPacticle.SetActive(true);
           spriteRenderer.enabled = false;
           Invoke("EnemyDie", 0.2f);
        }
    }
    public void EnemyDie()
    {
       gameObject.SetActive(false);
    }

}
