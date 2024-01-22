using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage5_1 : MonoBehaviour
{
    public GameObject bulletPrefab;

    private GameObject previousMonster;

    public void Attack()
    {
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");

        if (monsters.Length > 0)
        {
            List<GameObject> Monsters = new List<GameObject>(monsters);
            if (previousMonster != null)
            {
                Monsters.Remove(previousMonster);
            }

            if (Monsters.Count > 0)
            {
                GameObject randomMonster = Monsters[Random.Range(0, Monsters.Count)];

                StartCoroutine(DefenseMonster(randomMonster));
            }
        }
    }

    IEnumerator DefenseMonster(GameObject monster)
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.name = "MonsterDefense";

        while (bullet != null && monster != null)
        {
            bullet.transform.position = new Vector3(monster.transform.position.x, monster.transform.position.y, -6);

            MonsterController monsterController = monster.GetComponent<MonsterController>();
            if (monsterController != null)
            {
                monsterController.defense = true;
            }

            yield return null;
        }

        Destroy(bullet);

        if (monster != null)
        {
            MonsterController monsterController = monster.GetComponent<MonsterController>();
            if (monsterController != null)
            {
                yield return new WaitForSeconds(0.5f);
                monsterController.defense = false;
            }
        }
    }
}
