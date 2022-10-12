using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI textScore;

    void Update()
    {
        textScore.text = "" + (Data.coin > 1000 ? "" : 0) + (Data.coin > 100 ? "" : 0) + (Data.coin > 10 ? "" : 0) + (Data.coin > 0 ? Data.coin : 0);
    }
}
