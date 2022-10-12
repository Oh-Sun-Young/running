using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed;
    void Update()
    {
        if (transform.position.x > -Screen.width) transform.Translate(Vector2.left * speed * Time.deltaTime);
        else Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Animator>().SetTrigger("Die");
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
