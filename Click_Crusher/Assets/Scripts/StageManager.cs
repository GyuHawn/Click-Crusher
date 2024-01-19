using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Security.Cryptography;

public class StageManager : MonoBehaviour
{
    private MonsterSpwan monsterSpawn;
    private SelectItem selectItem;
    private SelectPass selectPass;
    private StageTimeLimit stageTimeLimit;
    private PlayerController playerController;
    private CharacterSkill characterSkill;
    private ItemSkill itemSkill;

    public bool gameStart = false; // 게임시작 여부

    public GameObject[] map;
    public int mainStage; // 메인스테이지 (1, 2, 3...)
    public int subStage; // 서브스테이지 (1-1, 1-2...)
    public TMP_Text stageText;

    public int base0Monster; // 스테이지 몬스터 [0]의 수
    public int base1Monster; // 스테이지 몬스터 [1]의 수
    public int base2Monster; // 스테이지 몬스터 [2]의 수
    public int base3Monster; // 스테이지 몬스터 [3]의 수
    public int bossMonster; // 보스몬스터 수

   // public int monsterCount = 0; // 소환된 몬스터 수

    public float timeLimit; // 스테이지당 제한시간

    public bool selectingItem;

    public float totalTime;
    public int rewardMoney;
    public TMP_Text totalTimeText;
    public TMP_Text rewardMoneyText;
    public TMP_Text finalWaveText;
    
    private void Awake()
    {
        monsterSpawn = GameObject.Find("Manager").GetComponent<MonsterSpwan>();
        selectItem = GameObject.Find("Manager").GetComponent<SelectItem>();
        selectPass = GameObject.Find("Manager").GetComponent<SelectPass>();
        stageTimeLimit = GameObject.Find("Manager").GetComponent<StageTimeLimit>();
        playerController = GameObject.Find("Manager").GetComponent<PlayerController>();
        characterSkill = GameObject.Find("Manager").GetComponent<CharacterSkill>();
        itemSkill = GameObject.Find("Manager").GetComponent<ItemSkill>();
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
            selectingItem = false;
            gameStart = true;

            totalTime = 0;
            rewardMoney = 0;
        }
    }

    void Update()
    {
        if (mainStage <= 8)
        {
            for (int i = 0; i < mainStage - 1; ++i)
            {
                map[i].SetActive(false);
            }

            map[mainStage - 1].SetActive(true);
        }
        else
        {
            for (int i = 0; i < 7; ++i)
            {
                map[i].SetActive(false);
            }

            map[7].SetActive(true);
        }


        if (mainStage <= 7)
        {
            stageText.text = "Stage " + mainStage + "-" + subStage;
        }
        else if(mainStage >= 8)
        {
            stageText.text = "Stage " + mainStage;
        }

        if(stageTimeLimit.stageFail >= stageTimeLimit.stageTime)
        {
            playerController.gameover.SetActive(true);
            gameStart = false;
        }

        totalTime = playerController.gameTime;
        totalTimeText.text = string.Format("{0:00}:{1:00}", Mathf.Floor(totalTime / 60), totalTime % 60);
        finalWaveText.text = mainStage + " - " + subStage;
        rewardMoneyText.text = rewardMoney.ToString();
    }

    public void Reward()
    {
        if (gameStart)
        {
            rewardMoney += (int)(totalTime * 1);

            int playerMoney = PlayerPrefs.GetInt("GameMoney", 0);

            playerMoney += rewardMoney;

            PlayerPrefs.SetInt("GameMoney", playerMoney);
            PlayerPrefs.Save();
        }       
    }

    public void StageMonsterSetting()
    {
        if (mainStage <= 7)
        {
            // 1~7 스테이지 설정
            switch (subStage)
            {
                case 1:
                   // base0Monster = 2 + mainStage;
                    base0Monster =1;
                    break;
                case 2:
                    base0Monster = 1 + mainStage;
                    base1Monster = 1 + mainStage;
                    break;
                case 3:
                    base0Monster = 1 + mainStage;
                    base1Monster = 1 + mainStage;
                    base2Monster = 1 + mainStage;
                    break;
                case 4:
                    if (mainStage == 6)
                    {
                        base0Monster = 1 + mainStage;
                        base1Monster = 1 + mainStage;
                        base2Monster = 1 + mainStage;
                        base3Monster = 1;
                    }
                    else
                    {
                        base0Monster = 1 + mainStage;
                        base1Monster = 1 + mainStage;
                        base2Monster = 1 + mainStage;
                        base3Monster = 1 + mainStage;
                    }
                    break;
                case 5:
                    if (mainStage == 6)
                    {
                        base0Monster = 1 + mainStage;
                        base1Monster = 1 + mainStage;
                        base2Monster = 1 + mainStage;
                        base3Monster = 2;
                        bossMonster = 1;
                    }
                    else
                    {
                        base0Monster = 1 + mainStage;
                        base1Monster = 1 + mainStage;
                        base2Monster = 1 + mainStage;
                        base3Monster = 1 + mainStage;
                        bossMonster = 1;
                    }        
                    break;
            }
        }
        else
        {
            // 8 스테이지 이후부터 InfiniteMonsters 사용
            base0Monster = 1 + mainStage;
            base1Monster = 1 + mainStage;
            base2Monster = 1 + mainStage;
            base3Monster = 1 + mainStage;
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

        stageTimeLimit.stageFail = 0f;
    }

    void SpawnMonsters()
    {
        //monsterCount = base0Monster + base1Monster + base2Monster + base3Monster + bossMonster; // 몬스터 수 설정
        monsterSpawn.MonsterInstantiate(base0Monster, base1Monster, base2Monster, base3Monster, bossMonster);
    }

    void SelectPass()
    {
        selectPass.passMenu.SetActive(true);
        playerController.isAttacking = true;
        Time.timeScale = 0f;
    }

    public void NextStage()
    {
        //if (monsterCount > 0) return; // 몬스터가 남아있다면 실행되지 않음

        characterSkill.CharacterCoolTime();

         if (mainStage < 8)
         {
             NextSubStage();

             if (mainStage >= 2 && mainStage < 8)
              {
                  if (subStage == 2)
                  {
                      selectingItem = true;
                      SelectPass();
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
                 NextMainStage();

                  selectItem.ItemSelect();
                  StartCoroutine(DelayStage());
              }
          }
          else
          {
              if (mainStage % 10 == 2 || mainStage % 10 == 5 || mainStage % 10 == 8)
              {
                  selectingItem = true;
                  SelectPass();
              }
              else if (mainStage % 10 == 0 || mainStage % 10 == 6)
              {
                  selectItem.ItemSelect();
                  StartCoroutine(DelayStage());
              }

              NextMainStage();
         }

        ResetStageState();
        NextStageSetting(); // 스테이지 이동시 몬스터수 초기화
        StageMonsterSetting();
        SpawnMonsters();
    }

    void ResetStageState()
    {
        GameObject[] skills = GameObject.FindObjectsOfType<GameObject>();

        foreach (GameObject skill in skills)
        {
            if (skill.name == "BossSkill" || skill.name == "PlayerSkill" || skill.name == "MonsterAttack" || skill.name == "MonsterDefense")
            {
                Destroy(skill);
            }
        }

        itemSkill.holyWave = false;
        playerController.stage5Debuff = false;
    }

    void NextSubStage()
    {
        subStage++;
        rewardMoney += 10;
    }

    void NextMainStage()
    {
        mainStage++;
        rewardMoney += 100;
    }

    IEnumerator DelayStage()
    {
        yield return new WaitForSeconds(1f);
    }

    public void GameOver()
    {
        StartCoroutine(GameOverButton());
    }

    IEnumerator GameOverButton()
    {
        yield return new WaitForSeconds(0.5f);

        LodingController.LoadScene("MainMenu");
    }
}
