using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaControlsScript : MonoBehaviour
{
    public SimpleHealthBar healthBar;
    public float health, maxHealth;
    public float damage;
    public float jumpStrength;
    private bool onGround;
    public float moveSpeed;
    Animator m_Animator;
    Rigidbody m_Rigidbody;

    void Start()
    {
        if (maxHealth < health)
        {
            maxHealth = health;
        }
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        onGround = true;
    }
    void FixedUpdate()
    {
        PlayerCommands();
    }

    void PlayerCommands()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        bool hasHorMove = !Mathf.Approximately(hor, 0f);
        bool hasVerMove = !Mathf.Approximately(ver, 0f);
        bool isWalking = hasHorMove || hasVerMove;

        Vector3 playerMovement = new Vector3(hor, 0, ver).normalized * moveSpeed * Time.deltaTime;

        Vector3 newPosition = new Vector3(hor, 0.0f, ver);


        bool attacking;

        if (Input.GetMouseButton(1))
        {
            playerMovement = new Vector3(0, 0, 0);
            attacking = true;
        }
        else attacking = false;


        if (Input.GetKey(KeyCode.Space) && onGround == true)
        {
            m_Rigidbody.velocity = new Vector3(0f, jumpStrength, 0f);
            onGround = false;
        }

        m_Animator.SetBool("IsAttacking", attacking);
        m_Animator.SetBool("OnGround", onGround);
        m_Animator.SetBool("IsWalking", isWalking);
        m_Animator.SetFloat("Horizontal_m", hor);
        m_Animator.SetFloat("Vertical_M", ver);

        transform.LookAt(newPosition + transform.position);
        transform.Translate(newPosition * moveSpeed * Time.deltaTime, Space.World);

        //transform.Translate(playerMovement, Space.Self);


    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("ground"))//if touching the ground
        {
            onGround = true;
        }
        if (other.gameObject.CompareTag("Weapon"))//if hit by a weapon
        {
            takeDamage();
        }
    }
    
    void takeDamage()
    {
        health -= damage;
        healthBar.UpdateBar(health, maxHealth);
    }
}
