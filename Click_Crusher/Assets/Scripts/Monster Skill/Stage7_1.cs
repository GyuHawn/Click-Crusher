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
        boxSize = new Vector3(-13.5f, 6.5f, 0);
    }

    public void Attack()
    {
        float randomY = Random.Range(pos.transform.position.y - boxSize.y / 2, pos.transform.position.y + boxSize.y / 2);

        Vector3 newPosition = new Vector3(transform.position.x, randomY, transform.position.z);

        transform.position = newPosition;
    }

}
