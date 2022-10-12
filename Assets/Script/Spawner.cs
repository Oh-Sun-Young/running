using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] EnemyPrefab;
    private float timeMinSpawn = 0;
    private float timeMaxSpawn = 2.5f;

    private float yPos;
    private float xPos = 15f;

    private GameObject[] platforms;
    public ArrayList platformsTemp = new ArrayList();

    public int cnt;
    public int currentIndex;

    private Vector3 poolPosition = new Vector3(0, -10, 10);
    private float lastSpawnTime;
    private float delaySpawnTime = 2.3f;

    void Start()
    {
        platforms = new GameObject[EnemyPrefab.Length];
        for (int i = 0; i < EnemyPrefab.Length; i++)
        {
            platforms[i] = Instantiate(EnemyPrefab[i], poolPosition, Quaternion.identity);
            platforms[i].SetActive(false);
        }
    }

    void Update()
    {

        if (GameManager.instance.isGameOver)
        {
            return;
        }

        if (lastSpawnTime == 0)
        {
            lastSpawnTime = Time.time;
        }

        UpdateSpawner();

        for (int i = 0; i < platformsTemp.Count; i++)
        {
            GameObject obj;
            obj = (GameObject)platformsTemp[i];
            if (obj.transform.position.x <= -20)
            {
                platformsTemp.RemoveAt(i);
                Destroy(obj);
            }
        }
    }

    public virtual void UpdateSpawner()
    {
        if (Time.time >= lastSpawnTime + delaySpawnTime)
        {
            currentIndex = Random.Range(0, platforms.Length);
            delaySpawnTime = Random.Range(timeMinSpawn, timeMaxSpawn);

            lastSpawnTime = Time.time;

            if (platforms[currentIndex].transform.position.x > -20 && platforms[currentIndex].transform.position.y == yPos)
            {
                GameObject obj = (GameObject)Instantiate(EnemyPrefab[currentIndex], new Vector3(xPos, yPos, 10), Quaternion.identity);
                obj.SetActive(false);
                obj.SetActive(true);

                platformsTemp.Add(obj);
            }
            else
            {
                platforms[currentIndex].SetActive(false);
                platforms[currentIndex].SetActive(true);
                platforms[currentIndex].transform.position = new Vector3(xPos, yPos, 10);

                for (int i = 0; i < platforms[currentIndex].transform.childCount; i++)
                {
                    platforms[currentIndex].transform.GetChild(i).gameObject.SetActive(true);
                }
            }
        }
    }
}
