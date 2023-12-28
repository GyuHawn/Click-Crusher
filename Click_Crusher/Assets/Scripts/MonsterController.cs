using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    private StageManager stageManager;
    private PlayerController playerController;
    private ItemSkill itemSkill;

    public int damage;
    public float maxHealth;
    public float currentHealth;

    // 피격 간격
    private bool canTakeDamage = true;

    public bool attack;
    public float attackTime;
    private bool boosAttack;

    public GameObject danager;
    public GameObject dieEffect;
    public GameObject sturn;

    // 플레이어 기술 관련
    public bool fired; // 불
    public bool stop; // 기절중인지
    public bool poisoned; // 중독

    SpriteRenderer spriteRenderer;
    private Animator anim;

    void Start()
    {
        stageManager = GameObject.Find("Manager").GetComponent<StageManager>();
        playerController = GameObject.Find("Manager").GetComponent<PlayerController>();
        itemSkill = GameObject.Find("Manager").GetComponent<ItemSkill>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        currentHealth = maxHealth;

        danager.SetActive(false);
        stop = false;
        attack = false;
        boosAttack = false;
        if(gameObject.tag == "Monster")
        {
            attackTime = Random.Range(3.0f, 6.0f);
        }
        else if(gameObject.tag == "Boss")
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

        // PC 마우스 클릭
        if (Input.GetMouseButtonDown(0) && !itemSkill.fireShotReady)
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject && canTakeDamage)
            {
                if (attack)
                {
                    playerController.playerHealth -= damage;
                }
                else
                {
                    if (itemSkill.meleeReady)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            currentHealth -= playerController.damage;
                            HitMonster(0.2f, 0.1f);
                        }
                        HitMonster(0.5f, 0.2f);
                    }
                    else if (itemSkill.rockReady)
                    {
                        currentHealth -= (playerController.damage + itemSkill.rockDamage);
                        itemSkill.rockReady = false;
                        HitMonster(0.5f, 0.2f);
                    }
                    else if (itemSkill.fireShotReady)
                    {
                        currentHealth -= (playerController.damage + itemSkill.fireShotDamage);
                        itemSkill.fireShotReady = false;
                        HitMonster(0.5f, 0.2f);
                    }
                    else
                    {
                        currentHealth -= playerController.damage;
                        HitMonster(0.5f, 0.2f);
                    }

                }
            }
        }

        // 모바일 터치
        if (Input.touchCount > 0 && !itemSkill.fireShotReady)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(touch.position);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject && canTakeDamage)
            {
                if (attack)
                {
                    playerController.playerHealth -= damage;
                }
                else
                {
                    if (itemSkill.meleeReady)
                    {
                        for (int i = 0; i < itemSkill.meleeNum; i++)
                        {
                            currentHealth -= playerController.damage;
                            HitMonster(0.3f, 0.2f);
                        }
                        HitMonster(0.5f, 0.2f);
                    }
                    else
                    {
                        currentHealth -= playerController.damage;
                        HitMonster(0.5f, 0.2f);
                    }
                }
            }
        }

        if (gameObject.tag == "Boss" && attack && !playerController.defending && !boosAttack)
        {
            playerController.playerHealth -= damage; // 보스가 한번만 공격하도록
            boosAttack = true;
        }

        if (attackTime <= 0)
        {
            StartCoroutine(MonsterAttack());
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
                if (itemSkill.posionTime >= 0)
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

    IEnumerator DeleteFire()
    {
        yield return new WaitForSeconds(3f);

        fired = false;
    }

    IEnumerator MonsterAttack()
    {
        danager.SetActive(true);
        yield return new WaitForSeconds(1.0f);

        danager.SetActive(false);

        attack = true;
        anim.SetBool("Attack", true);
        boosAttack = false;

        yield return new WaitForSeconds(1.0f);
        anim.SetBool("Attack", false);
        attack = false;

        if (gameObject.tag == "Monster")
        {
            attackTime = Random.Range(3.0f, 5.0f);
        }
        else if (gameObject.tag == "Boss")
        {
            attackTime = Random.Range(8.0f, 10.0f);
        }
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

        stageManager.monsterCount--;
        stageManager.NextStage();

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
            currentHealth -= itemSkill.fireShoSubDamage;
            HitMonster(0.5f, 0.2f);
        }
    }
}
