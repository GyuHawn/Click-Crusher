using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage7_1 : MonoBehaviour
{
    public GameObject pos;
    public Vector3 boxSize;

    void Start()
    {
        pos = GameObject.Find("Stage7 SkillPos"); // ¸Ê Áß¾Ó À§Ä¡ ºó ¿ÀºêÁ§Æ®
        StartCoroutine(RandomMovement());
    }

    IEnumerator RandomMovement()
    {
        yield return new WaitForSeconds(4f);

        while (true)
        {
            float randomY = Random.Range(pos.transform.position.y - boxSize.y / 2, pos.transform.position.y + boxSize.y / 2);

            Vector3 newPosition = new Vector3(transform.position.x, randomY, transform.position.z);

            transform.position = newPosition;

            yield return new WaitForSeconds(5f);
        }
    }
}
