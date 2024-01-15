using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage7 : MonoBehaviour
{
    private StageManager stageManager;

    public GameObject bossSkill;
    public List<GameObject> skills;

    public GameObject pos;
    public Vector3 boxSize;

    void Start()
    {
        stageManager = GameObject.Find("Manager").GetComponent<StageManager>();

        pos = GameObject.Find("Stage7 SkillPos");

        InvokeRepeating("Stage7BossSkill", 5f, 10f);
    }

    void Stage7BossSkill()
    {
        StartCoroutine(Skill());
    }

    IEnumerator Skill()
    {
        for (int i = 0; i < 10; i++)
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(pos.transform.position.x - boxSize.x / 2, pos.transform.position.x + boxSize.x / 2),
                Random.Range(pos.transform.position.y - boxSize.y / 2, pos.transform.position.y + boxSize.y / 2),
                pos.transform.position.z
            );

            GameObject skill = Instantiate(bossSkill, randomPosition, Quaternion.identity);

            skills.Add(skill);
        }

        yield return new WaitForSeconds(5f);

        foreach (GameObject skill in skills)
        {
            Destroy(skill);
        }
        skills.Clear();
    }
}
