using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemy;
    public GameObject gem;
    public Vector2 SpawnPoint;
    public PhotonView PV;


    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    // Start is called before the first frame update
    void Start()
    {
        SpawnPoint.x = (float)21.8;
        SpawnPoint.y = (float)-1.9;


        if (PV.Owner.IsMasterClient)
        {            
            CreateEnemy(SpawnPoint);
        }

        

        


    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void CreateEnemy(Vector2 SpawnPoint)
    {              

        GameObject enemyclone = PhotonNetwork.InstantiateRoomObject(Path.Combine("PhotonPrefabs", "Enemy"), SpawnPoint, Quaternion.identity);
        enemyclone.GetComponent<EnemyLife>().gemPrefab = gem;
        //enemyclone.GetComponent<EnemyAI>().player = prefab.GetComponent<Transform>();
    }
}
