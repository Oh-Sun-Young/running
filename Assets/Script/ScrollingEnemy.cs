using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingEnemy : MonoBehaviour
{
    private float speed;
    public float min;
    public float max;

    private void OnEnable()
    {
        speed = Random.Range(min, max);
    }

    private void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
}
