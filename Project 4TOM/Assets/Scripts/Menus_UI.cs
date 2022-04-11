using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menus_UI : MonoBehaviour
{
    //HUD
    public Image RingHPBar;
    public Image P2_RingHPBar;
    public Image P3_RingHPBar;
    public Image P4_RingHPBar;

    public Image[] Lives;
    public Image[] P2_Lives;
    public Image[] P3_Lives;
    public Image[] P4_Lives;

    public Image Alive_VFX;
    public Image P2_Alive_VFX;
    public Image P3_Alive_VFX;
    public Image P4_Alive_VFX;
    //public Image[] HealthPoints; //Different portions of HP in the UI

    public float Player1;
    public float Player2;
    public float Player3;
    public float Player4;

    public float CurrentHP, MaxHP = 100;
    public float P2_CurrentHP, P2_MaxHP = 100;
    public float P3_CurrentHP, P3_MaxHP = 100;
    public float P4_CurrentHP, P4_MaxHP = 100;

    public int CurrentLives, MaxLives = 3;
    public int P2_CurrentLives, P2_MaxLives = 3;
    public int P3_CurrentLives, P3_MaxLives = 3;
    public int P4_CurrentLives, P4_MaxLives = 3;

    float LerpSpeed; //Speed of changing the value of fill amount

    //Menus
    public GameObject GameOverMenu;
    public GameObject PauseMenu;
    public static bool GameIsPaused;
    public static bool GameIsOver;

    //Start is called before the first frame update
    void Start()
    {
        CurrentHP = MaxHP;
        CurrentLives = MaxLives;
    }

    // Update is called once per frame
    void Update()
    {
       //HUD
        //CurrentHP = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().currentHealthPoints;
        //CurrentLives = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().currentLives;

        if (CurrentHP > MaxHP)
        {
            CurrentHP = MaxHP;
        }

        LerpSpeed = 3f * Time.deltaTime;

        //Menus
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    if (GameIsPaused)
        //    {
        //        Resume();
        //    }
        //    else
        //    {
        //        Pause();
        //    }

        //}

        //if(PlayerIsAlive = false)
        //{
        //    Alive_VFX.enabled;
        //}

        //Function Call
        HPBarFiller();
        ColorChanger();
        DisplayCurrentLives();
        //GameOver();
    }
    
    //HUD
    void HPBarFiller() //Fill the HP Bar based on Current HP
    {
        RingHPBar.fillAmount = Mathf.Lerp(RingHPBar.fillAmount, (CurrentHP / MaxHP), LerpSpeed); //Changing the value of fill amount with HP
    }

    void DisplayCurrentLives() //Display Current Lives on HUD
    {
        for (int i = 0; i < Lives.Length; i++)
        {
            Lives[i].enabled = !DisplayLives(CurrentLives, i);
        }
    }

    void ColorChanger() //Change the colour of the HP bar fill based on current HP
    {
        Color HPColour = Color.Lerp(Color.red, Color.green, (CurrentHP / MaxHP));
        RingHPBar.color = HPColour;
    }

    bool DisplayLives(float CurrentLives, int DisplayedLives) //Formula: Displayed Lives = Current Lives
    {
        return ((DisplayedLives) >= CurrentLives);
    }

    //public void Damage(float damagePoints)
    //{
    //    if (CurrentHP > 0)
    //        CurrentHP -= damagePoints;

    //    else if (CurrentHP <= 0)
    //    {
    //        CurrentLives -= 1;
    //    }
    //}
    //public void Heal(float healingPoints)
    //{
    //    if (CurrentHP < MaxHP)
    //        CurrentHP += healingPoints;
    //}

    //Menus
    public void Resume() //Resume Game
    {
        PauseMenu.SetActive(false);
        //Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause() //Pause and show Pause Menu
    {
        PauseMenu.SetActive(true);
        //Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public void PlayButton() //Load lobby scene
    {
        SceneManager.LoadScene("LobbySceneNew");
    }

    public void QuitButton() //Quit the game
    {
        Debug.Log("Quit the game");
        Application.Quit();
    }

//    public void GameOver() //Game Over and show Game Over Menu
//    {
//        if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().currentHP <= 0)
//        {
//            Time.timeScale = 0f;
//            GameOverMenu.SetActive(true);
//        }
//    }
}
