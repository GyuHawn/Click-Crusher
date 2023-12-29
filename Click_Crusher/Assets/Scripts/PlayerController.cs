using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.TextCore.Text;

public class PlayerController : MonoBehaviour
{
    private Character character;

    public GameObject[] playerHealthUI;
    public int playerHealth;
    public GameObject gameover;

    public float damage;

    public GameObject defenseUI;
    public bool defending;
    public float defenseTime;
    public GameObject defenseCoolTime;
    public TMP_Text defenseCoolTimeText;

    public int money;

    void Start()
    {
        character = GameObject.Find("Manager").GetComponent<Character>();

        defending = false;

        playerHealth = 8;
        UpdateHealthUI();

        if (character.rock)
        {
            damage += character.rockDamage;
        }
    }

    void Update()
    {
        UpdateHealthUI();

        if (defenseTime >= 0)
        {
            defenseCoolTimeText.text = ((int)defenseTime).ToString();
            defenseTime -= Time.deltaTime;
        }
        else
        {
            defenseCoolTime.SetActive(false);
        }      
    }

    void UpdateHealthUI()
    {
        for (int i = 0; i < playerHealthUI.Length; i++)
        {
            playerHealthUI[i].SetActive(false);
        }

        if (playerHealth >= 0)
        {
            playerHealthUI[playerHealth].SetActive(true);
        }

        if (playerHealth <= 0)
        {
            gameover.SetActive(true);
        }
    }

    public void Defense()
    {
        if (defenseTime <= 0)
        {
            StartCoroutine(StartDefense());
        }
    }

    IEnumerator StartDefense()
    {
        defending = true;
        defenseUI.SetActive(true);
        defenseCoolTime.SetActive(true);
        defenseTime = 6;


        yield return new WaitForSeconds(1f);
        defending = false;
        defenseUI.SetActive(false);
    }
}
