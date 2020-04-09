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
        finalDamage = weaponDamage;



    }//if player's attacking, deals damage
}