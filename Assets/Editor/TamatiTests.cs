using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TamatiTests
{
    //Verify a PlayerController is added to the controllers array
    //Needed so that a sabotage can be targeted at players
    [Test]
    public void TestAddController()
    {
        //Set up test variables
        var controller = new PlayerController();
        var sabotage = new SabotageController();

        //Get the actual test result
        var actual = sabotage.addPlayerController(controller);

        //Verify the controller has been added to the array
        Assert.AreEqual(controller, actual[0]);
    }

    //Verify that a sabotage has been selected for application to players
    //Only one sabotage is planned for sprint 1, but more may be added in the future
    [Test]
    public void TestSabotage()
    {
        //Set up test variables
        var controller = new PlayerController();
        var sabotage = new SabotageController();

        //Get the expected and actual test results
        var actual = sabotage.sabotage(controller);
        var expected = 1; //1 represents StasisTrap

        //Verify the stasis trap sabotage has been chosen
        Assert.AreEqual(expected, actual);
    }

    //Verify that the stasis sabotage has been applied to a target player
    //Verify that the source player has NOT been sabotaged
    [Test]
    public void TestStasisTrap()
    {
        //Set up test variables
        var source = new PlayerController();
        var target = new PlayerController();
        var sabotage = new SabotageController();
        var targetArray = sabotage.addPlayerController(target);
        var stasisTrap = new StasisTrap();

        //Get the expected and actual test results
        var actual = stasisTrap.applySabotage(source, targetArray);
        var expected = true;

        //Verify the stasis trap sabotage has been chosen
        Assert.AreEqual(expected, actual);
    }
}
