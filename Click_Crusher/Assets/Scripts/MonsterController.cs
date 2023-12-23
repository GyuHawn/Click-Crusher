using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    private StageManager stagerManager;
    private PlayerController playerController;
    private ItemSkill itemSkill;

    public int damage;
    public float maxHealth;
    public float currentHealth;

    public bool attack;
    public float attackTime;
    private float originalAttackTime;
    private bool boosAttack;

    public GameObject danager;
    public GameObject dieEffect;

    private Animator anim;

    void Start()
    {
        stagerManager = GameObject.Find("Manager").GetComponent<StageManager>();
        playerController = GameObject.Find("Manager").GetComponent<PlayerController>();
        itemSkill = GameObject.Find("Manager").GetComponent<ItemSkill>();

        anim = GetComponent<Animator>();

        currentHealth = maxHealth;

        danager.SetActive(false);
        attack = false;
        boosAttack = false;
        originalAttackTime = attackTime;
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            Die();
        }

        // PC 마우스 클릭
        if (Input.GetMouseButtonDown(0) && !itemSkill.fireShotReady)
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                if (attack)
                {
                    playerController.playerHealth -= damage;
                }
                else
                {
                    currentHealth -= playerController.damage;
                }
            }
        }

        // 모바일 터치
        if (Input.touchCount > 0 && !itemSkill.fireShotReady)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(touch.position);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                if(attack)
                {
                    playerController.playerHealth -= damage;
                }
                else
                {
                    currentHealth -= playerController.damage;
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
        attackTime = originalAttackTime;

    }

    public void Die()
    {
        StartCoroutine(MonsterDie());
    }

    IEnumerator MonsterDie()
    {
        if(dieEffect != null)
        {
            SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
            renderer.enabled = false;

            dieEffect.SetActive(true);

            yield return new WaitForSeconds(1f);
        }

        stagerManager.monsterCount--;
        stagerManager.NextStage();

        Destroy(gameObject);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Fire"))
        {
            Debug.Log("불에닿음");
        }
    }
}
