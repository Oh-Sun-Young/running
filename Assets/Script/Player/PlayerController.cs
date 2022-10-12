using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static bool isJump;
    public static bool isAttack;

    public float jumpForce;
    public GameObject shootPoint;
    public GameObject shootPrefab;

    private int jumpCount = 0;
    private bool isJumpCount;
    private bool isGrounded = false;
    public bool isDead = false;
    private bool isShoot = false;
    private float timerShoot = 0;
    private Rigidbody2D playerRigidbody;
    public Animator[] animator;

    private int type;

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        isJumpCount = true;

        type = Data.instance.GetCharacterType();
    }
    private void Update()
    {
        if (isDead) return;

        if(transform.position.x < -5)
        {
            transform.Translate(Vector2.right * 3 * Time.deltaTime);
        }

        for (int i = 0; i < animator.Length; i++)
        {
            if (i == type) animator[i].gameObject.SetActive(true);
            else animator[i].gameObject.SetActive(false);
        }

        if (isJump && jumpCount < 2)
        {
            if (isJumpCount)
            {
                jumpCount++;
                isJumpCount = false;
                playerRigidbody.velocity = Vector2.zero;
                playerRigidbody.AddForce(new Vector2(0, jumpForce));
            }

            animator[type].SetBool("Shoot", false);
        }
        else if (!isJump && playerRigidbody.velocity.y > 0)
        {
            isJumpCount = true;
            playerRigidbody.velocity *= 0.5f;
        }
        if (isAttack)
        {
            animator[type].SetBool("Shoot", true);
            if (!isShoot)
            {
                isShoot = true;
                Instantiate(shootPrefab, shootPoint.transform.position, Quaternion.identity);
            }
            else
            {
                timerShoot += Time.deltaTime;
                if (timerShoot > 0.1f)
                {
                    isShoot = false;
                    timerShoot = 0;
                }
            }
        }
        else if (!isAttack)
        {
            animator[type].SetBool("Shoot", false);
        }
        animator[type].SetBool("Grounded", isGrounded);
    }

    void Die()
    {
        LifeManager.instance.PlayerDeath();

        if (LifeManager.instance.GetLife() == 0)
        {
            isDead = true;
        }
    }

    IEnumerator FallDeath()
    {
        Rigidbody2D myRigidbody = GetComponent<Rigidbody2D>();

        yield return new WaitForSeconds(1f);

        transform.position = new Vector3(-13, -1.16f, 10);

        yield return new WaitForSeconds(0.1f);
        myRigidbody.simulated = false;
        animator[type].SetBool("Grounded", true);

        while (transform.position.x < -5)
        {
            yield return null;
        }

        myRigidbody.simulated = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Coin")
        {
            Data.coin += 1;
            other.gameObject.SetActive(false);
        }

        if ((other.gameObject.tag == "Enemy") && !isDead)
        {
            Die();
        }

        if ((other.gameObject.tag == "Dead") && !isDead)
        {
            Die();
            StartCoroutine("FallDeath");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.contacts[0].normal.y > 0.7f)
        {
            isGrounded = true;
            jumpCount = 0;
        }
        if(collision.gameObject.tag == "Floor")
        {
            isJumpCount = true;
        }

        if (collision.gameObject.tag == "Enemy" && !isDead)
        {
            Die();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(isJump) isGrounded = false;
    }
}
