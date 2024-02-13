 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.ComponentModel;
using UnityEditor;
using TMPro;

public class GameStart : MonoBehaviour
{
    public GameObject settingMenu;
    public float moveDuration = 1.0f;
    private Vector3 startMenuPos;
    private Vector3 endMenuPos;
    private bool onSetting; 

    public GameObject resetMenu;

    public GameObject source;

    public GameObject tutorialMenu;
    public GameObject[] tutorials;
    public int tutorialNum;

    public TMP_Text startMenuText;
    public TMP_Text settingMenuText;
    public TMP_Text tutorialMenuText;
    public TMP_Text exitMenuText;



    private void Start()
    {
        onSetting = false;

        tutorialNum = -1;

        startMenuPos = new Vector3(870f, settingMenu.transform.localPosition.y, settingMenu.transform.localPosition.z);
        endMenuPos = new Vector3(540f, settingMenu.transform.localPosition.y, settingMenu.transform.localPosition.z);

        startMenuText.color = Color.black;
        settingMenuText.color = Color.black;
        tutorialMenuText.color = Color.black;
        exitMenuText.color = Color.black;
    }
    private void Update()
    {
        for (int i = 0; i < tutorials.Length; i++)
        {
            tutorials[i].SetActive(i == tutorialNum);
        }

        if(tutorialNum >= 6)
        {
            tutorialMenuText.color = Color.black;
            tutorialMenu.SetActive(false);
        }
    }

    public void NewGame()
    {
        startMenuText.color = Color.white;
        LodingController.LoadNextScene("Character");
    }

    public void OnSettingMenu()
    {
        StartCoroutine(MoveSettingMenu());
    }

    IEnumerator MoveSettingMenu()
    {
        settingMenuText.color = Color.white;

        if (!onSetting)
        {
            float elapsed = 0f;

            while (elapsed < moveDuration)
            {
                settingMenu.transform.localPosition = Vector3.Lerp(startMenuPos, endMenuPos, elapsed / moveDuration);
                elapsed += Time.deltaTime;
                yield return null;
            }

            settingMenu.transform.localPosition = endMenuPos;
            onSetting = true;
        }
        else
        {
            float elapsed = 0f;

            while (elapsed < moveDuration)
            {
                settingMenu.transform.localPosition = Vector3.Lerp(endMenuPos, startMenuPos, elapsed / moveDuration);
                elapsed += Time.deltaTime;
                yield return null;
            }

            settingMenu.transform.localPosition = startMenuPos;
            onSetting = false;
        }

        settingMenuText.color = Color.black;
    }

    public void OnResetMenu()
    {
        resetMenu.SetActive(!resetMenu.activeSelf);
    }

    public void ResetGame()
    {
        PlayerPrefs.DeleteAll();
        resetMenu.SetActive(false);
        settingMenu.SetActive(false);
    }
    public void OnTutorial()
    {
        tutorialMenuText.color = Color.white;
        tutorialMenu.SetActive(true);
        tutorialNum = 0;
    }

    public void NextTutorial()
    {
        if(tutorialNum < tutorials.Length)
        {
            tutorialNum++;
        }
    }
    
    public void BeforeTutorial()
    {
        if (tutorialNum > 0)
        {
            tutorialNum--;
        }     
    }

    public void OnSource()
    {
        source.SetActive(!source.activeSelf);
    }

    public void Exit()
    {
        exitMenuText.color = Color.black;
        Application.Quit();
    }
}
