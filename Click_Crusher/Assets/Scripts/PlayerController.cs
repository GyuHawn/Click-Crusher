using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Character character;
    private StageManager stageManager;

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

    private float gameTime;
    public TMP_Text gameTimeText;

    void Start()
    {
        character = GameObject.Find("Manager").GetComponent<Character>();
        stageManager = GameObject.Find("Manager").GetComponent<StageManager>();

        defending = false;

        playerHealth = 8;
        UpdateHealthUI();

        if (character.rock)
        {
            damage += character.rockDamage;
        }

        gameTime = 0f;
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

        // 게임 시간 증가
        gameTime += Time.deltaTime;

        // 시간을 텍스트로 표시
        gameTimeText.text = string.Format("{0:00}:{1:00}", Mathf.Floor(gameTime / 60), gameTime % 60);
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
            playerHealthUI[0].SetActive(true);
            Time.timeScale = 0f;
            stageManager.gameStart = false;
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

        yield return new WaitForSeconds(3f);

        defending = false;
        defenseUI.SetActive(false);
    }
}
