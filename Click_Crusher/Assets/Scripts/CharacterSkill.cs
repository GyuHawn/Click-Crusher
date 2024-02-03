using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterSkill : MonoBehaviour
{
    private ItemSkill itemSkill;
    private AudioManager audioManager;
    private PlayerController playerController;
    private MonsterController monsterController;

    public float rockDamage;
    public int rockTime;
    public GameObject rockCoolTime;
    public TMP_Text rockCoolTimeText;

    public GameObject waterEffect;
    public float waterDamage;
    public int waterTime;
    public GameObject waterCoolTime;
    public TMP_Text waterCoolTimeText;

    public float sturnDuration;
    public int sturnTime;
    public GameObject sturnCoolTime;
    public TMP_Text sturnCoolTimeText;

    private void Awake()
    {
        itemSkill = GameObject.Find("Manager").GetComponent<ItemSkill>();
        audioManager = GameObject.Find("Manager").GetComponent<AudioManager>();
        playerController = GameObject.Find("Manager").GetComponent<PlayerController>();
        monsterController = GameObject.Find("Manager").GetComponent<MonsterController>();
    }

    void Start()
    {
        BasicSettings();
    }

    void Update()
    {
        rockDamage = playerController.damage * 2;
        waterDamage = playerController.damage * 0.3f;
        sturnTime = 5;

        if (rockTime > 0)
        {
            rockCoolTime.SetActive(true);
            rockCoolTimeText.text = rockTime.ToString();
        }
        else
        {
           rockCoolTime.SetActive(false);
        }
        if (waterTime > 0)
        {
            waterCoolTime.SetActive(true);
            waterCoolTimeText.text = waterTime.ToString();
        }
        else
        {
            waterCoolTime.SetActive(false);
        }
        if (sturnTime > 0)
        {
            sturnCoolTime.SetActive(true);
            sturnCoolTimeText.text = sturnTime.ToString();
        }
        else
        {
            sturnCoolTime.SetActive(false);
        }
    }

    public void BasicSettings()
    {
        rockDamage = playerController.damage * 2f;
        waterDamage = playerController.damage * 0.3f;
        sturnDuration = 3f;
    }

    public void Rock()
    {
        if(rockTime <= 0)
        {
            audioManager.RockAudio();
            RockAttack();
        }
    }

    void RockAttack()
    {
        rockTime = 2;

        GameObject[] mosters = GameObject.FindGameObjectsWithTag("Monster");
        GameObject[] bossMonsters = GameObject.FindGameObjectsWithTag("Boss");

        List<GameObject> allMonsters = new List<GameObject>(mosters);
        allMonsters.AddRange(bossMonsters);

        foreach (GameObject monster in allMonsters)
        {
            MonsterController monsterController = monster.GetComponent<MonsterController>();

            if (monsterController != null)
            {
                GameObject rockInstance = Instantiate(itemSkill.rockEffect, monsterController.gameObject.transform.position, Quaternion.identity);

                if (monsterController.pRockTakeDamage)
                {
                    playerController.CRockDamageText(monsterController);
                    monsterController.currentHealth -= rockDamage;
                    monsterController.PlayerRockDamegeCoolDown(0.5f, 0.2f);
                }

                ItemSkill(monsterController);

                Destroy(rockInstance, 2f);
            }
        }
    }

    
    public void Sturn()
    {
        if (sturnTime <= 0)
        {
            audioManager.SturnAudio();
            SturnAttack();
        }      
    }
    
    void SturnAttack()
    {
        sturnTime = 2;

        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");

        foreach (GameObject monster in monsters)
        {
            MonsterController monsterController = monster.GetComponent<MonsterController>();
            GameObject sturnInstance = Instantiate(itemSkill.sturnEffect, monster.transform.position, Quaternion.identity);
            GameObject sturnimageInstance = Instantiate(itemSkill.sturnImage, monsterController.sturn.transform.position, Quaternion.identity);
            if (monsterController != null)
            {
                monsterController.stop = true;
                monsterController.attackTime += 5;
            }
            monsterToSturnImage[monster] = sturnimageInstance;
            Destroy(sturnimageInstance, 3f);
        }
        StartCoroutine(Removestun());
    }

    private Dictionary<GameObject, GameObject> monsterToSturnImage = new Dictionary<GameObject, GameObject>();
    IEnumerator Removestun()
    {
        yield return new WaitForSeconds(sturnDuration);
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
        foreach (GameObject monster in monsters)
        {
            MonsterController monsterController = monster.GetComponent<MonsterController>();
            if (monsterController != null)
            {
                monsterController.stop = false;
            }
        }
    }
    public void DestroyMonster(GameObject monster)
    {
        if (monsterToSturnImage.ContainsKey(monster))
        {
            Destroy(monsterToSturnImage[monster]);
            monsterToSturnImage.Remove(monster);
        }
        Destroy(monster);
    }

    public void Water()
    {
        if (waterTime <= 0)
        {
            StartCoroutine(WaterAttack());
        }       
    }

    IEnumerator WaterAttack()
    {
        waterTime = 2;

        List<GameObject> MonsterList = new List<GameObject>();

        for (int i = 0; i < 20; i++)
        {
            List<GameObject> selectedMonsters = new List<GameObject>();
            if (MonsterList.Count == 0)
            {
                GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
                GameObject[] bossMonsters = GameObject.FindGameObjectsWithTag("Boss");

                List<GameObject> allMonsters = new List<GameObject>(monsters);
                allMonsters.AddRange(bossMonsters);

                if (allMonsters.Count > 0)
                {
                    selectedMonsters.Add(allMonsters[Random.Range(0, allMonsters.Count)]);
                }

                MonsterList.AddRange(selectedMonsters);
            }
            else
            {
                if (MonsterList.Count > 0)
                {
                    selectedMonsters.Add(MonsterList[Random.Range(0, MonsterList.Count)]);
                }
            }

            foreach (GameObject monster in selectedMonsters)
            {
                MonsterController monsterController = monster.GetComponent<MonsterController>();

                if (monsterController != null)
                {
                    Vector3 waterPosition = new Vector3(monsterController.gameObject.transform.position.x, monsterController.gameObject.transform.position.y - 0.2f, monsterController.gameObject.transform.position.z);
                    GameObject waterInstance = Instantiate(waterEffect, waterPosition, Quaternion.Euler(90, 0, 0));
                    audioManager.WaterAudio();

                    if (monsterController.pWaterTakeDamage)
                    {
                        playerController.CWaterDamageText(monsterController);
                        monsterController.currentHealth -= waterDamage;
                        monsterController.PlayerWaterDamegeCoolDown(0.5f, 0.1f);
                    }

                    ItemSkill(monsterController);

                    Destroy(waterInstance, 2f);
                }
            }

            if (MonsterList.Count > 0)
            {
                MonsterList.Clear();
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    void ItemSkill(MonsterController monsterController)
    {
        if (itemSkill.isFire && Random.Range(0f, 100f) <= itemSkill.firePercent)
        {
            itemSkill.Fire(monsterController.gameObject.transform.position);
        }
        if (itemSkill.isFireShot && Random.Range(0f, 100f) <= itemSkill.fireShotPercent)
        {
            itemSkill.FireShot(monsterController.gameObject.transform.position);
        }
        if (itemSkill.isHolyWave && Random.Range(0f, 100f) <= itemSkill.holyWavePercent)
        {
            itemSkill.HolyWave();
        }
        if (itemSkill.isHolyShot && Random.Range(0f, 100f) <= itemSkill.holyShotPercent)
        {
            itemSkill.HolyShot(monsterController.gameObject.transform.position);
        }
        if (itemSkill.isPosion && Random.Range(0f, 100f) <= itemSkill.posionPercent)
        {
            itemSkill.Posion(monsterController.gameObject.transform.position);
        }
        if (itemSkill.isRock && Random.Range(0f, 100f) <= itemSkill.rockPercent)
        {
            itemSkill.Rock(monsterController.gameObject.transform.position);
        }
        if (itemSkill.isSturn && Random.Range(0f, 100f) <= itemSkill.sturnPercent)
        {
            itemSkill.Sturn();
        }
    }

    public void CharacterCoolTime()
    {
        if (rockTime > 0)
        {
            rockTime--;
        }
        else if (waterTime > 0)
        {
            waterTime--;
        }
        else if (sturnTime > 0)
        {
            sturnTime--;
        }
    }
}
