using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedAbility : MonoBehaviour
{
    public float speedTimer;

    private void Start()
    {
        speedTimer = 0;
    }
    public void ActivateAbility(PlayerController c)
    {
        if (c)
        {
            speedTimer += Time.deltaTime;

            if (speedTimer >= 3)
            {
                movementSpeed = 13.5f;
                speedTimer = 0;
                activateSpeed = false;
            }
        }
    }
}
