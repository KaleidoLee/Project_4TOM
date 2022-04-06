using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class CreateAndJoinRoom : MonoBehaviourPunCallbacks
{
    //public InputField CreateInput;
    //public InputField JoinInput;
    public TMP_InputField Input;
    string roomname;

    public void CreateRoom()
    {
        if (string.IsNullOrEmpty(Input.text))
        {
            return;
        }
        PhotonNetwork.CreateRoom(Input.text);
    }

    public void JoinRoom()
    {
        if (string.IsNullOrEmpty(Input.text))
        {
            return;
        }
        PhotonNetwork.JoinRoom(Input.text);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("SampleScene");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
    }
}
