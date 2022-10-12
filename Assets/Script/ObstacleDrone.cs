using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDrone : MonoBehaviour
{
    private void OnEnable()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).position = new Vector3(15f, transform.GetChild(i).position.y, 10);
            transform.GetChild(i).gameObject.SetActive(false);
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }
}
