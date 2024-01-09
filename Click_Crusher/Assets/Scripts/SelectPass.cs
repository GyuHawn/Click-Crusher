using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectPass : MonoBehaviour
{
    private PlayerController playerController;
    private StageManager stageManager;

    public GameObject passMenu;

    public int selecPass;

    public TMP_Text passName;
    public TMP_Text passEx;

    private void Start()
    {
        playerController = GameObject.Find("Manager").GetComponent<PlayerController>();
        stageManager = GameObject.Find("Manager").GetComponent<StageManager>();

        selecPass = 0;
    }

    public void powerUP()
    {
        selecPass = 1;
        passName.text = "공격력 상승";
        passEx.text = "플레이어의 공격력이 향상됩니다.";
    }
    public void TimeUP()
    {
        selecPass = 2;
        passName.text = "시간 추가";
        passEx.text = "스테이지의 제한시간이 확장됩니다.";
    }
    public void fullHealth()
    {
        selecPass = 3;
        passName.text = "체력회복";
        passEx.text = " 체력이 완전히 회복됩니다.";
    }
    public void GetMoney()
    {
        selecPass = 4;
        passName.text = "머니 획득";
        passEx.text = "게임 머니를 200원 획득합니다.";
    }

    public void EnterPass()
    {
        if (selecPass == 1)
        {
            playerController.damage += 10;
        }
        else if (selecPass == 2)
        {
            stageManager.timeLimit += 10;
        }
        else if (selecPass == 3)
        {
            playerController.playerHealth = 8;
        }
        else if (selecPass == 4)
        {
            PlayerPrefs.SetInt("GameMoney", PlayerPrefs.GetInt("GameMoney", 0) + 200);
        }

        stageManager.selectingItem = false;
        playerController.isAttacking = false;
        Time.timeScale = 1f;
        passMenu.SetActive(false);
    }
}
