using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;

//NOTE: For this scene to function correctly, the next built scene after the game scene MUST be the game ended scene

public class FinishPoint : MonoBehaviour 
{
    PhotonView winnerPV;
    //Canvas canvas;
    //TextMeshPro winnerText;

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") { //If the colliding object has the Player tag
            winnerPV = collision.GetComponent<PhotonView>(); //Get the winner

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1); //Load the next built scene (will be the post game scene)
            //Instansiate()
            //GameObject canvas = GameObject.FindGameObjectWithTag("CanvasUI");
            //canvas = tempObject.GetComponent<Canvas>(); //Get the canvas so the text objects can be accessed
            //winnerText = GameObject.Find("WinnerText").GetComponent<TextMeshPro>();
            //if (winnerText == null)
                //Debug.Log("Problem: Winner Text Reference is Null");
            //winnerText.text = winnerPV.Owner.NickName + "Wins!";
        }
    }
}
