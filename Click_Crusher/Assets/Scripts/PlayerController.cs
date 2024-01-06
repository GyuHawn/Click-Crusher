using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Burst.CompilerServices;

public class PlayerController : MonoBehaviour
{
    private Character character;
    private StageManager stageManager;
    private ItemSkill itemSkill;
    private AudioManager audioManager;

    public GameObject[] playerHealthUI;
    public int playerHealth;
    public GameObject gameover;

    public float damage;
    private bool isAttacking = false;

    public GameObject defenseEffect;
    public bool defending;
    public float defenseTime;
    public GameObject defenseCoolTime;
    public TMP_Text defenseCoolTimeText;

    public int money;

    public float gameTime;
    public TMP_Text gameTimeText;

    public bool isDragging = false; // �巡�� ������
    void Start()
    {
        character = GameObject.Find("Manager").GetComponent<Character>();
        stageManager = GameObject.Find("Manager").GetComponent<StageManager>();
        itemSkill = GameObject.Find("Manager").GetComponent<ItemSkill>();
        audioManager = GameObject.Find("Manager").GetComponent<AudioManager>();

        defending = false;

        playerHealth = 8;
        UpdateHealthUI();

        if (character.rock)
        {
            damage += character.rockDamage;
        }

        gameTime = 0f;
    }

    void Update()
    {
        UpdateHealthUI();

        if (defenseTime >= 0)
        {
            defenseCoolTimeText.text = ((int)defenseTime).ToString();
            defenseTime -= Time.deltaTime;
        }
        else
        {
            defenseCoolTime.SetActive(false);
        }

        gameTime += Time.deltaTime;
        gameTimeText.text = string.Format("{0:00}:{1:00}", Mathf.Floor(gameTime / 60), gameTime % 60);
        
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging && !isAttacking) // ���ο� ���� �߰�
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            if (hit.collider != null)
            {
                MonsterController monsterController = hit.collider.GetComponent<MonsterController>();

                if (monsterController != null)
                {                  
                    StartCoroutine(AttackMonster(monsterController));
                }
            }
        }
    }

    IEnumerator AttackMonster(MonsterController monsterController)
    {
        isAttacking = true; // ���� ������ ����
        
        if (monsterController.attack)
        {
            if (monsterController.canTakeDamage)
            {
                audioManager.HitAudio();

                playerHealth -= monsterController.damage;
                monsterController.DamegeCoolDown(1f);
            }
        }
        else
        {
            audioManager.AttackAudio();

            monsterController.currentHealth -= damage;
            itemSkill.SetCurrentAttackedMonster(monsterController.gameObject);
            monsterController.HitMonster(0.5f, 0.2f);
        }

        if (itemSkill.isFire && Random.Range(0f, 100f) <= itemSkill.firePercent)
        {
            Debug.Log("���̾�");
            itemSkill.Fire(monsterController.gameObject.transform.position);
        }
        if (itemSkill.isFireShot && Random.Range(0f, 100f) <= itemSkill.fireShotPercent)
        {
            Debug.Log("���̾");
            itemSkill.FireShot(monsterController.gameObject.transform.position);
        }
        if (itemSkill.isHolyWave && Random.Range(0f, 100f) <= itemSkill.holyWavePercent)
        {
            Debug.Log("Ȧ�����̺�");
            itemSkill.HolyWave();
        }
        if (itemSkill.isHolyShot && Random.Range(0f, 100f) <= itemSkill.holyShotPercent)
        {
            Debug.Log("Ȧ����");
            itemSkill.HolyShot(monsterController.gameObject.transform.position);
        }
        if (itemSkill.isMelee && Random.Range(0f, 100f) <= itemSkill.meleePercent)
        {
            Debug.Log("����");
            itemSkill.Melee(monsterController.gameObject.transform.position, itemSkill.meleeNum);
        }
        if (itemSkill.isPosion && Random.Range(0f, 100f) <= itemSkill.posionPercent)
        {
            Debug.Log("��");
            itemSkill.Posion(monsterController.gameObject.transform.position);
        }
        if (itemSkill.isRock && Random.Range(0f, 100f) <= itemSkill.rockPercent)
        {
            Debug.Log("��");
            itemSkill.Rock(monsterController.gameObject.transform.position);
        }
        if (itemSkill.isSturn && Random.Range(0f, 100f) <= itemSkill.sturnPercent)
        {
            Debug.Log("����");
            itemSkill.Sturn();
        }

        // ���콺�� ���ų� 0.2�ʵ� ���ݰ���
        float startTime = Time.time;
        yield return new WaitUntil(() => !Input.GetMouseButton(0) || Time.time - startTime > 0.2f);

        isAttacking = false; // ������ ����
    }

    void UpdateHealthUI()
    {
        for (int i = 0; i < playerHealthUI.Length; i++)
        {
            playerHealthUI[i].SetActive(false);
        }

        if (playerHealth >= 0)
        {
            playerHealthUI[playerHealth].SetActive(true);
        }

        if (playerHealth <= 0)
        {
            playerHealthUI[0].SetActive(true);
            Time.timeScale = 0f;
            stageManager.Reward();
            stageManager.gameStart = false;
            gameover.SetActive(true);
        }
    }

    public void Defense()
    {
        if (defenseTime <= 0)
        {
            StartCoroutine(StartDefense());
        }
    }

    IEnumerator StartDefense()
    {
        defending = true;

        audioManager.DefenseAudio();

        defenseEffect.SetActive(true);
        defenseCoolTime.SetActive(true);
        defenseTime = 6;

        yield return new WaitForSeconds(3f);

        defending = false;
        defenseEffect.SetActive(false);
    }
}
