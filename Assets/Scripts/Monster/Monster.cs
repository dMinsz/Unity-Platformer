using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    protected new Rigidbody2D rigidbody;
    protected Animator animator;
    protected new Collider2D collider;
    protected new SpriteRenderer renderer;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
        renderer = GetComponent<SpriteRenderer>();
    }

    protected virtual void Die()
    {
        rigidbody.gravityScale = 1.0f;
        rigidbody.velocity = Vector2.up * 3;
        animator.SetBool("Die", true);
        collider.enabled = false;

        Destroy(gameObject, 3f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            Rigidbody2D playerRigidbody = playerController.GetComponent<Rigidbody2D>();
            if (playerController.transform.position.y - 0.8f > transform.position.y)
            {
                playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, 10);
                Die();
            }
            else
            {
                int dirX = playerController.transform.position.x < transform.position.x ? -1 : 1;
                playerRigidbody.velocity = new Vector2(dirX * 3, 8);
                playerController.Hit();
            }
        }
    }
}
