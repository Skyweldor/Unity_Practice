using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    Camera cam;
    private Vector3 cameraPosition;
    private Vector3 cameraNewPosition = new Vector3(0.0f, 5.0f, 0.0f);
    private Vector3 moveCamera;
    public float camSmoothness = 0.5f;

    private void Start()
    {
        cam = GetComponent<Camera>();
        
    }

    private void LateUpdate()
    {    
    }

    public void toggleMoveUp()
    {
        cameraPosition = cam.transform.position;
        Vector3 newPosition = cameraPosition + cameraNewPosition;
        transform.position = Vector3.Slerp(cameraPosition, newPosition, camSmoothness);
    }
}
