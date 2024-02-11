 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    public GameObject settingMenu;
    public float moveDuration = 1.0f;
    private Vector3 startMenuPos;
    private Vector3 endMenuPos;
    private bool onSetting; 

    public GameObject resetMenu;

    public GameObject source;

    private void Start()
    {
        onSetting = false;

        startMenuPos = new Vector3(870f, settingMenu.transform.localPosition.y, settingMenu.transform.localPosition.z);
        endMenuPos = new Vector3(540f, settingMenu.transform.localPosition.y, settingMenu.transform.localPosition.z);
    }

    public void NewGame()
    {
        LodingController.LoadNextScene("Character");
        //   StartCoroutine(GameStartButton());
    }
    
    IEnumerator GameStartButton()
    {
        yield return new WaitForSeconds(1f);

        LodingController.LoadNextScene("Character");
    }

    public void OnSettingMenu()
    {
        StartCoroutine(MoveSettingMenu());
    }

    IEnumerator MoveSettingMenu()
    {
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

    public void OnSource()
    {
        source.SetActive(!source.activeSelf);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
