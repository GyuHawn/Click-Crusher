using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    private StageManager stageManager;

    // BGM
    public AudioSource bgmMainMenu; // 메인메뉴
    public AudioSource bgmCharacterMenu; // 캐릭터메뉴
    public AudioSource bgmStage; // 스테이지
    public AudioSource bgmBossStage; // 보스스테이지
    public AudioSource bgmSelectMenu; // 아이템선택메뉴
    public AudioSource bgmResultMenu; // 결과

    // function
    public AudioSource attackAudio; // 공격
    public AudioSource defenseAudio; // 방어
    public AudioSource hitAudio; // 피격
    public AudioSource monsterAttackAudio; // 몬스터 공격
    public AudioSource buttonAudio; // 버튼
    public AudioSource startAudio; // 시작

    // Item
    public AudioSource fireAudio; // 파이어
    public AudioSource fireShotAudio; // 파이어샷
    public AudioSource holyShotAudio; // 홀리샷
    public AudioSource holyWaveAudio; // 홀리웨이브
    public AudioSource meleeAudio; // 난투
    public AudioSource posionAudio; // 독
    public AudioSource rockAudio; // 돌
    public AudioSource sturnAudio; // 스턴
    
   // public Slider audioSlider;

    private void Awake()
    {
        stageManager = GameObject.Find("Manager").GetComponent<StageManager>();
    }

    void Start()
    {
        StopAudio();

        float volume = PlayerPrefs.GetFloat("Volume", 1.0f);

        // audioSlider.value = volume;

        if (SceneManager.GetActiveScene().name == "MainMenu" || SceneManager.GetActiveScene().name == "Loding")
        {
            bgmMainMenu.volume = volume;
            bgmMainMenu.Play();
        }
        else if (SceneManager.GetActiveScene().name == "Character")
        {
            bgmCharacterMenu.volume = volume;
        }
        else if (SceneManager.GetActiveScene().name == "Game")
        {
            bgmStage.volume = volume;
            bgmBossStage.volume = volume;
            bgmSelectMenu.volume = volume;
            bgmResultMenu.volume = volume;

            if (stageManager.mainStage <= 8)
            {
                if (stageManager.subStage == 5)
                {
                    bgmStage.Stop();
                    bgmBossStage.Play();
                }
                else
                {
                    bgmBossStage.Stop();
                    bgmStage.Play();
                }
            }
            else
            {
                bgmStage.Stop();
                bgmBossStage.Play();
            }

        }
    }
            
    /*void Update()
    {
        if (bgmStage != null && bgmBossStage != null && bgmSelectMenu != null && bgmResultMenu != null)
        {
            bgmStage.volume = audioSlider.value;
            bgmBossStage.volume = audioSlider.value;
            bgmSelectMenu.volume = audioSlider.value;
            bgmResultMenu.volume = audioSlider.value;
        }
        else if (bgmMainMenu != null)
        {
            bgmMainMenu.volume = audioSlider.value;
        }
        else if(bgmCharacterMenu != null)
        {
            bgmCharacterMenu.volume = audioSlider.value;
        }
        PlayerPrefs.SetFloat("Volume", audioSlider.value);
    }*/  

    // function
    public void AttackAudio()
    {
        attackAudio.Play();
    }
    public void DefenseAudio()
    {
        defenseAudio.Play();
    }
    public void HitAudio()
    {
        hitAudio.Play();
    }
    public void MonsterAttackAudio()
    {
        monsterAttackAudio.Play();
    }
    public void ButtonAudio()
    {
        buttonAudio.Play();
    }
    public void StartAudio()
    {
        startAudio.Play();
    }

    // Item
    public void FireAudio()
    {
        fireAudio.Play();
    }
    public void FireShotAudio()
    {
        fireShotAudio.Play();
    }
    public void HolyShotAudio()
    {
        holyShotAudio.Play();
    }
    public void HolyWaveAudio()
    {
        holyWaveAudio.Play();
    }
    public void MeleeAudio()
    {
        meleeAudio.Play();
    }
    public void PosionAudio()
    {
        posionAudio.Play();
    }
    public void RockAudio()
    {
        rockAudio.Play();
    }
    public void SturnAudio()
    {
        sturnAudio.Play();
    }


    // 시작시 소리 중복 제거용
    void StopAudio()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu" || SceneManager.GetActiveScene().name == "Loding")
        {
            buttonAudio.Stop();
            startAudio.Stop();
        }
        else if (SceneManager.GetActiveScene().name == "Character")
        {
            buttonAudio.Stop();
            startAudio.Stop();
        }
        else if (SceneManager.GetActiveScene().name == "Game")
        {
            bgmBossStage.Stop();
            bgmSelectMenu.Stop();
            bgmResultMenu.Stop();
            
            attackAudio.Stop();
            defenseAudio.Stop();
            hitAudio.Stop();
            monsterAttackAudio.Stop();
            buttonAudio.Stop();
            
            fireAudio.Stop();
            fireShotAudio.Stop();
            holyShotAudio.Stop();
            holyWaveAudio.Stop();
            meleeAudio.Stop();
            posionAudio.Stop();
            rockAudio.Stop();
            sturnAudio.Stop();
        }
    }
}