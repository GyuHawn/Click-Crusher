using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Burst.CompilerServices;

public class PlayerController : MonoBehaviour
{
    private StageManager stageManager;
    private ItemSkill itemSkill;
    private AudioManager audioManager;
    private CharaterSkill charaterSkill;

    public GameObject[] playerHealthUI;
    public int playerHealth;
    public GameObject gameover;

    public int damage;
    public bool isAttacking = false;
    public GameObject hubDamageText;

    public GameObject defenseEffect;
    public bool defending;
    public float defenseTime;
    public GameObject defenseCoolTime;
    public TMP_Text defenseCoolTimeText;

    public int money;

    public float gameTime;
    public TMP_Text gameTimeText;

    public bool isDragging = false; // 드래그 중인지
    void Start()
    {
        stageManager = GameObject.Find("Manager").GetComponent<StageManager>();
        itemSkill = GameObject.Find("Manager").GetComponent<ItemSkill>();
        audioManager = GameObject.Find("Manager").GetComponent<AudioManager>();
        charaterSkill = GameObject.Find("Manager").GetComponent<CharaterSkill>();

        defending = false;

        playerHealth = 8;
        UpdateHealthUI();

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

        if (isDragging && !isAttacking)
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
        isAttacking = true;
        
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

            PlayerDamageText(monsterController);

            itemSkill.SetCurrentAttackedMonster(monsterController.gameObject);
            monsterController.HitMonster(0.5f, 0.2f);
        }

        if (itemSkill.isFire && Random.Range(0f, 100f) <= itemSkill.firePercent)
        {
            Debug.Log("파이어");
            itemSkill.Fire(monsterController.gameObject.transform.position);
        }
        if (itemSkill.isFireShot && Random.Range(0f, 100f) <= itemSkill.fireShotPercent)
        {
            Debug.Log("파이어샷");
            itemSkill.FireShot(monsterController.gameObject.transform.position);
        }
        if (itemSkill.isHolyWave && Random.Range(0f, 100f) <= itemSkill.holyWavePercent)
        {
            Debug.Log("홀리웨이브");
            itemSkill.HolyWave();
        }
        if (itemSkill.isHolyShot && Random.Range(0f, 100f) <= itemSkill.holyShotPercent)
        {
            Debug.Log("홀리샷");
            itemSkill.HolyShot(monsterController.gameObject.transform.position);
        }
        if (itemSkill.isMelee && Random.Range(0f, 100f) <= itemSkill.meleePercent)
        {
            Debug.Log("난투");
            itemSkill.Melee(monsterController.gameObject.transform.position, itemSkill.meleeNum);

            StartCoroutine(MeleeAttack());            
        }
        IEnumerator MeleeAttack()
        {
            for (int i = 0; i < itemSkill.meleeNum; i++)
            {
                monsterController.currentHealth -= damage;

                PlayerDamageText(monsterController);

                yield return new WaitForSeconds(0.15f);
            }
        }

        if (itemSkill.isPosion && Random.Range(0f, 100f) <= itemSkill.posionPercent)
        {
            Debug.Log("독");
            itemSkill.Posion(monsterController.gameObject.transform.position);
        }
        if (itemSkill.isRock && Random.Range(0f, 100f) <= itemSkill.rockPercent)
        {
            Debug.Log("돌");
            itemSkill.Rock(monsterController.gameObject.transform.position);
        }
        if (itemSkill.isSturn && Random.Range(0f, 100f) <= itemSkill.sturnPercent)
        {
            Debug.Log("스턴");
            itemSkill.Sturn();
        }

        // 마우스를 때거나 0.2초뒤 공격가능
        float startTime = Time.time;
        yield return new WaitUntil(() => !Input.GetMouseButton(0) || Time.time - startTime > 0.2f);

        isAttacking = false; // 공격이 끝남
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

    // 플레이어 공격 데미지 텍스트
    public void PlayerDamageText(MonsterController monsterController)
    {
        GameObject damegeText = Instantiate(hubDamageText, monsterController.transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
        damegeText.GetComponent<DamageText>().damege = damage;
    }


    public void FireDamageText(MonsterController monsterController)
    {
        GameObject damegeText = Instantiate(hubDamageText, monsterController.transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
        damegeText.GetComponent<DamageText>().damege = (int)itemSkill.fireDamage;
    }

    public void FireShotDamageText(MonsterController monsterController)
    {
        GameObject damegeText = Instantiate(hubDamageText, monsterController.transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
        damegeText.GetComponent<DamageText>().damege = (int)itemSkill.fireShotDamage;
    }
    public void FireShotSubDamageText(MonsterController monsterController)
    {
        GameObject damegeText = Instantiate(hubDamageText, monsterController.transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
        damegeText.GetComponent<DamageText>().damege = (int)itemSkill.fireShotSubDamage;
    }

    public void HolyShotDamageText(MonsterController monsterController)
    {
        GameObject damegeText = Instantiate(hubDamageText, monsterController.transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
        damegeText.GetComponent<DamageText>().damege = (int)itemSkill.holyShotDamage;
    }

    public void HolyWaveDamageText(MonsterController monsterController)
    {
        GameObject damegeText = Instantiate(hubDamageText, monsterController.transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
        damegeText.GetComponent<DamageText>().damege = (int)itemSkill.holyWaveDamage;
    }
    public void RockDamageText(MonsterController monsterController)
    {
        GameObject damegeText = Instantiate(hubDamageText, monsterController.transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
        damegeText.GetComponent<DamageText>().damege = (int)itemSkill.rockDamage;
    }
    public void PoisonDamageText(MonsterController monsterController)
    {
        GameObject damegeText = Instantiate(hubDamageText, monsterController.transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
        damegeText.GetComponent<DamageText>().damege = (int)itemSkill.poisonDamage;
    }

    public void CWaterDamageText(MonsterController monsterController)
    {
        GameObject damegeText = Instantiate(hubDamageText, monsterController.transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
        damegeText.GetComponent<DamageText>().damege = (int)charaterSkill.waterDamage;
    }
    public void CRockDamageText(MonsterController monsterController)
    {
        GameObject damegeText = Instantiate(hubDamageText, monsterController.transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
        damegeText.GetComponent<DamageText>().damege = (int)charaterSkill.rockDamage;
    }
}
