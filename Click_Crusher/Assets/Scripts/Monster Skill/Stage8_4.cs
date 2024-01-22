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
        pos = GameObject.Find("Stage7 SkillPos"); // �� �߾� ��ġ �� ������Ʈ
        boxSize = new Vector3(-13.5f, 6.5f, 0);
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
            float randomAngle = Random.Range(0f, 360f); // 0~360�� �� ������ ���� ����
            Vector2 direction = new Vector2(Mathf.Cos(Mathf.Deg2Rad * randomAngle), Mathf.Sin(Mathf.Deg2Rad * randomAngle));

            // �Ѿ˿� ���� ������ �����Ͽ� ���ư����� ��
            bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpd;
            Destroy(bullet, 3f);
        }
        yield return new WaitForSeconds(1f);
        bulletList.Clear();
    }
}