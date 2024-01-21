using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage5_0 : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpd;

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
            float randomY = transform.position.y + Random.Range(-1f, 1f);
            Vector3 bulletPos = new Vector3(transform.position.x - 1.5f, randomY, + 1f);
            Vector2 direction = Vector2.right;

            GameObject bullet = Instantiate(bulletPrefab, bulletPos, Quaternion.identity);
            bullet.name = "MonsterAttack";

            bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpd;

            Destroy(bullet, 3f);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
