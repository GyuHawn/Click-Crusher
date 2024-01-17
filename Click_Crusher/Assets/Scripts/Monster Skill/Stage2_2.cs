using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2_2 : MonoBehaviour
{
    public GameObject[] bulletPrefabs;
    public float bulletSpeed;

    void Start()
    {
        InvokeRepeating("Attack", 1f, 3f);
    }

    void Attack()
    {
        StartCoroutine(MonsterAttack());
    }

    IEnumerator MonsterAttack()
    {
        for (int i = 0; i < 6; i++)
        {
            GameObject bullet = Instantiate(bulletPrefabs[i % bulletPrefabs.Length], gameObject.transform.position, Quaternion.identity);

            float randomAngle = Random.Range(0f, 360f);
            Vector3 direction = new Vector3(Mathf.Cos(randomAngle * Mathf.Deg2Rad), Mathf.Sin(randomAngle * Mathf.Deg2Rad), 1f);

            bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;

            Destroy(bullet, 5f);

            yield return new WaitForSeconds(0.2f);
        }
    }
}
