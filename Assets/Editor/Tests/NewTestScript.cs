using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class NewTestScript
{

    private GameObject chatholder = UnityEditor.AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Resources/PhotonPrefabs/ChatHandler.prefab");

    // A Test behaves as an ordinary method

    [SetUp]
    public void Setup()
    {
        GameObject Chatprefab = GameObject.Instantiate(chatholder, Vector2.zero, Quaternion.identity);

    }
    // A Test behaves as an ordinary method
    [Test]
    public void testConnection()
    {
       
    }

}
