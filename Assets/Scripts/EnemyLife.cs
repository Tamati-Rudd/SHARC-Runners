using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
/** 
** EnemyLife Script
** This script contains the life behaviour of the enemy:
** Receiving Damage 
** Death of enemy
** Generating gems
**/
public class EnemyLife : MonoBehaviourPunCallbacks
{
    private int HP = 10; //Health Point of Enemy

    //Materials used for the animation
    private Material matWhite;
    private Material matDefault;
    
    SpriteRenderer sr;
    public GameObject gemPrefab;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        matWhite = Resources.Load("WhiteFlash", typeof(Material)) as Material;
        matDefault = sr.material;
    }

    //Method for enemy detecting collision with player bullet
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //When enemy is struck by bullet
        if(collision.CompareTag("PlayerProjectile"))
        {
            HP--; //Health goes down
            //sr.material = matWhite; //Enemy flashes white to indicate hit 
            GenerateGem();

            if(HP <= 0)
            {
                KillSelf();
            }
            /*else
            {
                //Invoke("ResetMaterial", 1.5f);
            }*/
        }
    }

    /*private void ResetMaterial()
    {
        sr.material = matDefault;
    }*/

    //Method for enemy dying
    private void KillSelf()
    {
        Destroy(gameObject);
    }


    //Method for generating gems
    private void GenerateGem()
    {
        Instantiate(gemPrefab, transform.position, gemPrefab.transform.rotation);
    }
}
