using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Character character;
    private StageManager stageManager;
    private ItemSkill itemSkill;

    public GameObject[] playerHealthUI;
    public int playerHealth;
    public GameObject gameover;

    public float damage;

    public GameObject defenseEffect;
    public bool defending;
    public float defenseTime;
    public GameObject defenseCoolTime;
    public TMP_Text defenseCoolTimeText;

    public int money;

    private float gameTime;
    public TMP_Text gameTimeText;

    public bool isDragging = false; // 드래그 중인지
    void Start()
    {
        character = GameObject.Find("Manager").GetComponent<Character>();
        stageManager = GameObject.Find("Manager").GetComponent<StageManager>();
        itemSkill = GameObject.Find("Manager").GetComponent<ItemSkill>();

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

        // 게임 시간 증가
        gameTime += Time.deltaTime;

        // 시간을 텍스트로 표시
        gameTimeText.text = string.Format("{0:00}:{1:00}", Mathf.Floor(gameTime / 60), gameTime % 60);


        
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            if (hit.collider != null)
            {
                MonsterController monsterController = hit.collider.GetComponent<MonsterController>();

                if (monsterController != null)
                {
                    if (monsterController.attack)
                    {
                        if (monsterController.canTakeDamage)
                        {
                            playerHealth -= monsterController.damage;
                            monsterController.DamegeCoolDown(1f);
                        }
                    }
                    else
                    {
                        monsterController.currentHealth -= damage;
                        itemSkill.SetCurrentAttackedMonster(hit.collider.gameObject);
                        monsterController.HitMonster(0.5f, 0.2f);
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
            }
        }

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
        defenseEffect.SetActive(true);
        defenseCoolTime.SetActive(true);
        defenseTime = 6;

        yield return new WaitForSeconds(3f);

        defending = false;
        defenseEffect.SetActive(false);
    }
}
