using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    //variables to referencing gameobject to follow
    public Transform target;
    public Vector3 offset = new Vector3(0, 5, -10);
    //smooth following speed movement
    public float smoothSpeed = 0.125f;

    //late updating per frame for smooth camera transition
    void LateUpdate()
    {
        //checking if target isnt null
        if(target != null)
        {
            //calc position for camera
            Vector3 desiredPosition = target.position + offset;
            
            //interpolate to player position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            
            //updating camera position
            transform.position = smoothedPosition;

            //make the camera always look at the target
            transform.LookAt(target);
        }
    }
}
