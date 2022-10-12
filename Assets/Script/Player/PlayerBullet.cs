using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed;
    GameObject obj;
    void Update()
    {
        if (transform.position.x < Screen.width) transform.Translate(Vector2.right * speed * Time.deltaTime);
        else Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            obj = collision.gameObject;
            collision.gameObject.GetComponent<Animator>().SetTrigger("Die");
            Invoke("EnemyDisable", 0.25f);
            gameObject.SetActive(false);
        }
    }
    void EnemyDisable()
    {
        obj.SetActive(false);
    }
}
