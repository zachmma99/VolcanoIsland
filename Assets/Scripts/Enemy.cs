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
    }
}
