using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage8_4 : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpd;

    private List<GameObject> bulletList = new List<GameObject>();

    public GameObject pos;
    public Vector3 boxSize;

    void Start()
    {
        pos = GameObject.Find("Stage7 SkillPos"); // 맵 중앙 위치 빈 오브젝트
        boxSize = new Vector3(-13f, 5.5f, 0);
    }

    public void Attack()
    {
        StartCoroutine(MonsterAttack());
    }

    IEnumerator MonsterAttack()
    {
        for (int i = 0; i < 10; i++)
        {
            float randomX = Random.Range(-boxSize.x / 2f, boxSize.x / 2f);
            float randomY = Random.Range(-boxSize.y / 2f, boxSize.y / 2f);

            GameObject bullet = Instantiate(bulletPrefab, pos.transform.position + new Vector3(randomX, randomY, 1f), Quaternion.identity);
            bullet.name = "MonsterAttack";
            bulletList.Add(bullet);


            yield return new WaitForSeconds(0.3f);
        }

        foreach (GameObject bullet in bulletList)
        {
            float randomAngle = Random.Range(0f, 360f); // 0~360도 중 랜덤한 각도 설정
            Vector2 direction = new Vector2(Mathf.Cos(Mathf.Deg2Rad * randomAngle), Mathf.Sin(Mathf.Deg2Rad * randomAngle));

            // 총알에 대해 방향을 설정하여 날아가도록 함
            bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpd;
            Destroy(bullet, 3f);
        }
        yield return new WaitForSeconds(1f);
        bulletList.Clear();
    }
}
