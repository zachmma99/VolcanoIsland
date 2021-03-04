/*
Copyright 2020 Micah Schuster

Redistribution and use in source and binary forms, with or without modification,
are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this
list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice,
this list of conditions and the following disclaimer in the documentation and/or
other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED.
IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT,
INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING,
BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF
LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE
OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF
ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] enemies;
    public GameObject[] healthItems;

    private Transform player;

    public float minSpawnTime;
    public float maxSpawnTime;
    public float timeBetweenEnemySpawns;
    public float timeBetweenHealthSpawns;
    public float decreaseAmt;

    private float enemySpawnTimer;
    private float healthSpawnTimer;
    private float OGTime;

    void Start()
    {
        enemySpawnTimer = 0f;
        healthSpawnTimer = 0f;
        OGTime = timeBetweenEnemySpawns;
        player = GameManager.instance().playerPosition();
    }

    void Update()
    {
        if (!GameManager.instance().isPlayerActive())
        {
            return;
        }

        if (enemySpawnTimer <= 0f)
        {
            GameObject e = enemies[Random.Range(0, enemies.Length)];
            float eLocation = Random.Range(-8.5f, 60f);
            Instantiate(e, new Vector3(eLocation, 6f, 0f), Quaternion.identity);

            float smartLocation = Random.Range(player.position.x, player.position.x + 5);
            Instantiate(e, new Vector3(smartLocation, 6f, 0f), Quaternion.Euler(0f, 0f, Random.Range(5f, 45f)));

            timeBetweenEnemySpawns -= decreaseAmt;
            if (timeBetweenEnemySpawns < minSpawnTime) 
            {
                timeBetweenEnemySpawns = maxSpawnTime;
            }

            enemySpawnTimer = timeBetweenEnemySpawns;
        } 
        else 
        {
            enemySpawnTimer -= Time.deltaTime;
        }

        if (healthSpawnTimer <= 0f)
        {
            GameObject hI = healthItems[Random.Range(0, healthItems.Length)];
            float value = Random.Range(-8.5f, 60f);
            Vector3 position = new Vector3(value, 6.5f, 0f);
            Instantiate(hI, new Vector3(0f, 6f, 0f), Quaternion.identity);

            healthSpawnTimer = timeBetweenHealthSpawns;
        }
        else 
        {
            healthSpawnTimer -= Time.deltaTime;
        }

    }

    public void reset()
    {
        timeBetweenEnemySpawns = OGTime;
    }
}
