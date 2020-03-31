using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollider : MonoBehaviour
{
    public bool attack;
    void Start()
    {
        gameObject.SetActive(true);
        attack = false;
    }
}