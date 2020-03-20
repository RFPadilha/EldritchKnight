using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaCameraScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform Obstruction;
    public Transform Target;
    float zoomSpeed = 2f;
    void Start()
    {
        Obstruction = Target;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        ViewObstructed();
    }
    void ViewObstructed()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Target.position - transform.position, out hit, 4.5f))
        {
            if (hit.collider.gameObject.tag != "Player")
            {
                Obstruction = hit.transform;
                if (Obstruction.gameObject.GetComponent<MeshRenderer>())
                {
                    Obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
                    if (Vector3.Distance(Obstruction.position, transform.position) >= 3f && Vector3.Distance(transform.position, Target.position) >= 1.5f)
                    {
                        transform.Translate(Vector3.forward * zoomSpeed * Time.deltaTime);
                    }
                }
            }
        }
        else
        {
            Obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
            if (Vector3.Distance(transform.position, Target.position) < 4.5f)
            {
                transform.Translate(Vector3.back * zoomSpeed * Time.deltaTime);
            }
        }
    }
}
