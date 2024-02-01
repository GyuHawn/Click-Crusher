using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option : MonoBehaviour
{
    public GameObject settingMenu;
    public float moveDuration = 1.0f;
    private Vector3 startMenuPos;
    private Vector3 endMenuPos;
    private bool onSetting;

    private void Start()
    {
        onSetting = false;

        startMenuPos = new Vector3(950f, settingMenu.transform.localPosition.y, settingMenu.transform.localPosition.z);
        endMenuPos = new Vector3(360f, settingMenu.transform.localPosition.y, settingMenu.transform.localPosition.z);
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

    public void MainMenu()
    {
        LodingController.LoadNextScene("MainMenu");
    }
}
