using System.Collections;
using UnityEngine;

public class Stage8_3 : MonoBehaviour
{
    public GameObject effectPrifab;
    public GameObject PoisonPrefab;
    public GameObject pos;
    public Vector3 boxSize;

    private Vector3 beforePos;

    void Start()
    {
        pos = GameObject.Find("Stage7 SkillPos");
        beforePos = gameObject.transform.position;
        StartCoroutine(RandomMovement());
    }

    IEnumerator RandomMovement()
    {
        yield return new WaitForSeconds(3f);

        while (true)
        {
            float randomX = Random.Range(pos.transform.position.x - boxSize.x / 2, pos.transform.position.x + boxSize.x / 2);
            float randomY = Random.Range(pos.transform.position.y - boxSize.y / 2, pos.transform.position.y + boxSize.y / 2);
            float randomZ = -2f;

            transform.position = new Vector3(randomX, randomY, randomZ);

            StartCoroutine(SpawnEffect(beforePos));

            beforePos = transform.position;

            yield return new WaitForSeconds(5f);
        }
    }

    IEnumerator SpawnEffect(Vector3 position)
    {
        GameObject poison = Instantiate(PoisonPrefab, position, Quaternion.identity);
        GameObject effect = Instantiate(effectPrifab, position, Quaternion.Euler(60f, 0f, 0f));
        poison.name = "MonsterAttack";

        yield return new WaitForSeconds(8f);

        Destroy(poison);
    }
}
