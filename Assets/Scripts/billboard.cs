using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is used to make sure the camera follows the username tag
public class billboard : MonoBehaviour
{
    Camera cam;

    private void Update()
    {
        if (cam == null)
        {
            //find camera
            cam = FindObjectOfType<Camera>();
        }

        if(cam == null)
        {
            //if camera doesn't exist
            return;
        }

        //Look at camera and rotate accordingly
        transform.LookAt(cam.transform);
        transform.Rotate(Vector3.up * 180);
    }
}
