using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float minSpeed;
    public float maxSpeed;
    public int health;

    private float speed;
    private Player player;

    public GameObject healthGroundHit;
    
    // Start is called before the first frame update
    void Start()
    {
        speed = UnityEngine.Random.Range(minSpeed, maxSpeed);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
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
            player.gainHealth(health);

            GameObject.Destroy(this.gameObject);
        }
        if (other.tag == "Ground")
        {
            Instantiate(healthGroundHit, this.transform.position, Quaternion.identity);
            
            GameObject.Destroy(this.gameObject);
        }
        if (other.tag == "Enemy")
        {
            Instantiate(healthGroundHit, this.transform.position, Quaternion.identity);

            GameObject.Destroy(this.gameObject);
        }
    }
}
