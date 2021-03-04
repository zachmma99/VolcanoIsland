using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public float shootSpeed;   
     private Player player;
    private Enemy enemy;
    public GameObject hitEffect;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = player.transform.right;
        this.transform.Translate(direction * shootSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            //Instantiate(hitEffect, this.transform.position, Quaternion.identity);

            GameObject.Destroy(this.gameObject);
        }
        if (other.tag == "Ground")
        {
            Instantiate(hitEffect, this.transform.position, Quaternion.identity);

            GameObject.Destroy(this.gameObject);
        }
    }
}
