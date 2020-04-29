using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaControlsScript : MonoBehaviour
{
    //Variables to control player health and healthbar
    public SimpleHealthBar healthBar;
    public float health, maxHealth;
    private bool isDead;
    //public float damage;
    //Variables that control general Y movement
    public float jumpStrength;
    private bool onGround;
    public bool isBlocking;
    private bool isFalling;
    //player variables
    bool attacking;
    public float nextAttackTime;
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
        isDead = false;
        isBlocking = false;
        dashTime = startDashTime;
        
        if (maxHealth < health)
        {
            maxHealth = health;
        }//adjusts health to max at start of game

        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponentInChildren<Rigidbody>();
        weaponCollider = GetComponentInChildren<WeaponCollider>();

    }//initializes values and components
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

        bool jumpButtonDown = Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Joystick1Button0);
        bool attackButtonDown = Input.GetMouseButton(0) || !Mathf.Approximately(Input.GetAxis("RightTrigger"), 0f);
        bool blockButtonDown = Input.GetMouseButton(1) || !Mathf.Approximately(Input.GetAxis("LeftTrigger"), 0f);


        Vector3 newPosition = new Vector3(hor, 0.0f, ver);//regulates look direction, not really sure why tho


        
        if(Time.time > nextAttackTime)
        {
            attacking = false;
            isBlocking = false;
            weaponCollider.attack = false;
            if (attackButtonDown && Time.time > nextAttackTime + 0.4f && !blockButtonDown)//tests for attack while not blocking
            {
                nextAttackTime = Time.time + 0.4f;
                attacking = true;
                weaponCollider.attack = true;
            }
            else if(!attackButtonDown && Time.time > nextAttackTime + 0.4f && blockButtonDown)//tests for block while not attacking
            {
                isBlocking = true;
                newPosition = new Vector3(0f, 0f, 0f);
            }
        }//regulates attack and block



        if (jumpButtonDown && onGround == true)
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
            
            if (Input.GetKey(KeyCode.LeftShift) && isWalking == true && isDashing==false && (onGround == true || isFalling == true))
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
        }//dash cooldown

        if (Time.time > (hitTime + 1))
        {
            wasHit = false;
        }//resets being hit animation

        if (health <= 0)
        {
            isDead = true;
            m_Rigidbody.velocity = Vector3.zero;
        }
        else isDead = false;


        //Animator communications
        m_Animator.SetBool("IsAttacking", attacking);
        m_Animator.SetBool("OnGround", onGround);
        m_Animator.SetBool("IsWalking", isWalking);
        m_Animator.SetBool("IsFalling", isFalling);
        m_Animator.SetBool("IsDashing", isDashing);
        m_Animator.SetBool("WasHit", wasHit);
        m_Animator.SetBool("IsDead", isDead);
        m_Animator.SetBool("IsEnduring", isBlocking);


        //movement updater, slows down movement when attacking, or getting hit
        transform.LookAt(newPosition + transform.position);
        if(wasHit == true || attacking == true)
        {
            transform.Translate(newPosition * moveSpeed * Time.deltaTime/3, Space.World);
        }
        else {
            transform.Translate(newPosition * moveSpeed * Time.deltaTime, Space.World);
        }



    }//regulates all player input/output

    
    private void OnTriggerEnter(Collider other)//weapon hit detection
    {
        if (other.gameObject.GetComponent<WeaponCollider>().attack && wasHit == false)
        {
            hitTime = Time.time;
            wasHit = true;
            health -= other.gameObject.GetComponent<WeaponCollider>().finalDamage;
            healthBar.UpdateBar(health, maxHealth);
        }//if player's attacking, deals damage
        else {
            Debug.Log("Hits the air. Ouch");
        }//do nothing
    }
    
    void OnCollisionEnter(Collision other)
    { 
        if (other.gameObject.CompareTag("ground"))
        {
            onGround = true;
            isFalling = false;
        }//tests for collision with ground
    }//only used for "onGround" detection so far
    
    
    
}
