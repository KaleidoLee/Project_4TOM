using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class playerListItem : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_Text playerNameText;

    Player player;

    public void setUp(Player player1)
    {
        player = player1;
        playerNameText.text = player1.NickName;
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("on player left room runned");
        if (player == otherPlayer)
        {
            Destroy(gameObject);
        }
    }

    //public override void OnLeftRoom()
    //{
    //    Debug.Log("on left room runned");
    //    Destroy(gameObject);
    //}

}
