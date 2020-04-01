using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollider : MonoBehaviour
{
    public bool attack;
    public float weaponDamage;
    public float finalDamage;
    private void OnTriggerEnter(Collider other)//weapon hit detection
    {
        if (other.gameObject.GetComponent<ShieldCollider>().block)
        {
            finalDamage = weaponDamage * other.gameObject.GetComponent<ShieldCollider>().damageReduction;
        }
        
    }//if player's attacking, deals damage
}