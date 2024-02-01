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

    public bool enter;
    public TMP_Text enterText;

    private void Start()
    {
        waterChar = PlayerPrefs.GetInt("WaterCharOpen", 0) == 1;
        lightChar = PlayerPrefs.GetInt("LightCharOpen", 0) == 1;
        luckChar = PlayerPrefs.GetInt("LuckCharOpen", 0) == 1;

        enter = false;
    }

    void Update()
    {
        playerMoney = PlayerPrefs.GetInt("GameMoney", 0);
        playerMoneyText.text = playerMoney.ToString();

        if (!enter)
        {
            enterText.fontSize = 35f;
            enterText.text = "캐릭터를" + "\n" + "선택해 주세요.";
        }
        else
        {
            enterText.fontSize = 60f;
            enterText.text = "Start";
        }

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

        enter = true;
        selectChar = 1;
    }

    public void WaterChar()
    {
        if (waterChar)
        {
            waterEx.SetActive(true);
            rockEx.SetActive(false);
            lightEx.SetActive(false);
            luckEx.SetActive(false);

            enter = true;
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

            enter = true;
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

            enter = true;
            selectChar = 4;
        }
    }

    public void OpenWater()
    {
        if (playerMoney >= 100)
        {
            waterChar = true;
            playerMoney -= 100;
            PlayerPrefs.SetInt("WaterCharOpen", 1);
            PlayerPrefs.SetInt("GameMoney", playerMoney);
        }
    }

    public void OpenLight()
    {
        if (playerMoney >= 500)
        {
            lightChar = true;
            playerMoney -= 500;
            PlayerPrefs.SetInt("LightCharOpen", 1);
            PlayerPrefs.SetInt("GameMoney", playerMoney);
        }
    }

    public void OpenLuck()
    {
        if (playerMoney >= 1000)
        {
            luckChar = true;
            playerMoney -= 1000;
            PlayerPrefs.SetInt("LuckCharOpen", 1);
            PlayerPrefs.SetInt("GameMoney", playerMoney);
        }
    }

    public void GameStart()
    {
        if (enter)
        {
            PlayerPrefs.SetInt("SelectChar", selectChar);
            StartCoroutine(GameStartButton());
        }
    }

    IEnumerator GameStartButton()
    {
        yield return new WaitForSeconds(1f);

        LodingController.LoadNextScene("Game");
    }
}
