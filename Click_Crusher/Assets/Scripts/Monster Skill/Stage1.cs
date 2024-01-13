using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1 : MonoBehaviour
{
    public GameObject bossSkill;

    void Start()
    {
        InvokeRepeating("Stage1BossSkill", 5f, 10f);
    }

    void Stage1BossSkill()
    {
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");

        foreach (GameObject monster in monsters)
        {
            MonsterController monsterController = monster.GetComponent<MonsterController>();
            Vector3 skillPos = new Vector3(monster.transform.position.x, monster.transform.position.y, monster.transform.position.z -8);
            GameObject skill = Instantiate(bossSkill, monster.transform.position, Quaternion.identity);
            monsterController.boss1Defending = true;

            Destroy(skill, 2f);
            StartCoroutine(EndDefending(monsterController, 2f));
        }
    }

    IEnumerator EndDefending(MonsterController monsterController, float time)
    {
        yield return new WaitForSeconds(time);
        monsterController.boss1Defending = false;
    }
}
