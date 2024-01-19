using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage6_3 : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject pos;
    public Vector3 boxSize;

    void Start()
    {
        pos = GameObject.Find("Stage7 SkillPos"); // ¸Ê Áß¾Ó À§Ä¡ ºó ¿ÀºêÁ§Æ®
        StartCoroutine(MonsterAttack());
    }

    IEnumerator MonsterAttack()
    {
        while (true)
        { 
            float randomX = Random.Range(pos.transform.position.x - boxSize.x / 2, pos.transform.position.x + boxSize.x / 2);
            float randomY = Random.Range(pos.transform.position.y - boxSize.y / 2, pos.transform.position.y + boxSize.y / 2);
            float randomZ = -2f;

           Vector3 bulletPos = new Vector3(randomX, randomY, randomZ);

            GameObject bullet = Instantiate(bulletPrefab, bulletPos, Quaternion.Euler(180, 0, 0));
            bullet.name = "MonsterAttack";

            Destroy(bullet, 2f);
            yield return new WaitForSeconds(0.8f);
        }
    }
}
