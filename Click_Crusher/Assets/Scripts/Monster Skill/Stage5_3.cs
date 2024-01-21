using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage5_3 : MonoBehaviour
{
    public GameObject pos;
    public Vector3 boxSize;

    void Start()
    {
        pos = GameObject.Find("Stage7 SkillPos"); // ¸Ê Áß¾Ó À§Ä¡ ºó ¿ÀºêÁ§Æ®
        boxSize = new Vector3(-13.5f, 6.5f, 0);
        StartCoroutine(RandomMovement());
    }

    IEnumerator RandomMovement()
    {
        yield return new WaitForSeconds(5f);

        while (true)
        {
            float randomX = Random.Range(pos.transform.position.x - boxSize.x / 2, pos.transform.position.x + boxSize.x / 2);
            float randomY = Random.Range(pos.transform.position.y - boxSize.y / 2, pos.transform.position.y + boxSize.y / 2);
            float randomZ = -2f;

            transform.position = new Vector3(randomX, randomY, randomZ);

            yield return new WaitForSeconds(5f);
        }
    }
}
