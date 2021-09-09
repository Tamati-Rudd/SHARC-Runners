using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemy;
    public GameObject gem;
    public Vector2 SpawnPoint,SpawnPoint1,SpawnPoint2,SpawnPoint3;
    public PhotonView PV;


    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    // Start is called before the first frame update
    void Start()
    {
        SpawnPoint.x = (float)56.822;
        SpawnPoint.y = (float)-9.087;

        SpawnPoint1.x = (float)55.155;
        SpawnPoint1.y = (float)11.623;

        SpawnPoint2.x = (float)78.119;
        SpawnPoint2.y = (float)35.352;

        SpawnPoint3.x = (float)60.512;
        SpawnPoint3.y = (float)50.6927;

        if (PV.Owner.IsMasterClient)
        {            
            CreateEnemy(SpawnPoint);
            CreateEnemy(SpawnPoint1);
            CreateEnemy(SpawnPoint2);
            CreateEnemy(SpawnPoint3);
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
