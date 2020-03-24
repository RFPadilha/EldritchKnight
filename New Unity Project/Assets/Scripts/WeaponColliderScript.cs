using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WeaponColliderScript : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(gameObject.name + "was triggered by" + other.gameObject.name);
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log(other.gameObject.name + "takes damage");
        }
    }
}
