using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

public class Enemydeath : MonoBehaviour
{
    public Collider2D collider2D;

    public Animator animator;

    public SpriteRenderer spriteRenderer;

    public GameObject destroyPacticle;

    public float jumpForce = 2.5f;

    public int lifes = 2;

    private int initialLifes;

    private void Start()
    {
        initialLifes = lifes;
    }

    private void OnEnable()
    {
        lifes = initialLifes;
        spriteRenderer.enabled = true;
        if (destroyPacticle != null)
        {
            destroyPacticle.SetActive(false);
        }

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().linearVelocity = Vector2.up * jumpForce;
            LosseLifeAndHit();
            CheckLife();
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
