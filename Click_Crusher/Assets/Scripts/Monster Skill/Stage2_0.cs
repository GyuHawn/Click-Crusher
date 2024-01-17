using System.Collections;
using UnityEngine;

public class Stage2_0 : MonoBehaviour
{
    public GameObject bulletPrefab;
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
        for (int i = 0; i < 3; i++)
        {
            float randomAngle = Random.Range(0f, 360f);

            Vector3 direction = new Vector3(Mathf.Cos(randomAngle * Mathf.Deg2Rad), Mathf.Sin(randomAngle * Mathf.Deg2Rad), 1f);
            Vector3 bulletPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, +1f);
            GameObject bullet = Instantiate(bulletPrefab, bulletPos, Quaternion.identity);

            bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;

            Destroy(bullet, 5f);

            yield return new WaitForSeconds(0.2f);
        }
    }
}
