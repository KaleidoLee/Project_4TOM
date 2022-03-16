using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public int NumberOfPlayers;
    public GameObject BombTransferScriptObject;
    //public GameObject PlayerDetectorObject;
    // Start is called before the first frame update
    void Start()
    {
        BombTransferScriptObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Use this script to call UI Menus such as Pause Menu and Win Screen
        DetermineNumberOfPlayers();
        if (NumberOfPlayers == 4)
        {
            StartGame();
        }
    }

    private void DetermineNumberOfPlayers()
    {
        NumberOfPlayers = GameObject.FindGameObjectsWithTag("Player").Length;
    }

    private void StartGame()
    {
        BombTransferScriptObject.SetActive(true);
        PlayerDetectorAndBombTransferScript PlayerDetectorScript = BombTransferScriptObject.GetComponent<PlayerDetectorAndBombTransferScript>();
        PlayerDetectorScript.GameIsStarted = true;
        PlayerDetectorScript.PlayersWithBomb = 0; // set 0, so that it will have to randomise first at the start of the game
    }
}
