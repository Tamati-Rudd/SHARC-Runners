using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

public class ShairylTests
{

    private GameObject chatholder = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Resources/PhotonPrefabs/ChatHandler.prefab");
    
    // A Test behaves as an ordinary method

    [SetUp]
    public void Setup()
    {
        GameObject Chatprefab = GameObject.Instantiate(chatholder, Vector2.zero, Quaternion.identity);

    }



}
