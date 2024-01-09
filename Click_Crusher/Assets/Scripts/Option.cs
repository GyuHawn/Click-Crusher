using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option : MonoBehaviour
{
    public GameObject optionMenu;

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void OnOption()
    {
        optionMenu.SetActive(!optionMenu.activeSelf);
    }

    public void MainMenu()
    {
        LodingController.LoadScene("MainMenu");
    }
}
