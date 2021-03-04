using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Player : MonoBehaviour
{   
    public float speed;
    public int health;
    public int maxHealth;
    public int ammo;
    public float timeBetweenDashes;
    private float dashTimer;
    private float dashCoolDown;
    private float input;
    private Color baseColor;
    private Color hitColor;
    private new Rigidbody2D rigidbody;
    private SpriteRenderer[] sr;
    private SpriteRenderer head;
    private SpriteRenderer body;
    public Transform arm;

    private Animator anim;

    public GameObject attack;
    public GameObject healthUp;
    public GameObject dashEffect;
    public GameObject death;

    KeyCode dashKey = KeyCode.LeftShift;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        sr = GetComponentsInChildren<SpriteRenderer>();
        dashTimer = 0f;
        dashCoolDown = timeBetweenDashes;
        baseColor = new Color(255, 255, 255, 255);
        hitColor = new Color(255, 0, 0, 255);
        head = sr[0];
        body = sr[1];
    }

    void Update()
    {
        handleInput();

        if(input > 0 || input < 0)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }

        if(input > 0)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        else if (input < 0)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        input = Input.GetAxisRaw("Horizontal");
        rigidbody.velocity = new Vector2(input * speed, 0f);
    }

    void handleInput()
    {
        if ((Input.GetKeyDown(dashKey)) && (dashTimer <= 0f))
        {
            dash();
    
            dashTimer = timeBetweenDashes;
        }
        else 
        {
            dashTimer -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            sr[2].enabled = true;

            if (ammo > 0)
            {
                shoot();
            }
            else
            {
                return;
            }

            anim.ResetTrigger("notShooting");

            anim.SetTrigger("isShooting");
        }
        else
        {
            sr[2].enabled = false;

            anim.SetTrigger("notShooting");
            
            anim.ResetTrigger("isShooting");
        }

    }

    public void takeDamage(int value)
    {
        health -= value;

        sr[0].color = hitColor;
        sr[1].color = hitColor;

        GameManager.instance().updateHealthText(health);

        if (health <= 0)
        {
            Instantiate(death, this.transform.position, Quaternion.identity);
            this.gameObject.SetActive(false);
            GameManager.instance().deathPanelSwitch(true);
        }
    }

    public void gainHealth(int value)
    {   
        Instantiate(healthUp, 
                    new Vector3(this.transform.position.x, 
                                this.transform.position.y + 2,
                                this.transform.position.z),
                    Quaternion.identity);

        sr[0].color = baseColor;
        sr[1].color = baseColor;

        if (health < maxHealth)
        {
            health += value;
        }
        else
        {
            return;
        }

        GameManager.instance().updateHealthText(health);
    }

    void shoot()
    {
        ammo -= 1;

        GameManager.instance().updateAmmoText(ammo);

        Instantiate(attack, this.arm.position, Quaternion.identity);
    }

    void dash()
    {
        float currentPosX = this.transform.position.x;
        float currentPosY = this.transform.position.y;
        float newForwardPosX = currentPosX + 3;
        float newBackwardPosX = currentPosX - 3;

        if (this.transform.rotation.y == 0)
        {
            this.transform.position = new Vector3(newForwardPosX, currentPosY, 0f);
        }
        else
        {
            this.transform.position = new Vector3(newBackwardPosX, currentPosY, 0f);
        }

        Instantiate(dashEffect, new Vector3(this.transform.position.x, (this.transform.position.y + 0.5f), this.transform.position.z), Quaternion.Euler(0f, -90f, 90f));
    }

    public void reset()
    {
        health = 3;
        ammo = 10;
        head.color = baseColor;
        body.color = baseColor;
        GameManager.instance().updateHealthText(health);
        Vector3 pos = new Vector3(0f, -3.75f, 0f);
        this.transform.position = pos;
        Vector3 rot = new Vector3(0f, 0f, 0f);
        transform.eulerAngles = rot;
        this.gameObject.SetActive(true);
        timeBetweenDashes = dashCoolDown;
    }
}