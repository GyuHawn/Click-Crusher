using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2_3 : MonoBehaviour
{
    public GameObject bulletPrefab;

    void Start()
    {
        InvokeRepeating("Attack", 1f, 5f);
    }

    void Attack()
    {
        StartCoroutine(MonsterAttack());
    }

    IEnumerator MonsterAttack()
    {
        for (int i = 0; i < 3; i++)
        {
            Vector3 randomPosition = new Vector3(Random.Range(transform.position.x - 3f, transform.position.x + 3f), Random.Range(transform.position.y - 1f, transform.position.y + 1f), 5f);

            GameObject bullet = Instantiate(bulletPrefab, randomPosition, Quaternion.identity);

            Destroy(bullet, 3f);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(2f);
    }
}
