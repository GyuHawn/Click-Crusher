using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour
{
    public GameObject rockEx;
    public GameObject waterEx;
    public GameObject lightEx;
    public GameObject luckEx;

    private bool waterChar = false;
    private bool lightChar = false;
    private bool luckChar = false;

    public GameObject useWater;
    public GameObject useLight;
    public GameObject useLuck;

    void Start()
    {
        
    }

    
    void Update()
    {
        if (waterChar)
        {
            useWater.SetActive(false);
        }
        if (lightChar)
        {
            useLight.SetActive(false);
        }
        if (luckChar)
        {
            useLuck.SetActive(false);
        }
    }

    public void RockChar()
    {
        rockEx.SetActive(true);
        waterEx.SetActive(false);
        lightEx.SetActive(false);
        luckEx.SetActive(false);
        waterChar = true;
    }

    public void WaterChar()
    {
        if(waterChar)
        {
            waterEx.SetActive(true);
            rockEx.SetActive(false);
            lightEx.SetActive(false);
            luckEx.SetActive(false);
            lightChar = true;
        }
    }

    public void LightChar()
    {
        if (lightChar)
        {
            lightEx.SetActive(true);
            rockEx.SetActive(false);
            waterEx.SetActive(false);
            luckEx.SetActive(false);
            luckChar = true;
        }
    }

    public void LuckChar()
    {
        if (luckChar)
        {
            luckEx.SetActive(true);
            rockEx.SetActive(false);
            waterEx.SetActive(false);
            lightEx.SetActive(false);
        }
    }

    public void GameStart()
    {
        LodingController.LoadScene("Game");
    }
}
