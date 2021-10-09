using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
//This script contains how the tutorial texts are displayed on screen
public class TutorialManager : MonoBehaviour
{
    public void loadLevel()
    {
        SceneManager.LoadScene(3);
    }

    public void loadMenu()
    {
         SceneManager.LoadScene(0);
    }
    
}
