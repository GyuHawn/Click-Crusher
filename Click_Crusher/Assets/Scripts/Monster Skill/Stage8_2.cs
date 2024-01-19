using System.Collections;
using UnityEngine;

public class Stage8_2 : MonoBehaviour
{
    public GameObject attackEffectPrefab; // ¿Ã∆Â∆Æ «¡∏Æ∆’
    public GameObject bulletPrefab; // √—æÀ «¡∏Æ∆’
    public float bulletSpd;

    private Vector3 lastBulletPos;
    
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
        float randomAngle = Random.Range(0f, 360f);

        Vector3 direction = new Vector3(Mathf.Cos(randomAngle * Mathf.Deg2Rad), Mathf.Sin(randomAngle * Mathf.Deg2Rad), 1f);
        Vector3 bulletPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, +1f);
        GameObject bullet = Instantiate(bulletPrefab, bulletPos, Quaternion.identity);
        bullet.name = "MonsterAttack";
        bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpd;

        yield return new WaitForSeconds(2f);
        lastBulletPos = bullet.transform.position;
        Destroy(bullet);

        StartCoroutine(SpawnEffect(lastBulletPos));
    }

    IEnumerator SpawnEffect(Vector3 position)
    {
        GameObject effect = Instantiate(attackEffectPrefab, position, Quaternion.identity);

        yield return new WaitForSeconds(10f);

        Destroy(effect);
    }
}
