using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    PhotonView PV;
    

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        if (PV.IsMine)
        {
            CreateController();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CreateController()
    {
        //Spawn the Player
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerRed"), Vector2.zero, Quaternion.identity);
    }
}
