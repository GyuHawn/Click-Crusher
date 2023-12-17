using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    private StageManager stagerManager;
    private PlayerController playerController;

    public float damage;
    public float maxHealth;
    public float currentHealth;

    public bool attack;
    public float attackTime;
    private float originalAttackTime;

    private Animator anim;

    void Start()
    {
        stagerManager = GameObject.Find("Manager").GetComponent<StageManager>();
        playerController = GameObject.Find("Manager").GetComponent<PlayerController>();

        anim = GetComponent<Animator>();

        currentHealth = maxHealth;

        attack = false;
        originalAttackTime = attackTime;
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            Die();
        }

        // PC 마우스 클릭
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                if (attack)
                {
                    playerController.currentHealth -= damage;
                }
                else
                {
                    currentHealth -= playerController.damage;
                }
            }
        }

        // 모바일 터치
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(touch.position);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                if(attack)
                {
                    playerController.currentHealth -= damage;
                }
                else
                {
                    currentHealth -= playerController.damage;
                }
            }
        }

        if (attackTime <= 0)
        {
            attack = true;
            anim.SetBool("Attack", true);
            StartCoroutine(ResetAttackFlag());
        }
        else
        {
            attackTime -= Time.deltaTime;
        }
    }

    IEnumerator ResetAttackFlag()
    {
        yield return new WaitForSeconds(1.0f);
        anim.SetBool("Attack", false);
        attack = false;
        attackTime = originalAttackTime;
    }

    public void Die()
    {
        stagerManager.monsterCount--;
        stagerManager.NextStage();

        Destroy(gameObject);
    }
}
