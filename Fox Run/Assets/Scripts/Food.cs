using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Xml.Serialization;
using UnityEngine;

public class Food : MonoBehaviour
{
    public GameObject foodPrefab;//음식들
    public int count = 5;

    public float timeBetSpawnMin = 1f;
    public float timeBetSpawnMax = 2f;
    private float timeBetSpawn;

    public float yMin = -2f;//배치할 y최솟값
    public float yMax = 3f;
    private float xPos = 10f;

    private GameObject[] foods;
    private Vector2 poolPosition = new Vector2(0, -25);
    private float lastSpawnTime;
    private int currentIndex = 0;


    // Start is called before the first frame update
    void Start()
    {
        foods = new GameObject[count];
        for(int i = 0; i < count; i++)
        {
            foods[i] = Instantiate(foodPrefab, poolPosition, Quaternion.identity);
        }
        lastSpawnTime = 0f;
        timeBetSpawn = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isGameover) { return; }
        if (Time.time >= lastSpawnTime + timeBetSpawn) 
        {
            lastSpawnTime = Time.time;// 기록된 마지막 배치 시점을 현재 시점으로 갱신
            timeBetSpawn = Random.Range(timeBetSpawnMin, timeBetSpawnMax);
            float yPos = Random.Range(yMin, yMax);
            foods[currentIndex].SetActive(false);
            foods[currentIndex].SetActive(true);
            foods[currentIndex].transform.position = new Vector2(xPos, yPos);
            currentIndex++;
            if (currentIndex >= count)
            {
                currentIndex = 0;
            }
        }
    }
}
