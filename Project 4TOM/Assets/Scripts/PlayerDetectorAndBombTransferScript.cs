using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using Photon.Pun;

public class PlayerDetectorAndBombTransferScript : MonoBehaviour
{
    public bool GameIsStarted = false;
    public int NumberOfActivePlayers; // how many players are left in the game
    public int PlayerCheckLoop1 = 0; // first checking to determine how many players start the game
    public int PlayersWithBomb;
    public int RandomPlayer;

    public List<GameObject> ListOfPlayers = new List<GameObject>();
    //public GameObject WinScreenPanel;
    public TextMeshProUGUI WinPlayerName; // to show name of winning player


    // Start is called before the first frame update
    void Start() // because it manually turns off first by the game manager, it already started and detected that there is no players.
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (view.IsMine)
        {
            // if enough players, start game
            while (PlayerCheckLoop1 < 1) // because it manually turns off first by the game manager, it already started and detected that there is no players, so must also check whether game started or not.
            {
                if (GameIsStarted == true) // Check if the game has started
                {
                    // checking for each player in the whole scene
                    foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player")) // using lists
                    {
                        ListOfPlayers.Add(player); // add a player to the list of players
                    }
                }
                NumberOfActivePlayers = ListOfPlayers.Count; // only check number of active players once, then manually reduce it when a player loses
                PlayerCheckLoop1++; // PlayerCheckLoop is to ensure the check is only done once after the game starts, or it will keep adding players for list.

            }
            if (GameIsStarted == true)
            {
                UpdateList();
                RandomiseBombToPlayerList();
            }
        }
        //ShowWinScreen();
       
    }

    private void UpdateList()
    {
        // detecting if a player has died
        if (ListOfPlayers.Count > 1)
        {
            foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player")) // checking for each player in the list of players
            {
                {
                    if (player.GetComponent<PlayerController>().isBombOnPlayer == true)
                    {
                        player.GetComponent<PlayerController>().bombImage.SetActive(true);
                    }
                    else
                    {
                        player.GetComponent<PlayerController>().bombImage.SetActive(false);
                    }
                    //NumberOfActivePlayers = 0; // reset to count number of active players
                    if (player.GetComponent<PlayerController>().currentLives <= 0) // if current player loses 
                    {
                        player.GetComponent<PlayerController>().GamePosition = ListOfPlayers.Count; // set which place the player got in the game
                        player.GetComponent<PlayerController>().isBombOnPlayer = false;
                        ListOfPlayers.Remove(player);
                        Destroy(player); // will this work?
                        
                        NumberOfActivePlayers--;
                        PlayersWithBomb = 0; // After player dies, no player has bomb anymore, so need to set
                        
                    }

                    if (player.GetComponent<PlayerController>().currentHealthPoints <= 0)
                    {
                        player.GetComponent<PlayerController>().isBombOnPlayer = false;
                        PlayersWithBomb = 0;
                    }

                }
                
            } // end of foreach
        }

    }


    private void RandomiseBombToPlayerList()
    {
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (player.GetComponent<PlayerController>().isBombOnPlayer == false)
            {
                PlayersWithBomb = 0;
            }
            else if(player.GetComponent<PlayerController>().isBombOnPlayer == true)
            {
                PlayersWithBomb++;
            }

            if (PlayersWithBomb == 0) // it straight away checks
            {
                // randomise bomb
                RandomPlayer = Random.Range(0, ListOfPlayers.Count);
                ListOfPlayers[RandomPlayer].GetComponent<PlayerController>().isBombOnPlayer = true; // this causes all players to get the bomb
                //GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().isBombOnPlayer = true; // bomb will random to each player, so any player will always see themselves having the bomb
                PlayersWithBomb = 1;
                
            }

        }

    }

    void ShowWinScreen()
    {
        if (ListOfPlayers.Count == 1)
        {
            //exWinScreenPanel.SetActive(true);
            //WinPlayerName.text = GameObject.FindGameObjectWithTag("Player").name.ToString(); // find the winner. need to find a way to get player name generated by photon
        }

    }

}
