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

    //Unity components for quick refs
    Animator m_Animator;
    Rigidbody m_Rigidbody;

    void Start()
    {
        wasHit = false;
        if (maxHealth < health)
        {
            maxHealth = health;
        }//adjusts health to max at start of game
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();

    }
    void FixedUpdate()
    {
        m_Animator.SetBool("WasHit", wasHit);
        if (Time.time > (hitTime + 1))
        {
            wasHit = false;
        }
    }

    private void OnTriggerEnter(Collider other)//weapon hit detection
    {
        if (other.gameObject.GetComponent<WeaponCollider>().attack)
        {
           TakeDamage();
        }

    }
    void TakeDamage()
    {
        hitTime = Time.time;
        wasHit = true;
        health -= damage;
        healthBar.UpdateBar(health, maxHealth);
        
    }
}
