using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    private int type;
    [SerializeField] GameObject[] characterType;

    void Start()
    {
        type = Data.instance.characterType;

        for (int i = 0; i < characterType.Length; i++)
        {
            characterType[i].SetActive((i == type) ? true : false);
        }
    }
}
