using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpwan : MonoBehaviour
{
    private StageManager stagerManager;

    public GameObject[] stage1Monsters; // [0] - baseMonster, [1] - strongMonster, [2] - bossMonster
    public GameObject[] monsterSpawnPoints; // 몬스터 소환 위치
    public GameObject[] bossStageSpawnPoints; // 보스 스테이지 몬스터 소환 위치

    public Transform pos;

    private void Awake()
    {
        stagerManager = GameObject.Find("Manager").GetComponent<StageManager>();
    }

    public void MonsterInstantiate(int baseCount, int strongCount, int bossCount)
    {
        if(stagerManager.subStage == 5)
        {
            // 베이스 몬스터 소환
            for (int i = 0; i < baseCount; i++)
            {
                InstantiateRandom(stage1Monsters[0], bossStageSpawnPoints);
            }

            // 스트롱 몬스터 소환
            for (int i = 0; i < strongCount; i++)
            {
                InstantiateRandom(stage1Monsters[1], bossStageSpawnPoints);
            }
            // 보스 몬스터 소환
            if (bossCount > 0)
            {
                Instantiate(stage1Monsters[2], pos.transform.position, Quaternion.identity);
            }
        }
        else
        {
            // 베이스 몬스터 소환
            for (int i = 0; i < baseCount; i++)
            {
                InstantiateRandom(stage1Monsters[0], monsterSpawnPoints);
            }

            // 스트롱 몬스터 소환
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
        float radius = 1.0f; // 간격 조절을 위한 반지름

        float x = Random.Range(center.x - radius, center.x + radius);
        float y = Random.Range(center.y - radius, center.y + radius);
        float z = center.z;

        return new Vector3(x, y, z);
    }
}
