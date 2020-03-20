using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControls : MonoBehaviour
{
    public float jumpStrength;
    private bool onGround;
    public float moveSpeed;
    Animator m_Animator;
    Rigidbody m_Rigidbody;

    void Start()
    {
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
        Vector3 playerMovement = new Vector3(hor, 0, ver).normalized * moveSpeed * Time.deltaTime;

        Vector3 newPosition = new Vector3(hor, 0.0f, ver);
        

        bool attacking;

        if (Input.GetMouseButton(2))
        {
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
        m_Animator.SetFloat("Horizontal_m", hor);
        m_Animator.SetFloat("Vertical_M", ver);

        transform.LookAt(newPosition + transform.position);
        transform.Translate(newPosition * moveSpeed * Time.deltaTime, Space.World);

        //transform.Translate(playerMovement, Space.Self);

        
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("ground"))
        {
            onGround = true;
        }
    }
    
}
