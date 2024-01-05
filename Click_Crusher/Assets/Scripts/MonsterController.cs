using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    private Character character;
    private StageManager stageManager;
    private MonsterSpwan monsterSpawn;
    private PlayerController playerController;
    private ItemSkill itemSkill;

    public int damage;
    public float maxHealth;
    public float currentHealth;

    // 피격 간격
    public bool canTakeDamage = true;

    public bool attack;
    private int bossAttackNum;
    public float attackTime;

    public GameObject danager;
    public GameObject dieEffect;
    public GameObject sturn;

    // 플레이어 기술 관련
    public bool fired; // 불
    public bool stop; // 기절중인지
    public bool poisoned; // 중독   

    private int monsterLayer;
    private int bossLayer;

    SpriteRenderer spriteRenderer;
    private Animator anim;

    void Start()
    {
        character = GameObject.Find("Manager").GetComponent<Character>();
        stageManager = GameObject.Find("Manager").GetComponent<StageManager>();
        monsterSpawn = GameObject.Find("Manager").GetComponent<MonsterSpwan>();
        playerController = GameObject.Find("Manager").GetComponent<PlayerController>();
        itemSkill = GameObject.Find("Manager").GetComponent<ItemSkill>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        currentHealth = maxHealth;

        danager.SetActive(false);
        stop = false;
        attack = false;
        bossAttackNum = 1;

        monsterLayer = LayerMask.NameToLayer("Monster");
        bossLayer = LayerMask.NameToLayer("Boss");


        if (gameObject.tag == "Monster")
        {
            attackTime = Random.Range(3.0f, 6.0f);
        }
        else if (gameObject.tag == "Boss")
        {
            attackTime = Random.Range(7.0f, 10.0f);
        }
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            Die();
        }

        if (stop)
        {
            anim.enabled = false;
        }
        else
        {
            anim.enabled = true;
        }
/*
        if (Input.GetMouseButtonDown(0))
        {
            playerController.isDragging = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            playerController.isDragging = false;
        }*/

        /*if (playerController.isDragging)
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            if (hit.collider != null && (hit.collider.gameObject.layer == monsterLayer || hit.collider.gameObject.layer == bossLayer))
            {
                if (attack)
                {
                    if (canTakeDamage)
                    {
                        playerController.playerHealth -= damage;
                        StartCoroutine(DamageCooldown(1f));
                    }
                }
                else
                {
                    currentHealth -= playerController.damage;
                    itemSkill.SetCurrentAttackedMonster(hit.collider.gameObject);
                    HitMonster(0.5f, 0.2f);
                }

                if (itemSkill.isFire && Random.Range(0f, 100f) <= itemSkill.firePercent)
                {
                    Debug.Log("파이어");
                    itemSkill.Fire(hit.collider.gameObject.transform.position);
                }
                else if (itemSkill.isFireShot && Random.Range(0f, 100f) <= itemSkill.fireShotPercent)
                {
                    Debug.Log("파이어샷");
                    itemSkill.FireShot(hit.collider.gameObject.transform.position);
                }
                else if (itemSkill.isHolyWave && Random.Range(0f, 100f) <= itemSkill.holyWavePercent)
                {
                    Debug.Log("홀리웨이브");
                    itemSkill.HolyWave();
                }
                else if (itemSkill.isHolyShot && Random.Range(0f, 100f) <= itemSkill.holyShotPercent)
                {
                    Debug.Log("홀리샷");
                    itemSkill.HolyShot(hit.collider.gameObject.transform.position);
                }
                else if (itemSkill.isMelee && Random.Range(0f, 100f) <= itemSkill.meleePercent)
                {
                    Debug.Log("난투");
                    itemSkill.Melee(hit.collider.gameObject.transform.position, itemSkill.meleeNum);
                }
                else if (itemSkill.isPosion && Random.Range(0f, 100f) <= itemSkill.posionPercent)
                {
                    Debug.Log("독");
                    itemSkill.Posion(hit.collider.gameObject.transform.position);
                }
                else if (itemSkill.isRock && Random.Range(0f, 100f) <= itemSkill.rockPercent)
                {
                    Debug.Log("돌");
                    itemSkill.Rock(hit.collider.gameObject.transform.position);
                }
                else if (itemSkill.isSturn && Random.Range(0f, 100f) <= itemSkill.sturnPercent)
                {
                    Debug.Log("스턴");
                    itemSkill.Sturn();
                }
            }
        }*/

        if (attackTime <= 0)
        {
            attack = true;
            if (gameObject.tag == "Monster")
            {
                StartCoroutine(MonsterAttack());
            }
            else if(gameObject.tag == "Boss" && bossAttackNum == 1)
            {
                StartCoroutine(BossAttack());               
            }
        }
        else
        {
            attackTime -= Time.deltaTime;
        }

        // 플레이어 아이템 발동시 데미지
        if (itemSkill.holyWave && canTakeDamage)
        {
            if (canTakeDamage)
            {
                currentHealth -= itemSkill.holyWaveDamage;
                HitMonster(0.7f, 0.2f);
            }
        }

        if (poisoned)
        {
            if (canTakeDamage)
            {
                if (itemSkill.posionDuration >= 0)
                {
                    currentHealth -= itemSkill.poisonDamage;
                    HitMonster(0.5f, 0.2f);
                }
                else
                {
                    poisoned = false;
                }
            }
        }

        if (fired)
        {
            if (canTakeDamage)
            {
                currentHealth -= itemSkill.fireDamage;
                HitMonster(0.3f, 0.2f);
                StartCoroutine(DeleteFire());
            }
        }
    }

    IEnumerator Removestun()
    {
        yield return new WaitForSeconds(3f);

        stop = false;
    }

    IEnumerator DeleteFire()
    {
        yield return new WaitForSeconds(3f);

        fired = false;
    }

    IEnumerator BossAttack()
    {
        danager.SetActive(true);
        yield return new WaitForSeconds(1.0f);

        danager.SetActive(false);

        anim.SetBool("Attack", true);

        if (bossAttackNum != 0)
        {
            if (playerController.defending)
            {

            }
            else
            {
                bossAttackNum = 0;
                playerController.playerHealth -= damage;
            }         
        }

        yield return new WaitForSeconds(1f);
        anim.SetBool("Attack", false);

        attackTime = Random.Range(8.0f, 10.0f);
        StartCoroutine(ReadyBossAttack());
    }

    IEnumerator ReadyBossAttack()
    {
        yield return new WaitForSeconds(attackTime);
        bossAttackNum = 1;
    }


    IEnumerator MonsterAttack()
    {
        danager.SetActive(true);
        yield return new WaitForSeconds(1.0f);

        danager.SetActive(false);

        anim.SetBool("Attack", true);

        yield return new WaitForSeconds(1f);
        attack = false;
        anim.SetBool("Attack", false);

        attackTime = Random.Range(3.0f, 5.0f);
    }

    public void HitMonster(float damageCooldown, float colorChangeTime)
    {
        if (canTakeDamage)
        {
            if (fired)
            {
                spriteRenderer.color = new Color(1f, 0.5f, 0, 1);
                StartCoroutine(BackColor(colorChangeTime));
                StartCoroutine(DamageCooldown(damageCooldown));
            }
            else if (poisoned)
            {
                spriteRenderer.color = new Color(0.7f, 0, 0.7f, 1);
                StartCoroutine(BackColor(colorChangeTime));
                StartCoroutine(DamageCooldown(damageCooldown));
            }
            else
            {
                spriteRenderer.color = Color.red;
                StartCoroutine(BackColor(colorChangeTime));
                StartCoroutine(DamageCooldown(damageCooldown));
            }
        }
    }

    IEnumerator BackColor(float time)
    {
        yield return new WaitForSeconds(time);
        spriteRenderer.color = Color.white;
    }
    IEnumerator DamageCooldown(float damageCooldown)
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(damageCooldown);
        canTakeDamage = true;
    }

    public void DamegeCoolDown(float damageCooldown)
    {
        StartCoroutine(DamageCooldown(damageCooldown));
    }

    public void Die()
    {
        StartCoroutine(MonsterDie());
    }

    IEnumerator MonsterDie()
    {

        if (dieEffect != null)
        {
            SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
            renderer.enabled = false;

            dieEffect.SetActive(true);

            yield return new WaitForSeconds(1f);
        }

        //stageManager.monsterCount--;
        monsterSpawn.RemoveMonsterFromList(gameObject);

        if (stop)
        {
            itemSkill.DestroyMonster(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "HolyShot")
        {
            if (canTakeDamage)
            {
                currentHealth -= itemSkill.holyShotDamage;
                HitMonster(0.5f, 0.2f);
            }
        }
        else if (collision.gameObject.tag == "Fire")
        {
            fired = true;
        }
        else if (collision.gameObject.tag == "Poison")
        {
            poisoned = true;
        }
        else if (collision.gameObject.tag == "FireShotSub")
        {
            currentHealth -= itemSkill.fireShotSubDamage;
            HitMonster(0.5f, 0.2f);
        }
    }
}
