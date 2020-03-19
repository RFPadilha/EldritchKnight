using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControls : MonoBehaviour
{
    public float jumpStrength = 2f;
    public float MoveSpeed;
    Animator m_Animator;
    Rigidbody m_Rigidbody;

    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
    }
    void Update()
    {
        PlayerCommands();
    }

    void PlayerCommands()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        Vector3 playerMovement = new Vector3(hor, 0, ver) * MoveSpeed * Time.deltaTime;
       

        bool hasHorizontalInput = !Mathf.Approximately(hor, 0f);
        bool hasVerticalInput = !Mathf.Approximately(ver, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        bool attacking;

        if (Input.GetMouseButton(1))
        {
            attacking = true;
        }
        else attacking = false;


        if (Input.GetKey(KeyCode.Space))
        {
            Jump();
        }
       
        m_Animator.SetBool("IsWalking", isWalking);
        m_Animator.SetBool("IsAttacking", attacking);
        m_Animator.SetBool("OnGround", CanJump());

        transform.Translate(playerMovement, Space.Self);

        
    }
    void Jump()
    {
        if (CanJump())
        {
            m_Rigidbody.AddForce(jumpStrength * transform.up, ForceMode.Impulse);
        }
    }

    bool CanJump()
    {
        // Create Ray
        Ray ray = new Ray(transform.position, transform.up * -1);

        // Create Hit Info
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, transform.localScale.y + 0.2f))
        {
            return true;
        }

        // Nothing so return false
        return false;
    }
}
