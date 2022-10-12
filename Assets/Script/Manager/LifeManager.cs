using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour
{
    public static LifeManager instance
    {
        get
        {
            if(m_instance == null)
            {
                m_instance = FindObjectOfType<LifeManager>();
            }
            return m_instance;
        }
    }
    private static LifeManager m_instance;
    private int life;
    private bool check;

    private void Awake()
    {
        if(instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        life = Data.instance.characterLife;

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive((i < life) ? true : false);
        }
    }

    public int GetLife()
    {
        return life;
    }

    public void PlayerDeath()
    {
        if(0 < life && !check)
        {
            life--;
            Transform obj = transform.GetChild(life);
            StartCoroutine(LifeDestory(obj));
        }
    }

    IEnumerator LifeDestory(Transform obj, float delay = 0.05f)
    {
        check = true;

        for (int i = 0; i < obj.childCount; i++)
        {
            for (int j = 0; j < obj.childCount; j++)
            {
                obj.GetChild(j).gameObject.SetActive((i == j) ? true : false);
            }

            yield return new WaitForSeconds(delay);
        }

        Destroy(obj.gameObject);

        if (life == 0)
        {
            yield return new WaitForSeconds(0.2f);
            GameManager.instance.OnPlayerDead();
        }

        check = false;
    }
}
