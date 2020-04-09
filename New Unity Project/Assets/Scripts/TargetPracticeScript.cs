using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPracticeScript : MonoBehaviour
{
    //Variables to control player health and healthbar
    public SimpleHealthBar healthBar;
    public float health, maxHealth;
    public float damage;
    private bool wasHit;
    private float hitTime;
    private bool onGround;
    private bool isDead;
    public bool isBlocking;

    //Unity components for quick refs
    Animator m_Animator;
    Rigidbody m_Rigidbody;

    void Start()
    {
        onGround = true;
        wasHit = false;
        if (maxHealth < health)
        {
            maxHealth = health;
        }//adjusts health to max at start of game
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponentInChildren<Rigidbody>();

    }
    void FixedUpdate()
    {
        if (health <= 0)
        {
            isDead = true;
            m_Rigidbody.velocity = Vector3.zero;
        }
        else isDead = false;

        m_Animator.SetBool("WasHit", wasHit);
        if (Time.time > (hitTime + 1))
        {
            wasHit = false;
        }
        m_Animator.SetBool("OnGround", onGround);
        m_Animator.SetBool("IsDead", isDead);
    }

    private void OnTriggerEnter(Collider other)//weapon hit detection
    {
        if (other.gameObject.GetComponent<WeaponCollider>().attack && wasHit == false)
        {
            hitTime = Time.time;
            wasHit = true;
            health -= other.gameObject.GetComponent<WeaponCollider>().finalDamage;
            healthBar.UpdateBar(health, maxHealth);
        }//if player's attacking, deals damage
        else { }//do nothing
    }
}
