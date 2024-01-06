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
    public AudioSource bgmMainMenu; // ���θ޴�
    public AudioSource bgmCharacterMenu; // ĳ���͸޴�
    public AudioSource bgmStage; // ��������
    public AudioSource bgmBossStage; // ������������
    public AudioSource bgmSelectMenu; // �����ۼ��ø޴�
    public AudioSource bgmResultMenu; // ���

    // function
    public AudioSource attackAudio; // ����
    public AudioSource defenseAudio; // ���
    public AudioSource hitAudio; // �ǰ�
    public AudioSource monsterAttackAudio; // ���� ����
    public AudioSource buttonAudio; // ��ư
    public AudioSource startAudio; // ����

    // Item
    public AudioSource fireAudio; // ���̾�
    public AudioSource fireShotAudio; // ���̾
    public AudioSource holyShotAudio; // Ȧ����
    public AudioSource holyWaveAudio; // Ȧ�����̺�
    public AudioSource meleeAudio; // ����
    public AudioSource posionAudio; // ��
    public AudioSource rockAudio; // ��
    public AudioSource sturnAudio; // ����
    
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


    // ���۽� �Ҹ� �ߺ� ���ſ�
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