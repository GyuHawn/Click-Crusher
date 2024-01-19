using System.Collections;
using UnityEngine;

public class Stage4_3 : MonoBehaviour
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
        for(int i = 0; i < 3; i++) 
        {
            float randomX = Random.Range(-2f, 2f);
            Vector3 bulletPos = new Vector3(randomX, 1.5f, 0f);

            GameObject bullet = Instantiate(bulletPrefab, bulletPos, Quaternion.identity);
            bullet.name = "MonsterAttack";

            Destroy(bullet, 2f);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
