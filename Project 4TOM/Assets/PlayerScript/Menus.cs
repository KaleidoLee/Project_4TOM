using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
    public void PlayButton() //Load lobby scene
    {
        SceneManager.LoadScene("LoadingScreen");
    }

    public void QuitButton() //Quit the game
    {
        Debug.Log("Quit the game");
        Application.Quit();
    }
}
