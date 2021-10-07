using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public Vector3 checkPointRespawn;
    public bool isCheckPoint = false;
    PlayerController playerController;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "CheckPoint")
        {
            checkPointRespawn = transform.position;
            isCheckPoint = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Check if the target object is an enemy
        if (collision.gameObject.tag == "Enemy")
        {
            if (!(playerController.raceFinished)) //Move player back to the passed check point
                playerController.PV.transform.position = checkPointRespawn;
            else //Move player back to last passed check point while spectating. Done to prevent disruption of the race by spectators
                playerController.PV.transform.position = new Vector3(checkPointRespawn.x, checkPointRespawn.y, -100);
        }
    }

}
