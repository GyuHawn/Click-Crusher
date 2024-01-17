using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3_3 : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed;

    void Start()
    {
        InvokeRepeating("MonsterAttack", 1f, 3f);
    }

    void MonsterAttack()
    {
        float randomAngle = Random.Range(0f, 360f);

        Vector3 direction = new Vector3(Mathf.Cos(randomAngle * Mathf.Deg2Rad), Mathf.Sin(randomAngle * Mathf.Deg2Rad), 1f);

        Vector3 bulletPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, + 1f);
        GameObject bullet = Instantiate(bulletPrefab, bulletPos, Quaternion.identity);

        bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;

        Destroy(bullet, 5f);
    }
}
