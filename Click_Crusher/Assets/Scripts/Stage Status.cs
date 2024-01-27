using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class StageStatus : MonoBehaviour
{
    private StageTimeLimit stageTimeLimit;
    private PlayerController playerController;
    private ItemSkill itemSkill;
    private StageManager stageManager;

    public int buff;
    public int status;
    public Image stageStatus;
    public GameObject statusPos;

    // 버프
    public GameObject damageUp; // 기본데미지 증가
    public GameObject monsterHealthDown; // 몬스터 체력 감소
    public GameObject timeUp; // 제한시간 증가
    public GameObject percentUp; // 확률 증가
    public GameObject monsterDie; // 일정시간 마다 몬스터 제거
    private float timer = 0f;
    private float delay = 10f;
    private List<GameObject> buffList = new List<GameObject>(); // 버프 리스트

    // 디버프
    public GameObject damageDown; // 기본데미지 감소
    public GameObject monsterHealthUP; // 몬스터 체력 증가
    public GameObject timeDown; // 제한시간 감소
    public GameObject percentDown; // 확률감소
    public GameObject monsterAttackSpdUp; // 투사체 속도 증가
    public GameObject monsterDamageUp; // 몬스터 데미지 증가
    public GameObject monsterSizeUp;// 몬스터 크기 감소
    private List<GameObject> deBuffList = new List<GameObject>(); // 디버프 리스트

    private GameObject selectedEffect; // 선택된 버프

    private void Awake()
    {
        stageTimeLimit = GameObject.Find("Manager").GetComponent<StageTimeLimit>();
        playerController = GameObject.Find("Manager").GetComponent<PlayerController>();
        itemSkill = GameObject.Find("Manager").GetComponent<ItemSkill>();
        stageManager = GameObject.Find("Manager").GetComponent<StageManager>();
    }

    void Start()
    {
        // 리스트에 추가
        // 버프
        buffList.Add(damageUp);
        buffList.Add(monsterHealthDown);
        buffList.Add(timeUp);
        buffList.Add(percentUp);
        buffList.Add(monsterDie);

        // 디버프
        deBuffList.Add(damageDown);
        deBuffList.Add(monsterHealthUP);
        deBuffList.Add(timeDown);
        deBuffList.Add(percentDown);
        deBuffList.Add(monsterAttackSpdUp);
        deBuffList.Add(monsterDamageUp);
        deBuffList.Add(monsterSizeUp);
    }
    
    void Update()
    {
        Debug.Log("buff " + buff);
        Debug.Log("status " + status);

        if(buff == 1)
        {
            if(status == 1)
            {
                DamageUP();
            }
            else if (status == 2)
            {
                MonsterHealthDown();
            }
            else if (status == 3)
            {
                TimeUp();
            }
            else if (status == 4)
            {
                PercentUp();
            }
            else if (status == 5)
            {
                timer += Time.deltaTime;

                if (timer >= delay)
                {
                    MonsterDie();
                    timer = 0f;
                }
            }
        }
        else if(buff == 2)
        {
            if (status == 1)
            {
                DamageDown();
            }
            else if (status == 2)
            {
                MonsterHealthUP();
            }
            else if (status == 3)
            {
                TimeDown();
            }
            else if (status == 4)
            {
                PercentDown();
            }
            else if (status == 5)
            {
                MonsterAttackSpdUp();
            }
            else if (status == 6)
            {
                MonsterDamageUp();
            }
            else if (status == 7)
            {
                MonsterSizeUp();
            }
        }
    }

    // 스킬 한번만 값 변하도록

    // 버프
    // 기본데미지증가
    public void DamageUP()
    {
        Debug.Log("기본데미지증가");
        playerController.damage += (int)(playerController.damage * 0.5f);
    }

    // 몬스터 체력 감소
    public void MonsterHealthDown()
    {
        Debug.Log("몬스터 체력 감소");
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");

        foreach(GameObject monster in monsters)
        {
            MonsterController monsterController = monster.GetComponent<MonsterController>();
            monsterController.currentHealth = (monsterController.currentHealth * 0.7f);
        }
    }

    // 제한시간 증가
    public void TimeUp()
    {
        Debug.Log("제한시간 증가");
        stageTimeLimit.stageTime += 10;
    }

    // 확률 증가
    public void PercentUp()
    {
        Debug.Log("확률 증가");
        itemSkill.firePercent += 5f;
        itemSkill.fireShotPercent += 5f;
        itemSkill.holyShotPercent += 5f;
        itemSkill.holyWavePercent += 5f;
        itemSkill.rockPercent += 5f;
        itemSkill.posionPercent += 5f;
        itemSkill.meleePercent += 5f;
        itemSkill.sturnPercent += 5f;
    }

    // 일정시간 마다 몬스터 제거
    public void MonsterDie()
    {
        Debug.Log("일정시간 마다 몬스터 제거");
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");

        foreach (GameObject monster in monsters)
        {
            MonsterController monsterController = monster.GetComponent<MonsterController>();

            if (monsterController != null && monsterController.currentHealth > 0)
            {
                monsterController.currentHealth = 0;
                break;
            }
        }
    }

    // 디버프
    // 기본데미지 감소
    public void DamageDown()
    {
        Debug.Log("기본데미지 감소");
        playerController.damage -= (int)(playerController.damage * 0.5f);
    }

    // 몬스터 체력 증가
    public void MonsterHealthUP()
    {
        Debug.Log("몬스터 체력 증가");
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");

        foreach (GameObject monster in monsters)
        {
            MonsterController monsterController = monster.GetComponent<MonsterController>();
            monsterController.currentHealth = (monsterController.currentHealth * 1.5f);
        }
    }

    // 제한시간 감소
    public void TimeDown()
    {
        Debug.Log("제한시간 감소");
        stageTimeLimit.stageTime -= 10;
    }

    // 확률감소
    public void PercentDown()
    {
        Debug.Log("확률감소");
        itemSkill.firePercent -= 5f;
        itemSkill.fireShotPercent -= 5f;
        itemSkill.holyShotPercent -= 5f;
        itemSkill.holyWavePercent -= 5f;
        itemSkill.rockPercent -= 5f;
        itemSkill.posionPercent -= 5f;
        itemSkill.meleePercent -= 5f;
        itemSkill.sturnPercent -= 5f;
    }

    // 투사체 속도 증가
    public void MonsterAttackSpdUp()
    {
        Debug.Log("투사체 속도 증가");
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");

        foreach (GameObject monster in monsters)
        {
            MonoBehaviour[] scripts = monster.GetComponents<MonoBehaviour>();

            foreach (MonoBehaviour script in scripts)
            {
                if (script.GetType().Name.Contains("Stage"))
                {
                    System.Reflection.FieldInfo field = script.GetType().GetField("bulletSpd");

                    if (field != null)
                    {
                        field.SetValue(script, (float)field.GetValue(script) + 1);
                    }
                }
            }
        }
    }

    // 몬스터 데미지 증가
    public void MonsterDamageUp()
    {
        Debug.Log("몬스터 데미지 증가");
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");

        foreach (GameObject monster in monsters)
        {
            MonsterController monsterController = monster.GetComponent<MonsterController>();
            if(stageManager.mainStage < 8)
            {
                monsterController.damage = 2;
            }
            else if(stageManager.mainStage >= 8)
            {
                monsterController.damage = 3;
            }
        }
    }

    // 몬스터 크기 감소
    public void MonsterSizeUp()
    {
        Debug.Log("몬스터 크기 감소");
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");

        foreach (GameObject monster in monsters)
        {
            Transform monsterTransform = monster.transform;
            Vector3 newScale = monsterTransform.localScale + new Vector3(0.1f, 0.1f, 0f);
            monsterTransform.localScale = newScale;
        }
    }

    public void BuffStatus()
    {
        List<GameObject> selectedList = (Random.Range(0, 2) == 0) ? buffList : deBuffList;

        if(selectedList == buffList)
        {
            buff = 1;
            Buff();
        }
        else if(selectedList == deBuffList)
        {
            buff = 2;
            DeBuff();
        }

        if (selectedList.Count > 0)
        {
            int randomIndex = Random.Range(0, selectedList.Count);

            status = randomIndex + 1;

            selectedEffect = selectedList[randomIndex];

            selectedEffect.transform.position = statusPos.transform.position;
        }
    }

    public void ResetStatus()
    {
        if (selectedEffect != null)
        {
            selectedEffect.transform.position = new Vector3(100, 1500, 0);

            buff = 0;
            status = 0;

            selectedEffect = null;
        }
    }

    void Buff()
    {
        stageStatus.color = new Color(0f, 0.49f, 1f);
    }

    void DeBuff()
    {
        stageStatus.color = Color.red;
    }
}
