using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;


//This script spawns enemies on the scene
public class EnemyManager : MonoBehaviour
{
    public GameObject enemy;
    public GameObject gem;
    public Vector2 SpawnPoint, SpawnPoint1, SpawnPoint2, SpawnPoint3, SpawnPoint4, SpawnPoint5, 
    SpawnPoint6, SpawnPoint7, SpawnPoint8, SpawnPoint9, SpawnPoint10, SpawnPoint11, SpawnPoint12, SpawnPoint13,
    SpawnPoint14, SpawnPoint15, SpawnPoint16, SpawnPoint17, SpawnPoint18, SpawnPoint19, SpawnPoint20, SpawnPoint21;
    public PhotonView PV;


    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    // Start is called before the first frame update
    void Start()
    {
        int sceneNo = SceneManager.GetActiveScene().buildIndex;
        if(sceneNo == 1)
        {
            //spawnpoints for the enemy
            SpawnPoint.x = (float)56.822;
            SpawnPoint.y = (float)-9.087;

            SpawnPoint1.x = (float)55.155;
            SpawnPoint1.y = (float)11.623;

            SpawnPoint2.x = (float)78.119;
            SpawnPoint2.y = (float)35.352;

            SpawnPoint3.x = (float)60.512;
            SpawnPoint3.y = (float)50.6927;

            SpawnPoint4.x = (float)49.76936;
            SpawnPoint4.y = (float)0.8636761;

            SpawnPoint5.x = (float)83.96582;
            SpawnPoint5.y = (float)1.003826;

            SpawnPoint6.x = (float)65.39159;
            SpawnPoint6.y = (float)26.2;

            SpawnPoint7.x = (float)83.15574;
            SpawnPoint7.y = (float)25.97931;

            SpawnPoint8.x = (float)107.007;
            SpawnPoint8.y = (float)16.681;

            SpawnPoint9.x = (float)55.374;
            SpawnPoint9.y = (float)17.778;

            SpawnPoint10.x = (float)42.487;
            SpawnPoint10.y = (float)40.435;

            SpawnPoint11.x = (float)120.402;
            SpawnPoint11.y = (float)40.47;

            SpawnPoint12.x = (float)47.76164;//changed
            SpawnPoint12.y = (float)68.12277;

            SpawnPoint13.x = (float)74.27283;//changed
            SpawnPoint13.y = (float)68.38171;

            SpawnPoint14.x = (float)93.2;
            SpawnPoint14.y = (float)65.4;

            SpawnPoint15.x = (float)104.7;
            SpawnPoint15.y = (float)65.4;

            SpawnPoint16.x = (float)115.8;
            SpawnPoint16.y = (float)65.4;

            SpawnPoint17.x = (float)124.2;
            SpawnPoint17.y = (float)65.4;

            SpawnPoint18.x = (float)149.9819;
            SpawnPoint18.y = (float)64.59638;

            SpawnPoint19.x = (float)161.2674;
            SpawnPoint19.y = (float)-15.85811;

            SpawnPoint20.x = (float)155.0042;
            SpawnPoint20.y = (float)31.62046;      
        } else if(sceneNo == 4)
        {
            //spawnpoints for the enemy
            SpawnPoint.x = (float)94.3; //Dne
            SpawnPoint.y = (float)-50.84;

            SpawnPoint1.x = (float)76.8; //Done
            SpawnPoint1.y = (float)-18.6;

            SpawnPoint2.x = (float)32.1;//changed
            SpawnPoint2.y = (float)-64.4;

            SpawnPoint3.x = (float)22;//Changed
            SpawnPoint3.y = (float)-61.5;

            SpawnPoint4.x = (float)54.98; //Done
            SpawnPoint4.y = (float)11.91;

            SpawnPoint5.x = (float)-2.143608;//changed //patrolling enemy
            SpawnPoint5.y = (float)-85.99;

            SpawnPoint6.x = (float)65.39418;//Changed
            SpawnPoint6.y = (float)-26.7;

            SpawnPoint7.x = (float)46.39194;//Changed
            SpawnPoint7.y = (float)-17.72367;

            SpawnPoint8.x = (float)99.96; //Done
            SpawnPoint8.y = (float)3.65;

            SpawnPoint9.x = (float)27.89611;//Changed
            SpawnPoint9.y = (float)-7.251762;

            SpawnPoint10.x = (float)7.632291;//Changed
            SpawnPoint10.y = (float)-6.979765;

            SpawnPoint11.x = (float)14.57497;//changed
            SpawnPoint11.y = (float)-60.97709;

            SpawnPoint12.x = (float)-54.3;//changed
            SpawnPoint12.y = (float)-9.7;

            SpawnPoint13.x = (float)-55.02;//changed
            SpawnPoint13.y = (float)-26.69;

            SpawnPoint14.x = (float)-67.6;
            SpawnPoint14.y = (float)-7.1;

            SpawnPoint15.x = (float)-70.20993;
            SpawnPoint15.y = (float)-7.15;

            SpawnPoint16.x = (float)-64.6311;
            SpawnPoint16.y = (float)-55.96423;

            SpawnPoint17.x = (float)-41.56703;
            SpawnPoint17.y = (float)-55.96423;

            SpawnPoint18.x = (float)-19.79326;
            SpawnPoint18.y = (float)-58.06096;

            SpawnPoint19.x = (float)1.94;
            SpawnPoint19.y = (float)-63.49;

            SpawnPoint20.x = (float)31.43863;
            SpawnPoint20.y = (float)-76.12081;      
        }

        //only the masterclient can spawn enemies for everyone
        if (PV.Owner.IsMasterClient) 
        {
            if (sceneNo == 1)
            {
                CreateEnemy(SpawnPoint);
                CreateEnemy(SpawnPoint1);
                CreateEnemy(SpawnPoint2);
                CreateEnemy(SpawnPoint3);
                CreateEnemy(SpawnPoint20);
                CreateEnemyTurret(SpawnPoint4);
                CreateEnemyTurret(SpawnPoint5);
                CreateEnemyTurret(SpawnPoint6);
                CreateEnemyTurret(SpawnPoint7);
                CreateJumpingEnemy(SpawnPoint8);
                CreateJumpingEnemy(SpawnPoint9);
                CreateJumpingEnemy(SpawnPoint10);
                CreateJumpingEnemy(SpawnPoint11);
                CreateJumpingEnemy(SpawnPoint20);
                CreateEnemyTurret(SpawnPoint12);
                CreateEnemyTurret(SpawnPoint13);
                CreateEnemyTurret(SpawnPoint14);
                CreateEnemyTurret(SpawnPoint15);
                CreateEnemyTurret(SpawnPoint16);
                CreateEnemyTurret(SpawnPoint17);
                CreateEnemyTurret(SpawnPoint18);
            } else if(sceneNo == 4)
            {
                CreateEnemy(SpawnPoint);
                CreateEnemy(SpawnPoint1);
                CreateEnemyTurret(SpawnPoint2);
                CreateEnemyTurret(SpawnPoint3);
                CreateEnemyTurret(SpawnPoint4);
                CreateEnemy(SpawnPoint5);
                CreateEnemyTurret(SpawnPoint6);
                CreateEnemyTurret(SpawnPoint7);
                CreateJumpingEnemy(SpawnPoint8);
                CreateJumpingEnemy(SpawnPoint12);
                CreateJumpingEnemy(SpawnPoint13);
                CreateJumpingEnemy(SpawnPoint20);
                CreateEnemyTurret(SpawnPoint9);
                CreateEnemyTurret(SpawnPoint10);
                CreateEnemyTurret(SpawnPoint11);
                CreateEnemyTurret(SpawnPoint14);
                CreateEnemyTurret(SpawnPoint15);
                CreateEnemyTurret(SpawnPoint16);
                CreateEnemyTurret(SpawnPoint17);
                CreateEnemyTurret(SpawnPoint18);
                CreateEnemyTurret(SpawnPoint19);
                
            }
        }

    }

    //This function creates the patrolling enemy in the scene
    void CreateEnemy(Vector2 SpawnPoint)
    {              
        //instantiating the enemy
        GameObject enemyclone = PhotonNetwork.InstantiateRoomObject(Path.Combine("PhotonPrefabs", "Enemy"), SpawnPoint, Quaternion.identity);

        if (enemyclone != null)
        {
            //Assiging the crystal so it can be dropped
            enemyclone.GetComponent<EnemyLife>().gemPrefab = gem;
        }
        
    }

    //This function creates the jumping enemy in the scene
    void CreateJumpingEnemy(Vector2 SpawnPoint)
    {
         //instantiating the enemy
        GameObject enemyclone = PhotonNetwork.InstantiateRoomObject(Path.Combine("PhotonPrefabs", "JumpingEnemy"), SpawnPoint, Quaternion.identity);

        if (enemyclone != null)
        {
            //Assiging the crystal so it can be dropped
            enemyclone.GetComponent<EnemyLife>().gemPrefab = gem;
        }
    }

    //This function creates the turret enemy in the scene
    void CreateEnemyTurret(Vector2 SpawnPoint)
    {
         //instantiating the enemy
        GameObject enemyclone = PhotonNetwork.InstantiateRoomObject(Path.Combine("PhotonPrefabs", "EnemyTurret"), SpawnPoint, Quaternion.identity);

        if (enemyclone != null)
        {
            //Assiging the crystal so it can be dropped
            enemyclone.GetComponent<EnemyLife>().gemPrefab = gem;
        }
    }
}
