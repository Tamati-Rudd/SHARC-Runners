using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    private float rotateZ;
    public float rotationSpeed;
    public bool rotateClockwise;

    // Update is called once per frame
    void Update()
    {
        if (rotateClockwise == false)
        {
            rotateZ += Time.deltaTime * rotationSpeed;
        }
        else
        {
            rotateZ += -Time.deltaTime * rotationSpeed;
        }

        transform.rotation = Quaternion.Euler(0, 0, rotateZ);
    }
}
