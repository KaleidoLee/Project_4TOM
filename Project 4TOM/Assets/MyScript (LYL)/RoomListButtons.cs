using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using TMPro;
using Photon.Pun;

public class RoomListButtons : MonoBehaviour
{
    [SerializeField] TMP_Text roomNameText;
    public RoomInfo info;

    public void setUp(RoomInfo roomInfo)
    {
        info = roomInfo;
        roomNameText.text = info.Name;
    }

    public void JoinRoomClick()
    {
        PhotonLauncher.launcherInstance.joinRoom(info);
    }
}
