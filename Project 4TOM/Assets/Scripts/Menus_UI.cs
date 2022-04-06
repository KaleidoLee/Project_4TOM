using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menus_UI : MonoBehaviour
{
    //In-Game UIs
    public Image RingHPBar;
    public Image LivesUI;
    public Image[] HealthPoints; //Different portions of HP in the UI

    float currentHP, maxHP = 100;
    float LerpSpeed; //Speed of changing the value of fill amount

    //Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHP > maxHP)
        { 
            currentHP = maxHP; 
        }

        LerpSpeed = 3f * Time.deltaTime;
        
        //Function Call
        HPBarFiller();
        ColorChanger();
       
    }
    
    //In-Game UIs
    void HPBarFiller()
    {
        RingHPBar.fillAmount = Mathf.Lerp(RingHPBar.fillAmount, (currentHP / maxHP), LerpSpeed); //Changing the value of fill amount with HP

        for (int i = 0; i < HealthPoints.Length; i++)
        {
            HealthPoints[i].enabled = !DisplayHealthPoint(currentHP, i);
        }
    }

    void ColorChanger()
    {
        Color HPColour = Color.Lerp(Color.red, Color.green, (currentHP / maxHP));
        RingHPBar.color = HPColour;
    }

    bool DisplayHealthPoint(float _health, int pointNumber)
    {
        return ((pointNumber * 10) >= _health);
    }

    public void Damage(float damagePoints)
    {
        if (currentHP > 0)
            currentHP -= damagePoints;
    }
    public void Heal(float healingPoints)
    {
        if (currentHP < maxHP)
            currentHP += healingPoints;
    }

    //Menus
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
