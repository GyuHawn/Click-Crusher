using System.Collections;
using UnityEngine;

public class Stage1_1 : MonoBehaviour
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
        GameObject bullet = Instantiate(bulletPrefab, gameObject.transform.position, Quaternion.identity);

        bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;

        Destroy(bullet, 5f);
    }
}
