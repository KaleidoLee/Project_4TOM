using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Spawnplayers : MonoBehaviour
{

    public GameObject playerPrefab;
    public GameObject Player1;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    private void Start()
    {
        Vector2 randomPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);
        //Player1 = PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);
    }
    
}
