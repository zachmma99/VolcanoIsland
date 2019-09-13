using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    public float speed;   //available in the inspector
    private float input;
    public int health;

    private float eps = 0.0001f;

    //FIXME: I would much rather this be in a gamemanager class, not in the player class
    public GameObject deathPanel;

    //death fx
    public GameObject deathFX;

    Rigidbody2D rb; //needs a rigidbody
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        GameManager.instance().updateHealthText(health);
        GameManager.instance().deathPanelSwich(false);

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
        //input = Input.GetAxis("Horizontal"); //Has smoothing
        input = Input.GetAxisRaw("Horizontal"); //Has no smoothing

        //move player
        rb.velocity = new Vector2(input*speed,rb.velocity.y);
    }

    public void takeDamage(int value) {
        health -= value;
        GameManager.instance().updateHealthText(health);

        if (health <= 0) {
            //player dies
            Instantiate(deathFX, new Vector3(transform.position.x, transform.position.y, 0f), Quaternion.identity);
            this.gameObject.SetActive(false);
            //GameObject.Destroy(gameObject); //can't have this for an easy reset
            GameManager.instance().deathPanelSwich(true);
            
        }
    }

    public void reset(){
        health=3;
        Vector3 pos =new Vector3(0.14f,-1.41f,0.0781f);
        this.transform.position=pos;
        GameManager.instance().updateHealthText(health);
        this.gameObject.SetActive(true);
    }
}
