using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    private StageManager stagerManager;

    void Start()
    {
        stagerManager = GameObject.Find("Manager").GetComponent<StageManager>();
    }

    void Update()
    {
        
    }
    private void OnDestroy()
    {
        stagerManager.monsterCount--;
        stagerManager.NextStage();
    }
}
