using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public float speed;   //available in the inspector

    Rigidbody2D rb; //needs a rigidbody

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        //geting player input
        float input = Input.GetAxis("Horizontal"); //Has smoothing
        //float input = Input.GetAxisRaw("Horizontal"); //Has no smoothing

        //move player
        rb.velocity = new Vector2(input*speed,rb.velocity.y);
    }
}
