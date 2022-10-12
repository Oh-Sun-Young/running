using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimation : MonoBehaviour
{
    public float speed;
    public GameObject[] cellAnimation;

    int num;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        num = 0;
        CellAnimation();
    }
    void CellAnimation()
    {
        for (int i = 0; i < cellAnimation.Length; i++)
        {
            if (i != num) cellAnimation[i].SetActive(false);
            else cellAnimation[i].SetActive(true);
        }
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.unscaledDeltaTime;
        if (timer > speed)
        {
            timer = 0;
            if (num < cellAnimation.Length - 1)
            {
                num++;
            }
            else
            {
                num = 0;
            }
            CellAnimation();
        }
    }
}
