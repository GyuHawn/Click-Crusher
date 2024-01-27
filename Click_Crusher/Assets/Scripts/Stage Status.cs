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

    // ����
    public GameObject damageUp; // �⺻������ ����
    public GameObject monsterHealthDown; // ���� ü�� ����
    public GameObject timeUp; // ���ѽð� ����
    public GameObject percentUp; // Ȯ�� ����
    public GameObject monsterDie; // �����ð� ���� ���� ����
    private float timer = 0f;
    private float delay = 10f;
    private List<GameObject> buffList = new List<GameObject>(); // ���� ����Ʈ

    // �����
    public GameObject damageDown; // �⺻������ ����
    public GameObject monsterHealthUP; // ���� ü�� ����
    public GameObject timeDown; // ���ѽð� ����
    public GameObject percentDown; // Ȯ������
    public GameObject monsterAttackSpdUp; // ����ü �ӵ� ����
    public GameObject monsterDamageUp; // ���� ������ ����
    public GameObject monsterSizeUp;// ���� ũ�� ����
    private List<GameObject> deBuffList = new List<GameObject>(); // ����� ����Ʈ

    private GameObject selectedEffect; // ���õ� ����

    private void Awake()
    {
        stageTimeLimit = GameObject.Find("Manager").GetComponent<StageTimeLimit>();
        playerController = GameObject.Find("Manager").GetComponent<PlayerController>();
        itemSkill = GameObject.Find("Manager").GetComponent<ItemSkill>();
        stageManager = GameObject.Find("Manager").GetComponent<StageManager>();
    }

    void Start()
    {
        // ����Ʈ�� �߰�
        // ����
        buffList.Add(damageUp);
        buffList.Add(monsterHealthDown);
        buffList.Add(timeUp);
        buffList.Add(percentUp);
        buffList.Add(monsterDie);

        // �����
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

    // ��ų �ѹ��� �� ���ϵ���

    // ����
    // �⺻����������
    public void DamageUP()
    {
        Debug.Log("�⺻����������");
        playerController.damage += (int)(playerController.damage * 0.5f);
    }

    // ���� ü�� ����
    public void MonsterHealthDown()
    {
        Debug.Log("���� ü�� ����");
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");

        foreach(GameObject monster in monsters)
        {
            MonsterController monsterController = monster.GetComponent<MonsterController>();
            monsterController.currentHealth = (monsterController.currentHealth * 0.7f);
        }
    }

    // ���ѽð� ����
    public void TimeUp()
    {
        Debug.Log("���ѽð� ����");
        stageTimeLimit.stageTime += 10;
    }

    // Ȯ�� ����
    public void PercentUp()
    {
        Debug.Log("Ȯ�� ����");
        itemSkill.firePercent += 5f;
        itemSkill.fireShotPercent += 5f;
        itemSkill.holyShotPercent += 5f;
        itemSkill.holyWavePercent += 5f;
        itemSkill.rockPercent += 5f;
        itemSkill.posionPercent += 5f;
        itemSkill.meleePercent += 5f;
        itemSkill.sturnPercent += 5f;
    }

    // �����ð� ���� ���� ����
    public void MonsterDie()
    {
        Debug.Log("�����ð� ���� ���� ����");
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

    // �����
    // �⺻������ ����
    public void DamageDown()
    {
        Debug.Log("�⺻������ ����");
        playerController.damage -= (int)(playerController.damage * 0.5f);
    }

    // ���� ü�� ����
    public void MonsterHealthUP()
    {
        Debug.Log("���� ü�� ����");
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");

        foreach (GameObject monster in monsters)
        {
            MonsterController monsterController = monster.GetComponent<MonsterController>();
            monsterController.currentHealth = (monsterController.currentHealth * 1.5f);
        }
    }

    // ���ѽð� ����
    public void TimeDown()
    {
        Debug.Log("���ѽð� ����");
        stageTimeLimit.stageTime -= 10;
    }

    // Ȯ������
    public void PercentDown()
    {
        Debug.Log("Ȯ������");
        itemSkill.firePercent -= 5f;
        itemSkill.fireShotPercent -= 5f;
        itemSkill.holyShotPercent -= 5f;
        itemSkill.holyWavePercent -= 5f;
        itemSkill.rockPercent -= 5f;
        itemSkill.posionPercent -= 5f;
        itemSkill.meleePercent -= 5f;
        itemSkill.sturnPercent -= 5f;
    }

    // ����ü �ӵ� ����
    public void MonsterAttackSpdUp()
    {
        Debug.Log("����ü �ӵ� ����");
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

    // ���� ������ ����
    public void MonsterDamageUp()
    {
        Debug.Log("���� ������ ����");
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

    // ���� ũ�� ����
    public void MonsterSizeUp()
    {
        Debug.Log("���� ũ�� ����");
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
