using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class ReturnToLobby : MonoBehaviour
{
    public Button button;

   void Start()
    {
        button.onClick.AddListener(ReturnPlayerToLobby);
    }

    
    public void ReturnPlayerToLobby()
    {
        Destroy(Roommanager.Instance.gameObject);
        SceneManager.LoadScene(0);
    }
}
