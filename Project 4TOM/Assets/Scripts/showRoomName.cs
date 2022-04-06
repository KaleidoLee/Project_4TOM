using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class showRoomName : MonoBehaviour
{
    public string RoomName;
    // Start is called before the first frame update
    void Start()
    {
        RoomName = PhotonNetwork.CurrentRoom.Name;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        gameObject.GetComponent<TextMeshProUGUI>().SetText("RoomName : " + RoomName);
            
    }
}
