using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    //two speeds to pick a random value between
    public float minSpeed;
    public float maxSpeed;

    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        //pick random balue between min and max speed
        speed = Random.Range(minSpeed, maxSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        //translate this object down at speed
        this.transform.Translate(Vector3.down * speed * Time.deltaTime);
    }
}
