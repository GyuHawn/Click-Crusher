using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Stage2 : MonoBehaviour
{
    private MonsterController monsterController;
    public GameObject subBoss;
    public GameObject bossEffect;
    public float onSkillHealth;
    private bool onSkill = false;
    public GameObject skillPos;

    void Start()
    {
        monsterController = gameObject.GetComponent<MonsterController>();
        skillPos = GameObject.Find("Stage2BossSkill");
    }
    
    void Update()
    {
        if(monsterController.currentHealth <= onSkillHealth && !onSkill)
        {
            Stage2bossSkill();
        }
    }

    public void Stage2bossSkill()
    {
        onSkill = true;
        GameObject skill = Instantiate(bossEffect, skillPos.transform.position, Quaternion.identity);
        GameObject subInstantiate = Instantiate(subBoss, skillPos.transform.position, Quaternion.identity);
    }
}
