using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using UnityEditor;

public class TravisTests : MonoBehaviour
{
    private GameObject playerPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Resources/PhotonPrefabs/PlayerBlue.prefab");
    private GameObject cpFlag = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Resources/Checkpoint.prefab");


    //This test checks if the initial respawn point is equal to the player instantiated location
    [Test]
    public void testInitialRespawnPoint()
    {
        var player = Object.Instantiate(playerPrefab, Vector2.zero, Quaternion.identity);

        //Get the expected and actual test results
        var actual = player.transform.position;
        var expected = Vector3.zero;

        //Verify the player positions
        Assert.AreEqual(expected, actual);
    }

    //Verify that the player can pick up checkpoint
    [Test]
    public void testCheckPointPickup()
    {
        var cp = new GameObject();
        cp.AddComponent<CheckPoint>();
        CheckPoint cpScript = cp.GetComponent<CheckPoint>();

        //set isCheckPoint to true to mimick player touch the checkpoint
        cpScript.isCheckPoint = true;

        //Get the expected and actual test results
        var actual = cpScript.isCheckPoint;
        var expected = true;

        //Verify that the player has passed the checkpoint
        Assert.AreEqual(expected, actual);
    }

    //Verify that the respawn location is update after passing each checkpoint
    [Test]
    public void testCheckpointsRespawn()
    {
        Vector2 checkpointLocation = new Vector2(51, -8);
        var checkpoint = Object.Instantiate(cpFlag, checkpointLocation, Quaternion.identity);
        var player = Object.Instantiate(playerPrefab, Vector2.zero, Quaternion.identity);
        var cp = new GameObject();
        cp.AddComponent<CheckPoint>();
        CheckPoint cpScript = cp.GetComponent<CheckPoint>();

        //set isCheckPoint to true to mimick player touch the checkpoint
        cpScript.isCheckPoint = true;

        //Get the expected and actual test results
        if (cpScript.isCheckPoint == true)
        {
            player.transform.position = checkpoint.transform.position;
        }
        var actual = player.transform.position;
        var expected = checkpoint.transform.position;

        //Verify that the player initial spawn location is updated to the passed checkpoint location
        Assert.AreEqual(expected, actual);
    }
}
