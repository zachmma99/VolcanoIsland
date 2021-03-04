/*
Copyright 2020 Micah Schuster

Redistribution and use in source and binary forms, with or without modification,
are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this
list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice,
this list of conditions and the following disclaimer in the documentation and/or
other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED.
IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT,
INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING,
BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF
LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE
OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF
ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]

public class Player : MonoBehaviour
{
    public float speed;
    public int health;
    public int ammo;
    public float timeBetweenDashes;
    private float dashTimer;
    private float dashCooldown;
    private float input;

    private Color baseColor;
    private Color hitColor;

    private Rigidbody2D rb;
    private SpriteRenderer[] sr;
    public Transform arm;

    public GameObject attack;
    public GameObject healthUp;
    public GameObject dashEffect;
    public GameObject deathEffect;

    Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentsInChildren<SpriteRenderer>();
        arm = GetComponent<Transform>();
        anim = GetComponent<Animator>();
        GameManager.instance().updateHealthText(health);
        GameManager.instance().deathPanelSwitch(false);
        baseColor = new Color(255, 255, 255);
        hitColor = new Color(255, 0, 0);
        dashTimer = 0f;
        dashCooldown = timeBetweenDashes;
    }

    private void Update()
    {
        if (input > 0 || input < 0)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }

        if (input > 0)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        else if (input < 0)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
        
        handleInput();
    }

    void FixedUpdate()
    {
        input = Input.GetAxisRaw("Horizontal"); //Has no smoothing
        rb.velocity = new Vector2(input * speed, rb.velocity.y);
    }

    private void handleInput()
    {
        if ((Input.GetKeyDown(KeyCode.LeftShift)) && (dashTimer <= 0f))
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
            shoot();

            anim.ResetTrigger("notShooting");

            anim.SetTrigger("isShooting");
        }
        {
            anim.SetTrigger("notShooting");
            
            anim.ResetTrigger("isShooting");
        }
    }

    public void gainHealth(int value)
    {
        health += value;
        
        sr[0].color = baseColor;
        sr[1].color = baseColor;
        
        GameManager.instance().updateHealthText(health);
        
        Instantiate(healthUp, new Vector3(transform.position.x, transform.position.y + 2f, 0f), Quaternion.identity);
    }

    public void takeDamage(int value)
    {
        health -= value;

        sr[0].color = hitColor;
        sr[1].color = hitColor;

        GameManager.instance().updateHealthText(health);

        if (health <= 0)
        {
            Instantiate(deathEffect, new Vector3(transform.position.x, transform.position.y, 0f), Quaternion.identity);
            this.gameObject.SetActive(false);
            GameManager.instance().deathPanelSwitch(true);
        }
    }

    private void dash()
    {
        float currentPosX = this.transform.position.x;
        float currentPosY = this.transform.position.y;
        float currentPosZ = this.transform.position.z;
        float newForwardPosX = currentPosX + 3;
        float newBackwardPosX = currentPosX - 3;

        if (this.transform.rotation.y == 0)
        {
            this.transform.position = new Vector3(newForwardPosX, currentPosY, currentPosZ);
            Instantiate(dashEffect, new Vector3(newForwardPosX, this.transform.position.y + 0.5f, 0f), Quaternion.Euler(0f, -90f, 90f));
        }
        else
        {
            this.transform.position = new Vector3(newBackwardPosX, currentPosY, currentPosZ);
            Instantiate(dashEffect, new Vector3(newBackwardPosX, this.transform.position.y + 0.5f, 0f), Quaternion.Euler(0f, -90f, 90f));
        }
    }

    private void shoot()
    {   
        if (ammo > 0)
        {
            ammo -= 1;

            GameManager.instance().updateAmmoText(ammo);

            Instantiate(attack, new Vector3(arm.transform.position.x, arm.transform.position.y + 0.5f, 0f), Quaternion.identity);
        }
        else
        {
            return;
        }
    }

    public void reset()
    {
        health = 3;
        ammo = 10;
        Vector3 pos = new Vector3(0f, -3.75f, 0f);
        this.transform.position = pos;
        GameManager.instance().updateHealthText(health);
        GameManager.instance().updateAmmoText(ammo);
        this.gameObject.SetActive(true);
        sr[0].color = baseColor;
        sr[1].color = baseColor;
    }
}
