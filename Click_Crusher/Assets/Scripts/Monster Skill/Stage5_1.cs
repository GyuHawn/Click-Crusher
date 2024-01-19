using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage5_1 : MonoBehaviour
{
    public GameObject bulletPrefab;

    // 변수 추가: 이전에 선택한 몬스터
    private GameObject previousMonster;

    void Start()
    {
        InvokeRepeating("MonsterAttack", 5f, 15f);
    }

    void MonsterAttack()
    {
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");

        if (monsters.Length > 0)
        {
            List<GameObject> Monsters = new List<GameObject>(monsters);
            if (previousMonster != null)
            {
                Monsters.Remove(previousMonster);
            }

            GameObject randomMonster = Monsters[Random.Range(0, Monsters.Count)];

            previousMonster = randomMonster;

            StartCoroutine(FollowMonster(randomMonster));
        }
    }

    IEnumerator FollowMonster(GameObject monster)
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.name = "MonsterDefense";

        while (monster != null)
        {
            bullet.transform.position = new Vector3(monster.transform.position.x, monster.transform.position.y, -6);
            yield return null;
        }

        Destroy(bullet);
    }
}
