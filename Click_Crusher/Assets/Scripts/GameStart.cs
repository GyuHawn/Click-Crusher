using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    public GameObject settingMenu;
    public GameObject resetMenu;

    public void NewGame()
    {
        StartCoroutine(GameStartButton());
    }
    
    IEnumerator GameStartButton()
    {
        yield return new WaitForSeconds(1f);

        LodingController.LoadScene("Character");
    }

    public void OnSettingMenu()
    {
        settingMenu.SetActive(!settingMenu.activeSelf);
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
}
