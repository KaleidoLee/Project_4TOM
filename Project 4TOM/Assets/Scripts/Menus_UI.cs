using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menus_UI : MonoBehaviour
{
    //In-Game UIs
    public Image RingHPBar;
    public Image[] Lives;
    //public Image[] HealthPoints; //Different portions of HP in the UI

    public float CurrentHP, MaxHP = 100;
    public int CurrentLives, MaxLives = 3;
    float LerpSpeed; //Speed of changing the value of fill amount

    //Start is called before the first frame update
    void Start()
    {
        CurrentHP = MaxHP;
        CurrentLives = MaxLives;
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentHP > MaxHP)
        { 
            CurrentHP = MaxHP; 
        }

        LerpSpeed = 3f * Time.deltaTime;
        
        //Function Call
        HPBarFiller();
        ColorChanger();
       
    }
    
    //In-Game UIs
    void HPBarFiller()
    {
        RingHPBar.fillAmount = Mathf.Lerp(RingHPBar.fillAmount, (CurrentHP / MaxHP), LerpSpeed); //Changing the value of fill amount with HP

        for (int i = 0; i < Lives.Length; i++)
        {
            Lives[i].enabled = !DisplayHealthPoint(CurrentLives, i);
        }
    }

    void ColorChanger()
    {
        Color HPColour = Color.Lerp(Color.red, Color.green, (CurrentHP / MaxHP));
        RingHPBar.color = HPColour;
    }

    bool DisplayHealthPoint(float CurrentLives, int DisplayedLives)
    {
        return ((DisplayedLives) >= CurrentLives);
    }

    public void Damage(float damagePoints)
    {
        if (CurrentHP > 0)
            CurrentHP -= damagePoints;

        //else if (CurrentHP <= 0)
        //{
        //    CurrentLives -= 1;
        //}
    }
    public void Heal(float healingPoints)
    {
        if (CurrentHP < MaxHP)
            CurrentHP += healingPoints;
    }

    //Menus
    public void PlayButton() //Load lobby scene
    {
        SceneManager.LoadScene("LobbySceneNew");
    }

    public void QuitButton() //Quit the game
    {
        Debug.Log("Quit the game");
        Application.Quit();
    }
}
