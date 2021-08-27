using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//NOTE: For this scene to function correctly, the next built scene after the game scene MUST be the game ended scene

public class FinishPoint : MonoBehaviour //Written by: Tamati
{
    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") { //If the colliding object has the Player tag
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1); //Load the next built scene (will be the finish game scene)
        }
    }
}
