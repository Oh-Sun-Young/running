using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextAnimation : MonoBehaviour
{
    float timer;
    float timerMax;
    public TextMeshProUGUI TextInfo;
    void Update()
    {
        timer += Time.deltaTime;
        if (TextInfo.gameObject.activeSelf) timerMax = 0.75f;
        else timerMax = 0.25f;
        if (timer > timerMax)
        {
            timer = 0;
            if (TextInfo.gameObject.activeSelf) TextInfo.gameObject.SetActive(false);
            else TextInfo.gameObject.SetActive(true);
        }
    }
}
