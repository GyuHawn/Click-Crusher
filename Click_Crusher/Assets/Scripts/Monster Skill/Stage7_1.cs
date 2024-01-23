using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Stage7_1 : MonoBehaviour
{
    public GameObject pos;
    public Vector3 boxSize;
    public float moveSpd;

    void Start()
    {
        pos = GameObject.Find("Stage7 SkillPos"); // ¸Ê Áß¾Ó À§Ä¡ ºó ¿ÀºêÁ§Æ®
        boxSize = new Vector3(-13.5f, 6.5f, 0);
    }

    public void Attack()
    {
        float randomY = Random.Range(pos.transform.position.y - boxSize.y / 2, pos.transform.position.y + boxSize.y / 2);
        Vector3 newPosition = new Vector3(transform.position.x, randomY, transform.position.z);

        StartCoroutine(MoveToPosition(newPosition));
    }
    IEnumerator MoveToPosition(Vector3 newPosition)
    {
        while (transform.position != newPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, newPosition, moveSpd * Time.deltaTime);
            yield return null;
        }
    }

}
