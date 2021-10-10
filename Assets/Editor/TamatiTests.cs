using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;

public class TamatiTests
{
    private GameObject playerPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Resources/PhotonPrefabs/PlayerBlue.prefab");
    private GameObject sabControllerPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Resources/PhotonPrefabs/SabotageController.prefab");

    //Verify a PlayerController is added to the controllers array
    //Needed so that a sabotage can be targeted at players
    [Test]
    public void TestAddController()
    {
        //Set up test variables
        var player = Object.Instantiate(playerPrefab, Vector2.zero, Quaternion.identity);
        var controller = player.GetComponent<PlayerController>();
        var sabController = Object.Instantiate(sabControllerPrefab, Vector2.zero, Quaternion.identity);
        var sabotage = sabController.GetComponent<SabotageController>();

        //Get the actual test result
        var actual = sabotage.AddPlayerController(controller);

        //Verify the controller has been added to the array
        Assert.AreEqual(controller, actual[0]);
    }

    //Verify that a sabotage has been selected for application to players
    //Only one sabotage was planned for sprint 1, this test was updated in sprint 2 to test the range of sabotages
    [Test]
    public void TestSabotage()
    {
        //Set up test variables
        var player = Object.Instantiate(playerPrefab, Vector2.zero, Quaternion.identity);
        var controller = player.GetComponent<PlayerController>();
        var sabController = Object.Instantiate(sabControllerPrefab, Vector2.zero, Quaternion.identity);
        var sabotage = sabController.GetComponent<SabotageController>();

        //Get the expected and actual test results
        var actual = sabotage.Sabotage(controller, 1);
        var expectedRange = 3; 

        //Verify that a sabotage in the correct range has been selected (range: 1 to 3 for sprint 2)
        Assert.IsTrue(actual > 0);
        Assert.IsTrue(actual <= expectedRange);
    }

    //Verify that the stasis sabotage has been applied to a target player
    //Verify that the source player has NOT been sabotaged
    [Test]
    public void TestStasisTrap()
    {
        //Set up test variables
        var player1 = Object.Instantiate(playerPrefab, Vector2.zero, Quaternion.identity);
        var source = player1.GetComponent<PlayerController>();
        var player2 = Object.Instantiate(playerPrefab, Vector2.zero, Quaternion.identity);
        var target = player2.GetComponent<PlayerController>();
        var sabController = Object.Instantiate(sabControllerPrefab, Vector2.zero, Quaternion.identity);
        var sabotage = sabController.GetComponent<SabotageController>();
        var targetArray = sabotage.AddPlayerController(target);
        GameObject stasisChild = sabotage.transform.GetChild(0).gameObject;
        var stasisTrap = stasisChild.GetComponent<StasisTrap>();
        stasisTrap.unitTesting = true;

        //Enable the players to mimic the race start
        source.isDisabled = false; 
        target.isDisabled = false;

        //Get the expected and actual test results
        var actual = stasisTrap.ApplySabotage(source, targetArray);
        var expected = true;

        //Verify the stasis trap sabotage has been chosen
        Assert.AreEqual(expected, actual);
    }
}
