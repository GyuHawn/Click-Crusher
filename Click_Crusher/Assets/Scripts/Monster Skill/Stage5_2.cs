using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage5_2 : MonoBehaviour
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
        yield return new WaitForSeconds(5f);

        while (true)
        {
            float randomX = Random.Range(pos.transform.position.x - boxSize.x / 2, pos.transform.position.x + boxSize.x / 2);

            Vector3 newPosition = new Vector3(randomX, transform.position.y, transform.position.z);

            transform.position = newPosition;

            yield return new WaitForSeconds(5f);
        }
    }
}
