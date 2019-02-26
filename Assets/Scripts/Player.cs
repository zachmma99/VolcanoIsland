using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    public float speed;   //available in the inspector
    private float input;
    public int health;

    private float eps = 0.0001f;

    Rigidbody2D rb; //needs a rigidbody
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update() {
        if (input > 0+eps || input<0-eps) {
            anim.SetBool("isRunning", true);
        } else {
            anim.SetBool("isRunning", false);
        }

        //FIXME: Need to change sprite so that the highlighting on the sprite
        //is correct. For now, this is ok.
        if (input > 0) {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        else if(input < 0){
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
    }

    void FixedUpdate() {
        //geting player input
        input = Input.GetAxis("Horizontal"); //Has smoothing
        //float input = Input.GetAxisRaw("Horizontal"); //Has no smoothing

        //move player
        rb.velocity = new Vector2(input*speed,rb.velocity.y);
    }

    public void takeDamage(int value) {
        health -= value;
        if (health <= 0) {
            //player dies
            GameObject.Destroy(gameObject);
        }
    }
}
