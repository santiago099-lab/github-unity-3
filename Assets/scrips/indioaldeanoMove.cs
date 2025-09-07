using UnityEngine;

public class indioaldeanoMove : MonoBehaviour
{

    public float runspeed = 2;

    public float jumpspeed = 4;

    Rigidbody2D rb2D;

    public bool betterJump = false;

    public float fallMultiplier = 0.5f;

    public float lowJumpMultiplier = 1f;

    public SpriteRenderer spriteRenderer;

    public Animator animator;

    public void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
       if (Input.GetKey("d") || Input.GetKey("right"))
        {
            rb2D.linearVelocity = new Vector2(runspeed, rb2D.linearVelocityY);
            spriteRenderer.flipX = false;
            animator.SetBool("Run", true);
        }
       else if (Input.GetKey("a") || Input.GetKey("left"))
        {
            rb2D.linearVelocity = new Vector2(-runspeed, rb2D.linearVelocityY);
            spriteRenderer.flipX = true;
            animator.SetBool("Run", true);
        }

        else
        {
            rb2D.linearVelocity = new Vector2(0, rb2D.linearVelocityY);
            animator.SetBool("Run", false);
        }
       if (Input.GetKey(KeyCode.Space) && CheckGround.isGrounded)
        {
            rb2D.linearVelocity = new Vector2(rb2D.linearVelocityX, jumpspeed);
        }

       if(CheckGround.isGrounded==false)
        {
            animator.SetBool("Jump", true);
            animator.SetBool("Run", false);
        }
       if(CheckGround.isGrounded==true)
        {
            animator.SetBool("Jump", false);
        }

       if (betterJump)
          {
                if (rb2D.linearVelocityY < 0)
                {
                 rb2D.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier) * Time.deltaTime;
                }
               if (rb2D.linearVelocityY > 0 && !Input.GetKey(KeyCode.Space))
                {
                 rb2D.linearVelocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier) * Time.deltaTime;
                }
        }
    }
}
