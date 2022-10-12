using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject shootPrefab;
    public GameObject shootPoint;
    public float delayTime;
    private Animator animator;
    private bool isShoot;
    private float timerShoot;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isShoot)
        {
            isShoot = true;
            Instantiate(shootPrefab, shootPoint.transform.position, Quaternion.identity);
        }
        else
        {
            timerShoot += Time.deltaTime;
            if (timerShoot > delayTime)
            {
                isShoot = false;
                timerShoot = 0;
            }
        }
        //animator.SetBool("Shoot", isShoot);
    }
}
