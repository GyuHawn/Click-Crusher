using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpwan : MonoBehaviour
{
    private StageManager stagerManager;

    public GameObject[] stage1Monsters; // [0] - baseMonster, [1] - strongMonster, [2] - bossMonster
    public GameObject[] monsterSpawnPoints; // ���� ��ȯ ��ġ
    public GameObject[] bossStageSpawnPoints; // ���� �������� ���� ��ȯ ��ġ

    public Transform pos;

    private void Awake()
    {
        stagerManager = GameObject.Find("Manager").GetComponent<StageManager>();
    }

    public void MonsterInstantiate(int baseCount, int strongCount, int bossCount)
    {
        if(stagerManager.subStage == 5)
        {
            // ���̽� ���� ��ȯ
            for (int i = 0; i < baseCount; i++)
            {
                InstantiateRandom(stage1Monsters[0], bossStageSpawnPoints);
            }

            // ��Ʈ�� ���� ��ȯ
            for (int i = 0; i < strongCount; i++)
            {
                InstantiateRandom(stage1Monsters[1], bossStageSpawnPoints);
            }
            // ���� ���� ��ȯ
            if (bossCount > 0)
            {
                Instantiate(stage1Monsters[2], pos.transform.position, Quaternion.identity);
            }
        }
        else
        {
            // ���̽� ���� ��ȯ
            for (int i = 0; i < baseCount; i++)
            {
                InstantiateRandom(stage1Monsters[0], monsterSpawnPoints);
            }

            // ��Ʈ�� ���� ��ȯ
            for (int i = 0; i < strongCount; i++)
            {
                InstantiateRandom(stage1Monsters[1], monsterSpawnPoints);
            }
        }


    }

    void InstantiateRandom(GameObject monsterPrefab, GameObject[] spawnPoints)
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Vector3 randomPosition = GetRandomPosition(spawnPoints[randomIndex].transform.position);
        Instantiate(monsterPrefab, randomPosition, Quaternion.identity);
    }

    Vector3 GetRandomPosition(Vector3 center)
    {
        float radius = 1.0f; // ���� ������ ���� ������

        float x = Random.Range(center.x - radius, center.x + radius);
        float y = Random.Range(center.y - radius, center.y + radius);
        float z = center.z;

        return new Vector3(x, y, z);
    }
}
