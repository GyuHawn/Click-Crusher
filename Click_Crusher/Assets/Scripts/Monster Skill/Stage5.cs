using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage5 : MonoBehaviour
{
    private PlayerController playerController;

    public GameObject bossEffect;

    void Start()
    {
        playerController = GameObject.Find("Manager").GetComponent<PlayerController>();
        InvokeRepeating("Stage5bossSkill", 5f, 10f);
    }

    void Update()
    {
        
    }

    public void Stage5bossSkill()
    {
        StartCoroutine(PlayerDebuff());
    }

    IEnumerator PlayerDebuff()
    {
        GameObject skill = Instantiate(bossEffect, gameObject.transform.position, Quaternion.identity);
        playerController.stage5Debuff = true;

        yield return new WaitForSeconds(5f);

        Destroy(skill);
        playerController.stage5Debuff = false;
    }
}
