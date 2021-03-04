using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float minSpeed;
    public float maxSpeed;
    public int damage;

    private float speed;

    Player player;
    public GameObject hitEffect;

    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.instance().playerTag();
        speed = UnityEngine.Random.Range(minSpeed, maxSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            player.takeDamage(damage);

            Instantiate(hitEffect, this.transform.position, Quaternion.identity);

            GameObject.Destroy(this.gameObject);
        }
        if (other.tag == "Ground")
        {
            Instantiate(hitEffect, this.transform.position, Quaternion.identity);

            GameObject.Destroy(this.gameObject);
        }
        if (other.tag == "Attack")
        {
            Instantiate(hitEffect, this.transform.position, Quaternion.identity);

            GameObject.Destroy(this.gameObject);
        }
    }
}
