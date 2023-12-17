using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpwan : MonoBehaviour
{
    private StageManager stagerManage;

    private int stage;

    public GameObject[] stage1Monsters; // [0] - baseMonster, [1] - strongMonster, [2] - bossMonster
    public GameObject[] stage2Monsters;
    public GameObject[] stage3Monsters;
    public GameObject[] stage4Monsters;

    public GameObject[] monsterSpawnPoints; // 몬스터 소환 위치
    public GameObject[] bossStageSpawnPoints; // 보스 스테이지 몬스터 소환 위치

    public Transform pos;

    private void Awake()
    {
        stagerManage = GameObject.Find("Manager").GetComponent<StageManager>();
    }

    private void Update()
    {
        stage = stagerManage.mainStage;
    }

    public void MonsterInstantiate(int baseCount, int strongCount, int bossCount)
    {     
        if (stagerManage.subStage == 5)
        {
            if (stagerManage.mainStage == 1)
            {
                for (int i = 0; i < baseCount; i++)
                {
                    InstantiateRandom(stage1Monsters[0], bossStageSpawnPoints);
                }
                for (int i = 0; i < strongCount; i++)
                {
                    InstantiateRandom(stage1Monsters[1], bossStageSpawnPoints);
                }
                if (bossCount > 0)
                {
                    Instantiate(stage1Monsters[2], pos.position, Quaternion.identity);
                }
            }
            else if (stagerManage.mainStage == 2)
            {
                for (int i = 0; i < baseCount; i++)
                {
                    InstantiateRandom(stage2Monsters[0], bossStageSpawnPoints);
                }
                for (int i = 0; i < strongCount; i++)
                {
                    InstantiateRandom(stage2Monsters[1], bossStageSpawnPoints);
                }
                if (bossCount > 0)
                {
                    Instantiate(stage2Monsters[2], pos.position, Quaternion.identity);
                }
            }
            else if (stagerManage.mainStage == 3)
            {
                for (int i = 0; i < baseCount; i++)
                {
                    InstantiateRandom(stage3Monsters[0], bossStageSpawnPoints);
                }
                for (int i = 0; i < strongCount; i++)
                {
                    InstantiateRandom(stage3Monsters[1], bossStageSpawnPoints);
                }
                if (bossCount > 0)
                {
                    Instantiate(stage3Monsters[2], pos.position, Quaternion.identity);
                }
            }
            else if (stagerManage.mainStage == 4)
            {
                for (int i = 0; i < baseCount; i++)
                {
                    InstantiateRandom(stage4Monsters[0], bossStageSpawnPoints);
                }

                for (int i = 0; i < strongCount; i++)
                {
                    InstantiateRandom(stage4Monsters[1], bossStageSpawnPoints);
                }
                if (bossCount > 0)
                {
                    Instantiate(stage4Monsters[2], pos.position, Quaternion.identity);
                }
            }
        }

        if (stagerManage.subStage != 5)
        {
            if (stagerManage.mainStage == 1)
            {
                for (int i = 0; i < baseCount; i++)
                {
                    InstantiateRandom(stage1Monsters[0], monsterSpawnPoints);
                }
                for (int i = 0; i < strongCount; i++)
                {
                    InstantiateRandom(stage1Monsters[1], monsterSpawnPoints);
                }
            }
            else if (stagerManage.mainStage == 2)
            {
                for (int i = 0; i < baseCount; i++)
                {
                    InstantiateRandom(stage2Monsters[0], monsterSpawnPoints);
                }
                for (int i = 0; i < strongCount; i++)
                {
                    InstantiateRandom(stage2Monsters[1], monsterSpawnPoints);
                }
            }
            else if (stagerManage.mainStage == 3)
            {
                for (int i = 0; i < baseCount; i++)
                {
                    InstantiateRandom(stage3Monsters[0], monsterSpawnPoints);
                }
                for (int i = 0; i < strongCount; i++)
                {
                    InstantiateRandom(stage3Monsters[1], monsterSpawnPoints);
                }
            }
            else if (stagerManage.mainStage == 4)
            {
                for (int i = 0; i < baseCount; i++)
                {
                    InstantiateRandom(stage4Monsters[0], monsterSpawnPoints);
                }
                for (int i = 0; i < strongCount; i++)
                {
                    InstantiateRandom(stage4Monsters[1], monsterSpawnPoints);
                }
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
        float radius = 1.0f;

        float x = Random.Range(center.x - radius, center.x + radius);
        float y = Random.Range(center.y - radius, center.y + radius);
        float z = center.z;

        return new Vector3(x, y, z);
    }
}
