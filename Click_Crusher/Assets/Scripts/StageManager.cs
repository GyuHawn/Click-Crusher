using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StageManager : MonoBehaviour
{
    private MonsterSpwan monsterSpawn;

    private bool gameStart = false; // ���ӽ��� ����

    public int mainStage; // ���ν������� (1, 2, 3..)
    public int subStage; // ���꽺������ (1-1, 1-2...)
    public TMP_Text stageText;

    public int baseMonster; // �Ϲݸ��� ��
    public int strongMonster; // ��ȭ���� ��
    public int bossMonster; // �������� ��

    public int monsterCount = 0; // ��ȯ�� ���� ��

    private void Awake()
    {
        monsterSpawn = GameObject.Find("Manager").GetComponent<MonsterSpwan>();
    }

    void Start()
    {
        // 1-1 ���� ���� �� ���� ����
        if (!gameStart)
        {
            mainStage = 1;
            subStage = 1;
            gameStart = true;
            StageMonsterSetting();
            SpawnMonsters();
        }
    }

    void Update()
    {
        stageText.text = "Stage " + mainStage + "-" + subStage;
    }

    public void StageMonsterSetting()
    {
        if (subStage == 1)
        {
            baseMonster = 5;
        }
        else if (subStage == 2)
        {
            baseMonster = 4;
            strongMonster = 2;
        }
        else if (subStage == 3)
        {
            baseMonster = 3;
            strongMonster = 3;
        }
        else if (subStage == 4)
        {
            baseMonster = 3;
            strongMonster = 4;
        }
        else if (subStage == 5)
        {
            baseMonster = 4; // ���� �������������� �⺻ ���� ����
            strongMonster = 3; // ���� �������������� ��ȭ ���� ����
            bossMonster = 1;
        }
    }

    void SpawnMonsters()
    {
        monsterCount = baseMonster + strongMonster + bossMonster; // ���� �� ����
        monsterSpawn.MonsterInstantiate(baseMonster, strongMonster, bossMonster);
    }

    public void NextStage()
    {
        if (monsterCount > 0) return; // ���Ͱ� �����ִٸ� ������� ����

        subStage++;
        if (subStage > 5)
        {
            subStage = 1;
            mainStage++;
        }

        StageMonsterSetting();
        SpawnMonsters();
    }
}
