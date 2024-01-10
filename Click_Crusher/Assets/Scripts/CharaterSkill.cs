using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharaterSkill : MonoBehaviour
{
    private ItemSkill itemSkill;
    private AudioManager audioManager;
    private PlayerController playerController;

    public float rockDamage;

    public GameObject waterEffect;
    public float waterDamage;

    public float sturnDuration;

    void Start()
    {
        itemSkill = GameObject.Find("Manager").GetComponent<ItemSkill>();
        audioManager = GameObject.Find("Manager").GetComponent<AudioManager>();
        playerController = GameObject.Find("Manager").GetComponent<PlayerController>();

        BasicSettings();
    }

    public void BasicSettings()
    {
        rockDamage = playerController.damage * 2f;
        waterDamage = playerController.damage * 0.3f;
        sturnDuration = 3f;
    }

        public void Rock()
    {
        audioManager.RockAudio();
        RockAttack();
    }

    void RockAttack()
    {
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

                playerController.CRockDamageText(monsterController);
                monsterController.currentHealth -= rockDamage;

                Destroy(rockInstance, 2f);
            }
        }
    }

    
    public void Sturn()
    {
        audioManager.SturnAudio();
        SturnAttack();
    }
    
    void SturnAttack()
    {
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
        StartCoroutine(WaterAttack());
    }

    IEnumerator WaterAttack()
    {
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

                    playerController.CWaterDamageText(monsterController);
                    monsterController.currentHealth -= waterDamage;

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

}
