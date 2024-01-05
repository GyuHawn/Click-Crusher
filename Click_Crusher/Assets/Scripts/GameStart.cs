using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    public void NewGame()
    {
        StartCoroutine(GameStartButton());
    }
    
    IEnumerator GameStartButton()
    {
        yield return new WaitForSeconds(1f);

        LodingController.LoadScene("Character");
    }

    public void ResetGame()
    {
        PlayerPrefs.DeleteAll();
    }
}
