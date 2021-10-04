using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
     public Transform firepoint;
    public GameObject enemyBullet;
    private float timeBetween;
    public float  startTimeBetween;

    private void Start()
    {
        timeBetween = startTimeBetween;
    }

    private void Update()
    {
        if(timeBetween <= 0)
        {
            Instantiate(enemyBullet, firepoint.position, firepoint.rotation);
            timeBetween = startTimeBetween;
        }
        else 
        {
            timeBetween -= Time.deltaTime;
        }
    }
}
