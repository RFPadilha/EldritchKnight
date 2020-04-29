using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ArenaCameraScript : MonoBehaviour
{
    public Transform Obstruction;
    //Transform Target;//used for removing view obstructions

    public List<Transform> targets;
    public Vector3 offset;//used to adjust camera distance and center

    public float minZoom = 40f;
    public float maxZoom = 10f;
    public float zoomLimiter = 50f;

    private Camera cam;

    float zoomSpeed = 2f;
    void Start()
    {
        //Obstruction = Target;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        //ViewObstructed();
    }
    void LateUpdate()
    {
        cameraMove();
        cameraZoom();
    }
    void cameraMove()//keeps the camera focused on all players
    {
        if (targets.Count == 0)
        {
            return;
        }
        Vector3 centerPoint = GetCenterPoint();
        Vector3 newPosition = centerPoint + offset;
        transform.position = newPosition;
    }
    Vector3 GetCenterPoint()//returns the center point between players
    {
        if(targets.Count == 1)
        {
            return targets[0].position;
        }
        Bounds cameraBounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i= 0; i<targets.Count; i++){
            cameraBounds.Encapsulate(targets[i].position);
        }
        return cameraBounds.center;
    }

    void cameraZoom()
    {
        float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance()/zoomLimiter);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, Time.deltaTime);
    }

    float GetGreatestDistance()
    {
        Bounds playerBounds = new Bounds(targets[0].position, Vector3.zero);
        for(int i = 0; i< targets.Count; i++)
        {
            playerBounds.Encapsulate(targets[i].position);
        }
        return playerBounds.size.x;
    }













    /*
    void ViewObstructed()//detects view obstructions
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
    */
}
