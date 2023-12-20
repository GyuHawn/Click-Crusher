using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StageManager : MonoBehaviour
{
    private MonsterSpwan monsterSpawn;
    private SelectItem selectItem;

    private bool gameStart = false; // 게임시작 여부

    public int mainStage; // 메인스테이지 (1, 2, 3..)
    public int subStage; // 서브스테이지 (1-1, 1-2...)
    public TMP_Text stageText;

    public int baseMonster; // 일반몬스터 수
    public int strongMonster; // 강화몬스터 수
    public int bossMonster; // 보스몬스터 수

    public int monsterCount = 0; // 소환된 몬스터 수

    private void Awake()
    {
        monsterSpawn = GameObject.Find("Manager").GetComponent<MonsterSpwan>();
        selectItem = GameObject.Find("Manager").GetComponent<SelectItem>();
    }

    void Start()
    {
        // 1-1 시작 설정 후 게임 시작
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
        if (subStage == 1)
        {
            //baseMonster = 5;
            baseMonster = 1;
        }
        else if (subStage == 2)
        {
            //baseMonster = 4;
            //strongMonster = 2;
            baseMonster = 1;
            strongMonster = 1;
        }
        else if (subStage == 3)
        {
            //baseMonster = 3;
            //strongMonster = 3;
            baseMonster = 1;
            strongMonster = 1;
        }
        else if (subStage == 4)
        {
            //baseMonster = 3;
            //strongMonster = 4;
            baseMonster = 1;
            strongMonster = 1;
        }
        else if (subStage == 5)
        {
            //baseMonster = 4;
            //strongMonster = 3;
            baseMonster = 1;
            strongMonster = 1;
            bossMonster = 1;
        }
    }

    void NextStageSetting()
    {
        baseMonster = 1;
        strongMonster = 0;
        bossMonster = 0;
    }
  
    void SpawnMonsters()
    {
        monsterCount = baseMonster + strongMonster + bossMonster; // 몬스터 수 설정
        monsterSpawn.MonsterInstantiate(baseMonster, strongMonster, bossMonster);
    }

    public void NextStage()
    {
        if (monsterCount > 0) return; // 몬스터가 남아있다면 실행되지 않음

        subStage++;
        if (subStage > 5)
        {
            subStage = 1;
            mainStage++;

            selectItem.ItemSelect();
            StartCoroutine(DelayStage());
        }

        NextStageSetting(); // 스테이지 이동시 몬스터수 초기화
        StageMonsterSetting();
        SpawnMonsters();
    }

    IEnumerator DelayStage()
    {
        yield return new WaitForSeconds(3f);
    }
}