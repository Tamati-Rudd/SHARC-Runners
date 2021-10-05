using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using UnityEditor;

public class TravisTests : MonoBehaviour
{
    private GameObject playerPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Resources/PhotonPrefabs/PlayerBlue.prefab");

    //This test checks if the initial respawn point is set to first picked up checkpoint
    [Test]
    public void testInitialRespawnPoint()
    {

    }

    //Verify that the player will respawn to checkpoint location upon death
    [Test]
    public void testPlayerRespawn()
    {

    }

    //Verify that the respawn location is update after passing each checkpoint
    [Test]
    public void testCheckpointsRespawn()
    {
    }
}
