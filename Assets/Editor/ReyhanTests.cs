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
    private GameObject counterPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Resources/PhotonPrefabs/Counter.prefab");
   
    //This test checks if the player is able to collect a crystal 
    [Test]
    public void testCollectedCrystal()
    {
        var player = Object.Instantiate(playerPrefab, Vector2.zero, Quaternion.identity);
        var playerScript = player.GetComponent<PlayerController>();
        var collectableScript = playerScript.GetComponent<Collectable>();

        //Get the expected and actual test results
        var actual = collectableScript.CollectCrystal(8);
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
        var collectableScript = playerScript.GetComponent<Collectable>();
        var abilityCScript = playerScript.GetComponent<AbilityController>();
        var speedScript = playerScript.GetComponent<SpeedAbility>();

        //Simulate that a player has collected a crystal
        collectableScript.CollectCrystal(8);
 
        //pick the speed ability
        abilityCScript.RunAbility(0, true);
        
        //Get the expected and actual test results
        var actual = speedScript.unitTesting2;
        var result = true;
       
        //Verify that the player has activated there speed ability
        Assert.AreEqual(result, actual);
    }

    //Verify that the player has increased the speed
    [Test]
    public void testSpeedAbility()
    {
        var player = Object.Instantiate(playerPrefab, Vector2.zero, Quaternion.identity);
        var playerScript = player.GetComponent<PlayerController>();
        var collectableScript = playerScript.GetComponent<Collectable>();
        var abilityCScript = playerScript.GetComponent<AbilityController>();
        var speedScript = playerScript.GetComponent<SpeedAbility>();
        
        //Simulate that a player has collected a crystal
        collectableScript.CollectCrystal(8);

        //pick the speed ability
        abilityCScript.RunAbility(0, true);
        
        //Get the expected and actual test results
        var actual = playerScript.movementSpeed;
        var result = 20;
        
        //Verify that the player has movement has been changed
        Assert.AreEqual(result, actual);

    }
}
