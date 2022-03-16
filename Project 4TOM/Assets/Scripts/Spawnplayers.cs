using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Spawnplayers : MonoBehaviour
{

    public GameObject playerPrefab;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    // use to determine who is what player number
    //public int playerNumber = 1;
    private void Start()
    {
        Vector2 randomPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);
        //playerPrefab.GetComponent<PlayerController>().PlayerNumber = playerNumber; // this is adding to the player in prefab, not in resources. why?
        //playerNumber++;
    }
    
}
