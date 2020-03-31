using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaControlsScript : MonoBehaviour
{
    //Variables to control player health and healthbar
    public SimpleHealthBar healthBar;
    public float health, maxHealth;
    public float damage;
    //Variables that control general Y movement
    public float jumpStrength;
    private bool onGround;
    private bool isFalling;
    //player variables
    bool attacking;
    public float attackDuration;
    public float moveSpeed;
    //Timed events variables
    public float cooldownTime;
    private float dashRefreshTime;
    private float dashTime;
    public float startDashTime;
    public float dashSpeed;
    private float dashDuration;
    private bool isDashing;
    private bool wasHit;
    private float hitTime;

    //Unity components for quick refs
    Animator m_Animator;
    Rigidbody m_Rigidbody;
    private WeaponCollider weaponCollider;

    void Start()
    {
        wasHit = false;
        isDashing = false;
        onGround = true;
        isFalling = false;
        dashTime = startDashTime;
        attackDuration = 0f;

        weaponCollider = GetComponentInChildren<WeaponCollider>();
        
        if (maxHealth < health)
        {
            maxHealth = health;
        }//adjusts health to max at start of game
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        
    }
    void FixedUpdate()
    {
        PlayerCommands();
    }

    void PlayerCommands()
    {
        //regulates movement
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        bool hasHorMove = !Mathf.Approximately(hor, 0f);
        bool hasVerMove = !Mathf.Approximately(ver, 0f);
        bool isWalking = hasHorMove || hasVerMove;

        Vector3 newPosition = new Vector3(hor, 0.0f, ver);

        



        if (Input.GetMouseButton(0) && Time.time > attackDuration)
        {
            attackDuration = Time.time + 1f;
            attacking = true;
            weaponCollider.attack = true;
        }//regulates attack, currently on left MB
        if(Time.time > attackDuration)
        {
            attacking = false;
            weaponCollider.attack = false;
        }


        if (Input.GetKey(KeyCode.Space) && onGround == true)
        {
            m_Rigidbody.velocity = new Vector3(0f, jumpStrength, 0f);
            onGround = false;
            
            
            
        }//Regulates jump
        if (m_Rigidbody.velocity.y <= 0 && onGround==false)
        {
            isFalling = true;
        }//Regulates fall

        
        if (Time.time > dashRefreshTime)
        {

            if (dashTime <= 0)
            {
                dashTime = startDashTime;
                m_Rigidbody.velocity = newPosition;
                
            }
            
            if (Input.GetKey(KeyCode.LeftShift) && isWalking == true && isDashing==false)
            {
                
                dashTime -= Time.deltaTime;
                isDashing = true;
                m_Rigidbody.velocity = newPosition * dashSpeed;
                dashRefreshTime = Time.time + cooldownTime;
                dashDuration = Time.time + startDashTime;
               
            }
        }//regulating dash
        if (Time.time > dashDuration)
        {
            isDashing = false;
        }

        if (Time.time > (hitTime + 1))
        {
            wasHit = false;
        }//resets being hit animation


        //Animator communications
        m_Animator.SetBool("IsAttacking", attacking);
        m_Animator.SetBool("OnGround", onGround);
        m_Animator.SetBool("IsWalking", isWalking);
        m_Animator.SetBool("IsFalling", isFalling);
        m_Animator.SetBool("IsDashing", isDashing);
        m_Animator.SetBool("WasHit", wasHit);


        //movement updater
        transform.LookAt(newPosition + transform.position);
        if(wasHit == true || attacking == true)
        {
            transform.Translate(newPosition * moveSpeed * Time.deltaTime/2, Space.World);
        }
        else {
            transform.Translate(newPosition * moveSpeed * Time.deltaTime, Space.World);
        }



    }//regulates all player input

    private void OnTriggerEnter(Collider other)//weapon hit detection
    {
        if (other.gameObject.GetComponent<WeaponCollider>().attack)
        {
            TakeDamage();
        }
    }
    void OnCollisionEnter(Collision other)
    { 
        if (other.gameObject.CompareTag("ground"))//if touching the ground
        {
            onGround = true;
            isFalling = false;
        }
    }//collision detection, used for jumps so far
    
    void TakeDamage()
    {
        hitTime = Time.time;
        wasHit = true;
        health -= damage;
        healthBar.UpdateBar(health, maxHealth);


    }
}
