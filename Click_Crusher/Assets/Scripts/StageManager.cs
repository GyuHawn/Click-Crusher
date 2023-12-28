using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StageManager : MonoBehaviour
{
    private MonsterSpwan monsterSpawn;
    private SelectItem selectItem;
    private SelectPass selectPass;

    private bool gameStart = false; // ���ӽ��� ����

    public int mainStage; // ���ν������� (1, 2, 3...)
    public int subStage; // ���꽺������ (1-1, 1-2...)
    public TMP_Text stageText;

    public int base0Monster; // �������� ���� [0]�� ��
    public int base1Monster; // �������� ���� [1]�� ��
    public int base2Monster; // �������� ���� [2]�� ��
    public int base3Monster; // �������� ���� [3]�� ��
    public int bossMonster; // �������� ��

    public int monsterCount = 0; // ��ȯ�� ���� ��

    public float timeLimit; // ���������� ���ѽð�

    private void Awake()
    {
        monsterSpawn = GameObject.Find("Manager").GetComponent<MonsterSpwan>();
        selectItem = GameObject.Find("Manager").GetComponent<SelectItem>();
        selectPass = GameObject.Find("Manager").GetComponent<SelectPass>();
    }

    void Start()
    {
        // 1-1 ���� ���� �� ���� ����
        if (!gameStart)
        {
            mainStage = 1;
            subStage = 1;
            StageMonsterSetting();
            SpawnMonsters();
            gameStart = true;
        }
    }

    void Update()
    {
        stageText.text = "Stage " + mainStage + "-" + subStage;
    }

    public void StageMonsterSetting()
    {
        if (mainStage <= 7)
        {
            // 1~7 �������� ����
            switch (subStage)
            {
                case 1:
                    base0Monster = 1;
                    break;
                case 2:
                    base0Monster = 1;
                    base1Monster = 1;
                    break;
                case 3:
                    base0Monster = 1;
                    base1Monster = 1;
                    base2Monster = 1;
                    break;
                case 4:
                    base0Monster = 1;
                    base1Monster = 1;
                    base2Monster = 1;
                    base3Monster = 1;
                    break;
                case 5:
                    base0Monster = 1;
                    base1Monster = 1;
                    base2Monster = 1;
                    base3Monster = 1;
                    bossMonster = 1;
                    break;
            }
        }
        else
        {
            // 8 �������� ���ĺ��� InfiniteMonsters ���
            base0Monster = 1;
            base1Monster = 1;
            base2Monster = 1;
            base3Monster = 1;
            bossMonster = 1;
        }
    }

    void NextStageSetting()
    {
        base0Monster = 1;
        base1Monster = 0;
        base2Monster = 0;
        base3Monster = 0;
        bossMonster = 0;
    }

    void SpawnMonsters()
    {
        monsterCount = base0Monster + base1Monster + base2Monster + base3Monster + bossMonster; // ���� �� ����
        monsterSpawn.MonsterInstantiate(base0Monster, base1Monster, base2Monster, base3Monster, bossMonster);
    }

    public void NextStage()
    {
        if (monsterCount > 0) return; // ���Ͱ� �����ִٸ� ������� ����

        if (mainStage >= 8)
        {
            if (mainStage % 10 == 2 || mainStage % 10 == 5 || mainStage % 10 == 8)
            {
                selectPass.passMenu.SetActive(true);
            }
            else if(mainStage % 10 == 0 || mainStage % 10 == 6)
            {
                selectItem.ItemSelect();
                StartCoroutine(DelayStage());
            }

            mainStage++;
        }
        else
        {
            subStage++;

            if (mainStage >= 2 && mainStage < 8)
            {
                if (subStage == 2)
                {
                    selectPass.passMenu.SetActive(true);
                }
            }

            if (subStage == 3)
            {
                selectItem.ItemSelect();
                StartCoroutine(DelayStage());
            }
            else if (subStage > 5)
            {
                subStage = 1;
                mainStage++;

                selectItem.ItemSelect();
                StartCoroutine(DelayStage());
            }
        }

        NextStageSetting(); // �������� �̵��� ���ͼ� �ʱ�ȭ
        StageMonsterSetting();
        SpawnMonsters();
    }

    IEnumerator DelayStage()
    {
        yield return new WaitForSeconds(1f);
    }
}
