using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class RoomListItem : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_Text text;
    public RoomInfo info;
    
    public void SetUp(RoomInfo _info)
    {
        SetRoomInfo(_info);
        text.text = _info.Name;
    }

    public void OnClick()
    {
        PhotonNetwork.JoinRoom(info.Name);
        MenuManager.Instance.OpenMenu("Loading");
    }

    public void SetRoomInfo(RoomInfo info)
    {
        this.info = info;
    }

    public RoomInfo getRoomInfo()
    {
        return info;
    }
}
