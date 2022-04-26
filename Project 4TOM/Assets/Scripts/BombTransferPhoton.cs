using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon;

public class BombTransferPhoton : MonoBehaviourPun
{
    public List<GameObject> ListOfPlayers = new List<GameObject>();
    //public List<string> PlayerNickName = new List<string>();
    public bool NoBombOnAllPlayers;
    PhotonView view;

    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
        view.RPC("ListPlayers", RpcTarget.All);
    }

    // Update is called once per frame
    void Update()
    {
        view.RPC("RandomiseBomb", RpcTarget.All);
    }

    [PunRPC]
    void RandomiseBomb()
    {
        foreach (GameObject player in ListOfPlayers)
        {
            if (player.GetComponent<PlayerController>().isBombOnPlayer == false)
            {
                NoBombOnAllPlayers = true;
            }
            else
            {
                NoBombOnAllPlayers = false;
            }
        }

        if (NoBombOnAllPlayers == true)
        {
            ListOfPlayers[(Random.Range(0, ListOfPlayers.Count))].GetComponent<PlayerController>().isBombOnPlayer = true; // technically this should make NoBombOnAllPlayers = false;
        }
    }

    [PunRPC]
    void ListPlayers()
    {
        // checking for each player in the whole scene
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player")) // using lists
        {
            ListOfPlayers.Add(player); // add a player to the list of players
            //PlayerNickName.Add(player.GetComponent<PhotonNetwork>().NickName); //PhotonNetwork.NickName
        }
    }

}
