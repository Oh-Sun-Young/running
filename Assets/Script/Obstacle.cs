using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public GameObject[] obstacles;

    private void OnEnable()
    {
        int num = Random.Range(0, obstacles.Length);
        for (int i = 0; i < obstacles.Length; i++)
        {
            if(i == num)
            {
                obstacles[i].SetActive(true);
            }
            else
            {
                obstacles[i].SetActive(false);
            }
        }
    }
}
