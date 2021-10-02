using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using UnityEditor;

public class ReyhanTests : MonoBehaviour
{
    private GameObject playerPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Resources/PhotonPrefabs/PlayerBlue.prefab");
    private GameObject crystalPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Resources/PhotonPrefabs/Square.prefab");

    //This test checks if the player is able to collect a crystal 
    [Test]
    public void testCollectedCrystal()
    {
        var player = Object.Instantiate(playerPrefab, Vector2.zero, Quaternion.identity);
        var playerScript = player.GetComponent<PlayerController>();
        var crystal = Object.Instantiate(crystalPrefab, Vector2.zero, Quaternion.identity);
        var collectableScript = playerScript.GetComponent<Collectable>();

        collectableScript.OnTriggerEnter2D(crystal.GetComponent<Collider2D>());
        //playerScript.isDisabled = false;

        var actual = collectableScript;
        var collected = true;

        //Verify that the player has collected a crystal
        Assert.AreEqual(collected, actual);
    }

    //Verify that the player has used the ability 
    [Test]
    public void testAbility()
    {
        var player = Object.Instantiate(playerPrefab, Vector2.zero, Quaternion.identity);
        var playerScript = player.GetComponent<PlayerController>();

        playerScript.isDisabled = false;

        AbilityController aController = new AbilityController();


        var actual = aController.runAbility(1);
        var result = true;

        Assert.AreEqual(result, actual);
    }

    //Verify that the player has increased the speed
    [Test]
    public void testSpeedAbility()
    {
        var player = Object.Instantiate(playerPrefab, Vector2.zero, Quaternion.identity);
        var playerScript = player.GetComponent<PlayerController>();
        var crystal = Object.Instantiate(crystalPrefab, Vector2.zero, Quaternion.identity);
        var collectableScript = player.GetComponent<Collectable>();


        //var actual = playerScript.pickAbility(1);
        var result = true;

        //Assert.AreEqual(result, actual);

    }
}
