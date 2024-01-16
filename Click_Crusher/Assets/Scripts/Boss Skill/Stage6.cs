using System.Collections;
using UnityEngine;

public class Stage6 : MonoBehaviour
{
    public GameObject bossEffect;
    public GameObject skillPos;

    void Start()
    {
        skillPos = GameObject.Find("BossSkillPos");
        StartCoroutine(SpawnBossSkill());
    }

    IEnumerator SpawnBossSkill()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            Vector3 randomPosition = new Vector3(
                Random.Range(skillPos.transform.position.x - 4f, skillPos.transform.position.x + 4f),
                Random.Range(skillPos.transform.position.y - 1f, skillPos.transform.position.y + 2.5f),
                skillPos.transform.position.z - 8
            );

            GameObject skill = Instantiate(bossEffect, randomPosition, Quaternion.identity);
            skill.name = "BossSkill";
            yield return new WaitForSeconds(3f);

            Destroy(skill);
        }
    }
}
