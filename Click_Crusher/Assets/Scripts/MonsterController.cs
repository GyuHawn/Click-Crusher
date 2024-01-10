using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    private MonsterSpwan monsterSpawn;
    private PlayerController playerController;
    private ItemSkill itemSkill;
    private AudioManager audioManager;

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
    private bool isMonsterAttacking = false;
    private bool isBossAttacking = false;

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
        monsterSpawn = GameObject.Find("Manager").GetComponent<MonsterSpwan>();
        playerController = GameObject.Find("Manager").GetComponent<PlayerController>();
        itemSkill = GameObject.Find("Manager").GetComponent<ItemSkill>();
        audioManager = GameObject.Find("Manager").GetComponent<AudioManager>();

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

        if (attackTime <= 0)
        {
            attack = true;
            if (gameObject.tag == "Monster")
            {
                audioManager.MonsterAttackAudio();               
                StartCoroutine(MonsterAttackReady());
            }
            else if(gameObject.tag == "Boss" && bossAttackNum == 1)
            {
                StartCoroutine(BossAttackReady());                           
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
                playerController.HolyWaveDamageText(this);

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
                    playerController.PoisonDamageText(this);

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
                playerController.FireDamageText(this);

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

    IEnumerator BossAttackReady()
    {
        if (!isBossAttacking)
        {
            isBossAttacking = true;
            danager.SetActive(true);
            yield return new WaitForSeconds(1.0f);
            danager.SetActive(false);

            yield return StartCoroutine(BossAttack());

            isBossAttacking = false;
        }
    }

    IEnumerator BossAttack()
    {
        danager.SetActive(true);
        yield return new WaitForSeconds(0.5f);

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
        danager.SetActive(false);

        attackTime = Random.Range(8.0f, 10.0f);
        StartCoroutine(ReadyBossAttack());
    }

    IEnumerator ReadyBossAttack()
    {
        yield return new WaitForSeconds(attackTime);
        bossAttackNum = 1;
    }

    IEnumerator MonsterAttackReady()
    {
        if (!isMonsterAttacking)
        {
            isMonsterAttacking = true;
            danager.SetActive(true);
            yield return new WaitForSeconds(1.0f);
            danager.SetActive(false);

            yield return StartCoroutine(MonsterAttack());

            isMonsterAttacking = false;
        }
    }

    IEnumerator MonsterAttack()
    {
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
                playerController.HolyShotDamageText(this);

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
            if (canTakeDamage)
            {
                playerController.FireShotSubDamageText(this);

                currentHealth -= itemSkill.fireShotSubDamage;
                HitMonster(0.5f, 0.2f);
            }
        }
    }
}
