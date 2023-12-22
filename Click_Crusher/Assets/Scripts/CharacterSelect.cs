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

    public int selectChar;

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

        selectChar = 1;
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

            selectChar = 2;
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

            selectChar = 3;
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

            selectChar = 4;
        }
    }

    public void GameStart()
    {
        PlayerPrefs.SetInt("SelectChar", selectChar);
        LodingController.LoadScene("Game");
    }
}
