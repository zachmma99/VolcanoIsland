using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Enemy : MonoBehaviour
{

    //reference to the player script
    Player p;

    public int damage;

    //two speeds to pick a random value between
    public float minSpeed;
    public float maxSpeed;

    private float speed;

    //death particle effects
    public GameObject deathEffect;

    // Start is called before the first frame update
    void Start()
    {
        //pick random balue between min and max speed
        speed = Random.Range(minSpeed, maxSpeed);

        p = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        //translate this object down at speed
        this.transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            //reduce player health
            p.takeDamage(damage);
            GameObject.Destroy(gameObject);
        }

        //destroy enemy if hits the ground
        if (collision.tag == "Ground") {
            Instantiate(deathEffect, new Vector3(transform.position.x, transform.position.y, 0f), Quaternion.identity);
            GameObject.Destroy(gameObject);

        }
    }
}
