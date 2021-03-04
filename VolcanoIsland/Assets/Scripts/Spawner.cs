using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] enemies;
    public GameObject[] healthItems;

    public float timeBetweenEnemySpawns;
    public float timeBetweenHealthSpawns;
    private float OGTime;
    public float minSpawnTime;
    public float decreaseAmt;
    private float spawnTimer, healthSpawnTimer;

    private new Camera camera;

    Player player;

    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = 0f;
        healthSpawnTimer = 0f;
        OGTime = timeBetweenEnemySpawns;
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        player = GameManager.instance().playerTag();
    }

    // Update is called once per frame
    void Update()
    {
        float cameraXleft = camera.transform.position.x;
        float cameraXright = cameraXleft + camera.pixelWidth;

        if (!GameManager.instance().isPlayerActive())
        {
            return;
        }

        if (spawnTimer <= 0f)
        {
            GameObject e = enemies[Random.Range(0, enemies.Length)];
            float enemyLocation = Random.Range(-8.5f, 40f);
            Instantiate(e, new Vector3(enemyLocation, 6f, 0f), Quaternion.identity);

            Instantiate(e, new Vector3(player.transform.position.x, 6f, 0f), Quaternion.Euler(0f, 0f, Random.Range(5f, 45f)));

            timeBetweenEnemySpawns -= decreaseAmt;
            if (timeBetweenEnemySpawns < minSpawnTime)
            {
                timeBetweenEnemySpawns = minSpawnTime;
            }

            spawnTimer = timeBetweenEnemySpawns;
        }
        else
        {
            spawnTimer -= Time.deltaTime;
        }

        if (healthSpawnTimer <= 0f)
        {
            healthSpawn();

            timeBetweenHealthSpawns -= decreaseAmt;
            if (timeBetweenHealthSpawns < minSpawnTime)
            {
                timeBetweenHealthSpawns = minSpawnTime;
            }

            healthSpawnTimer = timeBetweenHealthSpawns;
        }
        else
        {
            healthSpawnTimer -= Time.deltaTime;
        }
        
    }

    void healthSpawn()
{
        GameObject hI = healthItems[Random.Range(0, healthItems.Length)];
        float locationHI = Random.Range(-8.5f, 8.5f);
        Instantiate(hI, new Vector3(locationHI, 6f, 0f), Quaternion.identity);
    }

    public void reset()
    {
        timeBetweenEnemySpawns = OGTime;
    }
}
