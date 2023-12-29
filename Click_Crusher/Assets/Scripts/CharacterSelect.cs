using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

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

    public int playerMoney;
    public TMP_Text playerMoneyText;

    public GameObject waterOpenBtn;
    public GameObject lightOpenBtn;
    public GameObject luckOpenBtn;

    void Update()
    {
        playerMoneyText.text = playerMoney.ToString();

        if (waterChar)
        {
            useWater.SetActive(false);
            waterOpenBtn.SetActive(false);
        }
        if (lightChar)
        {
            useLight.SetActive(false);
            lightOpenBtn.SetActive(false);
        }
        if (luckChar)
        {
            useLuck.SetActive(false);
            luckOpenBtn.SetActive(false);
        }
    }

    public void RockChar()
    {
        rockEx.SetActive(true);
        waterEx.SetActive(false);
        lightEx.SetActive(false);
        luckEx.SetActive(false);

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

    public void OpenWater()
    {
        if(playerMoney >= 100)
        {
            waterChar = true;
            playerMoney -= 100;
        }
    }
    public void OpenLight()
    {
        if (playerMoney >= 500)
        {
            lightChar = true;
            playerMoney -= 500;
        }      
    }
    public void OpenLuck()
    {
        if (playerMoney >= 1000)
        {
            luckChar = true;
            playerMoney -= 1000;
        }       
    }


    public void GameStart()
    {
        PlayerPrefs.SetInt("SelectChar", selectChar);
        LodingController.LoadScene("Game");
    }
}
