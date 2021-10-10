using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

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

    //This method handles player death, which occurs upon collision with enemies or missiles
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Missile")
        {    
            if (!(playerController.raceFinished)) //Move player back to the passed check point
                playerController.PV.transform.position = checkPointRespawn;
            else //Move player back to last passed check point while spectating. Done to prevent disruption of the race by spectators
                playerController.PV.transform.position = new Vector3(checkPointRespawn.x, checkPointRespawn.y, -100);

            //If the collision was with a missile, verify the collision was with the missile's target and end the missile early
            if (collision.gameObject.tag == "Missile")
            {
                SabotageMissile missile = collision.gameObject.GetComponent<SabotageMissile>();
                PlayerController target = missile.target;
                if (target.PV == playerController.PV)
                    missile.EndMissile();
            }
        }
    }
}
