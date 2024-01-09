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
        passName.text = "���ݷ� ���";
        passEx.text = "�÷��̾��� ���ݷ��� ���˴ϴ�.";
    }
    public void TimeUP()
    {
        selecPass = 2;
        passName.text = "�ð� �߰�";
        passEx.text = "���������� ���ѽð��� Ȯ��˴ϴ�.";
    }
    public void fullHealth()
    {
        selecPass = 3;
        passName.text = "ü��ȸ��";
        passEx.text = " ü���� ������ ȸ���˴ϴ�.";
    }
    public void GetMoney()
    {
        selecPass = 4;
        passName.text = "�Ӵ� ȹ��";
        passEx.text = "���� �Ӵϸ� 200�� ȹ���մϴ�.";
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
