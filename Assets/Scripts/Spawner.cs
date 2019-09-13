using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //inspector arrays for the spawn location objects and enemy prefabs
    public Transform[] spawnLocations;
    public GameObject[] enemies;

    //public variables to control spawn behavior
    public float timeBetweenSpawns;

    public float minSpawnTime;
    public float decreaseAmt;

    private float spawnTimer;

    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        //stop spawning if there is no character
        if (GameObject.FindGameObjectWithTag("Player") == null || GameObject.FindGameObjectWithTag("Player").activeSelf==false) {
            return;
        }
        if (spawnTimer <= 0) {
            //spawn a enemy
            GameObject enemy = enemies[Random.Range(0, enemies.Length)];
            Transform location = spawnLocations[Random.Range(0, spawnLocations.Length)].transform;
            Instantiate(enemy,new Vector3(location.position.x,location.position.y,0f),Quaternion.identity);

            //increasing difficulty after every spawn
            timeBetweenSpawns -=decreaseAmt;
            if (timeBetweenSpawns < minSpawnTime) {
                timeBetweenSpawns = minSpawnTime;
            }

            spawnTimer = timeBetweenSpawns;
        } else {
            spawnTimer -= Time.deltaTime;
        }

    }

    public void reset(){
        timeBetweenSpawns=1.25f;
    }
}
