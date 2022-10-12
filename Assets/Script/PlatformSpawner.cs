using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject[] platformPrefab;
    public float[] timeBetSpawn;

    private float yPos = -2.8f;
    private float xPos = 15f;

    private GameObject[] platforms;
    //public GameObject[] platformsTemp;
    public ArrayList platformsTemp = new ArrayList();
    public int jumpPlatformStart;
    public int cnt;
    public int currentIndex;

    private Vector3 poolPosition = new Vector3(0, -10, 10);
    private float lastSpawnTime;
    private float delaySpawnTime = 7.9f;
    // Start is called before the first frame update
    void Start()
    {
        platforms = new GameObject[platformPrefab.Length];
        for (int i =0; i < platformPrefab.Length; i++)
        {
            platforms[i] = Instantiate(platformPrefab[i], poolPosition, Quaternion.identity);
            platforms[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (GameManager.instance.isGameOver)
        {
            return;
        }

        if(lastSpawnTime == 0)
        {
            lastSpawnTime = Time.time;
        }

        if(Time.time >= lastSpawnTime + delaySpawnTime)
        {
            currentIndex = Random.Range(((currentIndex == 0) ? 1 : 0 ), (currentIndex < jumpPlatformStart - 1) ? jumpPlatformStart : jumpPlatformStart - 2);

            lastSpawnTime = Time.time;
            delaySpawnTime = timeBetSpawn[currentIndex];

            if (platforms[currentIndex].transform.position.x > -20 && platforms[currentIndex].transform.position.y == yPos)
            {
                GameObject obj = (GameObject)Instantiate(platformPrefab[currentIndex], new Vector3(xPos, yPos, 10), Quaternion.identity);
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
}
