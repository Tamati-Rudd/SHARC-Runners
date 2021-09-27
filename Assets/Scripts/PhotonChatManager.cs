using ExitGames.Client.Photon;
using Photon.Chat;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PhotonChatManager : MonoBehaviour, IChatClientListener
{
    ChatClient chatClient;
     
    public TMP_InputField msg;
    public TMP_Text display;
    //TMP_Text chatDisplay;



    public void DebugReturn(DebugLevel level, string message)
    {
       
    }

    public void OnChatStateChange(ChatState state)
    {
        
    }
    public void OnConnected()
    {
        chatClient.Subscribe(new string[] { PhotonNetwork.CurrentRoom.Name }); //subscribe to chat channel once connected to server
    }

    public void OnDisconnected()
    {
        throw new System.NotImplementedException();
    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        int msgCount = messages.Length;
        for (int i = 0; i < msgCount; i++)
        { //go through each received msg
            string sender = senders[i];
            
            string msg = (string) messages[i];
            display.text = display.text + "\n" + sender + ": " +msg;
            
        }
    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        throw new System.NotImplementedException();
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        throw new System.NotImplementedException();
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        Debug.Log("Subscribed to a new channel!");
    }

    public void OnUnsubscribed(string[] channels)
    {
        throw new System.NotImplementedException();
    }

    public void OnUserSubscribed(string channel, string user)
    {
        throw new System.NotImplementedException();
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        //establishing connection with the server
        string user_id = PlayerPrefs.GetString("username");
        chatClient = new ChatClient(this);
        chatClient.Connect(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat, PhotonNetwork.AppVersion,
            new AuthenticationValues(user_id));
        

    }

    public void SendMsg()
    {
        if(string.IsNullOrEmpty(msg.text))
        {
            return;
        }

        chatClient.PublishMessage(PhotonNetwork.CurrentRoom.Name, msg.text);
        msg.text = "";


    }

    // Update is called once per frame
    void Update()
    {
        if (chatClient != null) {
            chatClient.Service();
        }
    }

   public void Disconnect()
    {
        if (chatClient != null) { chatClient.Disconnect(); }
    }

    void OnApplicationQuit()
    {
        if (chatClient != null) { chatClient.Disconnect(); }
    }

}
