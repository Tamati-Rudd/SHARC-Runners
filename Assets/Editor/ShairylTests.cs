using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using UnityEditor;
using Photon.Pun;
using Photon.Chat;

public class ShairylTests
{
    private GameObject chatholder = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Resources/PhotonPrefabs/ChatHandler.prefab");
   
    //This Test checks to see if a connection to a server can be established
    //Needed for the chat system to work
    [Test]
    public void testConnection()
    {
        //string user_id = PlayerPrefs.GetString("username");
        var chat = Object.Instantiate(chatholder, Vector2.zero, Quaternion.identity);
        var chatScript = chat.GetComponent<PhotonChatManager>();

        chatScript.chatClient = new ChatClient(chatScript);

        //attempt to establish connection
        bool actual =  chatScript.chatClient.Connect(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat, PhotonNetwork.AppVersion,
            new AuthenticationValues("test"));

    
        //connection should be successful
        Assert.AreEqual(true, actual);
    }

    [Test]
    public void testSubscribing()
    {
        //string user_id = PlayerPrefs.GetString("username");
        var chat = Object.Instantiate(chatholder, Vector2.zero, Quaternion.identity);
        var chatScript = chat.GetComponent<PhotonChatManager>();
 

        chatScript.chatClient = new ChatClient(chatScript);

        //Establish connection
        chatScript.chatClient.Connect(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat, PhotonNetwork.AppVersion,
            new AuthenticationValues("test"));

        //if connection was successfull
        if (chatScript.chatClient.CanChat == true)
        {
            //attempt to subscribe to a channel
           var actual = chatScript.chatClient.Subscribe(new string[] { "test" });

            Assert.AreEqual(true, actual);
        }
        

       
       
    }


    [Test]
    public void testPublishingMsg()
    {
        //string user_id = PlayerPrefs.GetString("username");
        var chat = Object.Instantiate(chatholder, Vector2.zero, Quaternion.identity);
        var chatScript = chat.GetComponent<PhotonChatManager>();

        chatScript.chatClient = new ChatClient(chatScript);

        //Establish connection
        chatScript.chatClient.Connect(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat, PhotonNetwork.AppVersion,
            new AuthenticationValues("test"));

        chatScript.OnConnected();


        if (chatScript.chatClient.CanChat == true)
        {
            //attempt to send message to server
            bool actual = chatScript.chatClient.PublishMessage("TestChannel", "Test Message"); ;
            Assert.AreEqual(true, actual);
        }

        //attempt to send message
       
       
    }



}
