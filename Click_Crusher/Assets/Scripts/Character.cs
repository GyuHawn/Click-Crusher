using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{
    public int currentCharacter;

    public float rockDamage;

    public GameObject waterEffect;
    public float waterDamage;

    public bool rock;
    public bool water;
    public bool lihgt;
    public bool luck;

    void Start()
    {
        currentCharacter = PlayerPrefs.GetInt("SelectChar");

        rock = false;
        water = false;
        lihgt = false;
        luck = false;
    }

    
    void Update()
    {
        if(currentCharacter == 1) 
        {
            rock = true;
        }
        else if(currentCharacter == 2)
        {
            water = true;
        }
        else if(currentCharacter == 3)
        {
            lihgt = true;
        }
        else if(currentCharacter==4)
        {
            luck = true;
        }
    }

    public void Water(Vector3 targetPosition)
    {
        GameObject waterInstance = Instantiate(waterEffect, targetPosition, Quaternion.Euler(90, 0, 0));

        Destroy(waterInstance, 0.5f);
    }  

    public void MainMenu() // 테스트 오류시 이동용 버튼
    {
        LodingController.LoadScene("MainMenu");
    }
}
