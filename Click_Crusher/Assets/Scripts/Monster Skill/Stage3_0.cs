using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3_0 : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed;

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
        float randomAngle = Random.Range(0f, 360f);

        for(int i = 0; i < 6; i++)
        {
            Vector3 direction = new Vector3(Mathf.Cos(randomAngle * Mathf.Deg2Rad), Mathf.Sin(randomAngle * Mathf.Deg2Rad), 1f);
            Vector3 bulletPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, +1f);
            GameObject bullet = Instantiate(bulletPrefab, bulletPos, Quaternion.identity);

            bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;

            yield return new WaitForSeconds(0.2f);

            Destroy(bullet, 5f);
        }
    }
}
