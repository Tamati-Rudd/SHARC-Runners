using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialFinishLine : MonoBehaviour
{
   private void OnTriggerEnter2D(Collider2D collision)
    {
        //Check if target object is a collectable
        if (collision.CompareTag("Player"))
        {
            finish();
        }
    }
   
    public void finish()
    {
         Application.Quit();
    }
}
