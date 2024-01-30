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
    private CharacterSkill charaterSkill;

    public GameObject[] playerHealthUI;
    public GameObject healthEffect;
    public int playerHealth;
    public GameObject gameover;
    public bool die = false;

    public int damage;
    public bool isAttacking = false;
    public bool hitBullet = true;
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

    public GameObject attckEffect;
    public GameObject dragEffect;

    public bool stage5Debuff = false;
    public bool isStageHit = true;

    public Vector3 lastClickPosition; // 마지막 클릭위치

    private void Awake()
    {
        stageManager = GameObject.Find("Manager").GetComponent<StageManager>();
        itemSkill = GameObject.Find("Manager").GetComponent<ItemSkill>();
        audioManager = GameObject.Find("Manager").GetComponent<AudioManager>();
        charaterSkill = GameObject.Find("Manager").GetComponent<CharacterSkill>();
    }

    void Start()
    {
        defending = false;

        damage = 10;
        playerHealth = 8;
        UpdateHealthUI();

        gameTime = 0f;
        lastClickPosition = Vector3.zero;
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
            dragEffect.SetActive(true);
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            dragEffect.SetActive(false);
        }

        if (isDragging)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dragEffect.transform.position = new Vector3(mousePosition.x, mousePosition.y, dragEffect.transform.position.z);
        }

        if (isDragging && !isAttacking)
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            lastClickPosition = worldPoint;

            if (hit.collider != null)
            {
                MonsterController monsterController = hit.collider.GetComponent<MonsterController>();
                if (hit.collider.CompareTag("Stage6") || hit.collider.CompareTag("Stage4") || hit.collider.CompareTag("Stage7"))
                {
                    if (isStageHit)
                    {                    
                        StartCoroutine(Stage6Hit());
                    }
                }
                else if (hit.collider.CompareTag("HealthUp"))
                {
                    if(playerHealth >= 8)
                    {
                        Destroy(hit.collider.gameObject);
                    }
                    else
                    {
                        playerHealth++;
                        Destroy(hit.collider.gameObject);
                    }
                }
                else
                {
                    if (hit.collider.CompareTag("Bullet") && hitBullet)
                    {
                        audioManager.HitAudio();

                        playerHealth -= 1;
                        Vector3 effectPos = new Vector3(transform.position.x, transform.position.y, transform.position.z -6f);
                        GameObject effect = Instantiate(healthEffect, transform.position, Quaternion.identity);
                        StartCoroutine(BulletHitCooldown(0.2f));
                    }
                    else if (monsterController != null)
                    {
                        if (monsterController.boss1Defending)
                        {

                        }
                        else
                        {
                            StartCoroutine(AttackMonster(monsterController));
                        }
                    }
                }
            }
        }
    }

    IEnumerator BulletHitCooldown(float damageCooldown)
    {
        hitBullet = false;
        yield return new WaitForSeconds(damageCooldown);
        hitBullet = true;
    }

    public Vector3 GetLastClickPosition()
    {
        return lastClickPosition;
    }

    IEnumerator Stage6Hit()
    {
        isStageHit = false;      
        playerHealth -= 1;
        Vector3 effectPos = new Vector3(transform.position.x, transform.position.y, transform.position.z - 6f);
        GameObject effect = Instantiate(healthEffect, effectPos, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        isStageHit = true;
    }

    void AttackEffect(Vector3 targetPosition)
    {
        float xOffset = Random.Range(-0.5f, 0.5f);
        float yOffset = Random.Range(-0.2f, 0.2f);
        Vector3 spawnPosition = targetPosition + new Vector3(xOffset, yOffset, -5);

        GameObject monsterHit = Instantiate(attckEffect, spawnPosition, Quaternion.identity);

        StartCoroutine(RotateAndShrinkEffect(monsterHit.transform, 0.3f)); // 0.3초 후에 몬스터 히트 이펙트 제거
    }

    IEnumerator RotateAndShrinkEffect(Transform effectTransform, float destroyDelay)
    {
        float duration = 0.3f;
        float elapsedTime = 0f;

        Quaternion startRotation = effectTransform.rotation;
        Quaternion targetRotation = startRotation * Quaternion.Euler(0, 0, 180);

        Vector3 startScale = effectTransform.localScale;
        Vector3 targetScale = Vector3.zero;

        while (elapsedTime < duration)
        {
            effectTransform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime / duration);
            effectTransform.localScale = Vector3.Slerp(startScale, targetScale, elapsedTime / duration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        effectTransform.rotation = targetRotation;
        effectTransform.localScale = targetScale;

        yield return new WaitForSeconds(destroyDelay);
        Destroy(effectTransform.gameObject);
    }

    IEnumerator AttackMonster(MonsterController monsterController)
    {
        isAttacking = true;

        if (monsterController.defense)
        {
            isAttacking = false;
            yield break;
        }

        audioManager.AttackAudio();

        if (monsterController.playerTakeDamage)
        {
            AttackEffect(monsterController.gameObject.transform.position);
            monsterController.currentHealth -= damage;
            PlayerDamageText(monsterController);
        }

        itemSkill.SetCurrentAttackedMonster(monsterController.gameObject);

        if (stage5Debuff)
        {
            monsterController.PlayerDamegeCoolDown(3f, 0.2f);
        }
        else
        {
            monsterController.PlayerDamegeCoolDown(0.2f, 0.2f);
        }


        if (itemSkill.isFire && Random.Range(0f, 100f) <= itemSkill.firePercent)
        {
            itemSkill.Fire(monsterController.gameObject.transform.position);
        }
        if (itemSkill.isFireShot && Random.Range(0f, 100f) <= itemSkill.fireShotPercent)
        {
            itemSkill.FireShot(monsterController.gameObject.transform.position);
            if (monsterController.fireShotTakeDamage)
            {
                FireDamageText(monsterController);
                monsterController.currentHealth -= itemSkill.fireShotDamage;
                monsterController.FireShotDamegeCoolDown(0.5f, 0.2f);
            }
        }
        if (itemSkill.isHolyWave && Random.Range(0f, 100f) <= itemSkill.holyWavePercent)
        {
            itemSkill.HolyWave();
        }
        if (itemSkill.isHolyShot && Random.Range(0f, 100f) <= itemSkill.holyShotPercent)
        {
            itemSkill.HolyShot(monsterController.gameObject.transform.position);
        }
        if (itemSkill.isMelee && Random.Range(0f, 100f) <= itemSkill.meleePercent)
        {
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
            itemSkill.Posion(monsterController.gameObject.transform.position);
        }
        if (itemSkill.isRock && Random.Range(0f, 100f) <= itemSkill.rockPercent)
        {
            itemSkill.Rock(monsterController.gameObject.transform.position);
            if (monsterController.rockTakeDamage)
            {
                RockDamageText(monsterController);
                monsterController.currentHealth -= itemSkill.rockDamage;
                monsterController.RockDamegeCoolDown(0.5f, 0.2f);
            }
        }
        if (itemSkill.isSturn && Random.Range(0f, 100f) <= itemSkill.sturnPercent && monsterController.CompareTag("Monster"))
        {
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
            GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
            GameObject boss = GameObject.FindWithTag("Boss");
            foreach(GameObject monster in monsters)
            {
                Destroy(monster);
            }
            Destroy(boss);

            playerHealthUI[0].SetActive(true);
            Time.timeScale = 0f;
            stageManager.Reward();
            isDragging = false;
            dragEffect.SetActive(false);
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
        if(monsterController != null)
        {
            GameObject damegeText = Instantiate(hubDamageText, monsterController.transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
            damegeText.GetComponent<DamageText>().damege = damage;
        }
    }

    public void FireDamageText(MonsterController monsterController)
    {
        if (monsterController != null)
        {
        GameObject damegeText = Instantiate(hubDamageText, monsterController.transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
        damegeText.GetComponent<DamageText>().damege = (int)itemSkill.fireDamage;
        }
    }

    public void FireShotDamageText(MonsterController monsterController)
    {
        if (monsterController != null)
        {
        GameObject damegeText = Instantiate(hubDamageText, monsterController.transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
        damegeText.GetComponent<DamageText>().damege = (int)itemSkill.fireShotDamage;
        }
    }
    public void FireShotSubDamageText(MonsterController monsterController)
    {
        if (monsterController != null)
        {
        GameObject damegeText = Instantiate(hubDamageText, monsterController.transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
        damegeText.GetComponent<DamageText>().damege = (int)itemSkill.fireShotSubDamage;
        }
    }

    public void HolyShotDamageText(MonsterController monsterController)
    {
        if (monsterController != null)
        {
        GameObject damegeText = Instantiate(hubDamageText, monsterController.transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
        damegeText.GetComponent<DamageText>().damege = (int)itemSkill.holyShotDamage;
        }
    }

    public void HolyWaveDamageText(MonsterController monsterController)
    {
        if (monsterController != null)
        {
        GameObject damegeText = Instantiate(hubDamageText, monsterController.transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
        damegeText.GetComponent<DamageText>().damege = (int)itemSkill.holyWaveDamage;
        }
    }
    public void RockDamageText(MonsterController monsterController)
    {
        if (monsterController != null)
        {
        GameObject damegeText = Instantiate(hubDamageText, monsterController.transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
        damegeText.GetComponent<DamageText>().damege = (int)itemSkill.rockDamage;
        }
    }
    public void PoisonDamageText(MonsterController monsterController)
    {
        if (monsterController != null)
        {
        GameObject damegeText = Instantiate(hubDamageText, monsterController.transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
        damegeText.GetComponent<DamageText>().damege = (int)itemSkill.poisonDamage;
        }
    }

    public void CWaterDamageText(MonsterController monsterController)
    {
        if (monsterController != null)
        {
        GameObject damegeText = Instantiate(hubDamageText, monsterController.transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
        damegeText.GetComponent<DamageText>().damege = (int)charaterSkill.waterDamage;
        }
    }
    public void CRockDamageText(MonsterController monsterController)
    {
        if (monsterController != null)
        {
        GameObject damegeText = Instantiate(hubDamageText, monsterController.transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
        damegeText.GetComponent<DamageText>().damege = (int)charaterSkill.rockDamage;
        }
    }
}
