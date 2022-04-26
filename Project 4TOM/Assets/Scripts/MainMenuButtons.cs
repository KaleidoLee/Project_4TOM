using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenuButtons : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gameTitle;
    private static Material m_TextBaseMaterial;
    private static Material m_TextHighlightMaterial;
    
    private float glowIntensity = 0.3f;
    [SerializeField] private float glowRate;
    private bool glowDown = true;

    public AudioMixer audioMixer;

    private void Start()
    {
        //Get a reference to the default base material
        m_TextBaseMaterial = gameTitle.fontSharedMaterial;
        
        //Create new instance of the material assigned to the text object
        //Assumes all text objects will use the same highlight
        m_TextHighlightMaterial = new Material(m_TextBaseMaterial);
    }

    private void Update()
    {
        if (glowDown == true)
        {
            glowIntensity -= glowRate * Time.deltaTime;
        }
        else
        {
            glowIntensity += glowRate * Time.deltaTime;
        }

        if (glowIntensity >= 0.3)
            glowDown = true;
        else if (glowIntensity <= 0)
            glowDown = false;
        
        m_TextHighlightMaterial.SetFloat(ShaderUtilities.ID_GlowPower, glowIntensity);
        gameTitle.fontSharedMaterial = m_TextHighlightMaterial;
        gameTitle.UpdateMeshPadding();
        Debug.Log(glowIntensity);
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

    public void BGMVolume(float value)
    {
        audioMixer.SetFloat("BGM", value);
    }

    public void SFXVolume(float value)
    {
        audioMixer.SetFloat("SFX", value);
    }
}
