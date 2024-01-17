using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3_2 : MonoBehaviour
{
    private MonsterSpwan monsterSpwan;

    public GameObject eggPrefab;
    public GameObject monsterPrefab;

    void Start()
    {
        monsterSpwan = GameObject.Find("Manager").GetComponent<MonsterSpwan>();

        InvokeRepeating("Spwan", 3f, 10f);
    }

    void Spwan()
    {
        StartCoroutine(SpwanMonster());
    }

    IEnumerator SpwanMonster()
    {
        Vector3 randomPos = new Vector3(Random.Range(-4f, 4f), Random.Range(-1f, 2f), 1f);
        GameObject egg = Instantiate(eggPrefab, randomPos, Quaternion.identity);

        yield return new WaitForSeconds(3f);

        Destroy(egg);

        Vector3 monsterPos = new Vector3(randomPos.x, randomPos.y, -2f);
        GameObject monster = Instantiate(monsterPrefab, randomPos, Quaternion.identity);
        monsterSpwan.spawnedMonsters.Add(monster);
    }
}
