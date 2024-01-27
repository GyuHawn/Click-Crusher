using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageStatus : MonoBehaviour
{
    private StageTimeLimit stageTimeLimit;
    private PlayerController playerController;
    private ItemSkill itemSkill;

    public Image stageStatus;
    public GameObject statusPos;

    // 버프
    public GameObject damageUp; // 기본데미지 증가
    public GameObject monsterHealthDown; // 몬스터 체력 감소
    public GameObject timeUp; // 제한시간 증가
    public GameObject percentUp; // 확률 증가
    public GameObject monsterDie; // 일정시간 마다 몬스터 제거
    private List<GameObject> buff = new List<GameObject>(); // 버프 리스트

    // 디버프
    public GameObject damageDown; // 기본데미지 감소
    public GameObject monsterHealthUP; // 몬스터 체력 증가
    public GameObject timeDown; // 제한시간 감소
    public GameObject percentDown; // 확률감소
    public GameObject attackSpdDown; // 공속 감소
    public GameObject monsterDamageUp; // 몬스터 데미지 증가
    public GameObject monsterSpawnUp;// 몬스터 소환량 증가
    public GameObject monsterSizeUp;// 몬스터 크기 감소
    private List<GameObject> deBuff = new List<GameObject>(); // 디버프 리스트

    private GameObject selectedEffect; // 선택된 버프

    private void Awake()
    {
        stageTimeLimit = GameObject.Find("Manager").GetComponent<StageTimeLimit>();
        playerController = GameObject.Find("Manager").GetComponent<PlayerController>();
        itemSkill = GameObject.Find("Manager").GetComponent<ItemSkill>();
    }

    void Start()
    {
        // 리스트에 추가
        // 버프
        buff.Add(damageUp);
        buff.Add(monsterHealthDown);
        buff.Add(timeUp);
        buff.Add(percentUp);
        buff.Add(monsterDie);

        // 디버프
        deBuff.Add(damageDown);
        deBuff.Add(monsterHealthUP);
        deBuff.Add(timeDown);
        deBuff.Add(percentDown);
        deBuff.Add(attackSpdDown);
        deBuff.Add(monsterDamageUp);
        deBuff.Add(monsterSpawnUp);
        deBuff.Add(monsterSizeUp);
    }
    
    void Update()
    {
        
    }

    // 버프
    // 기본데미지증가
    public void DamageUP()
    {

    }
    // 몬스터 체력 감소
    public void MonsterHealthDown()
    {

    }
    // 제한시간 증가
    public void TimeUp()
    {

    }
    // 확률 증가
    public void PercentUp()
    {

    }
    // 일정시간 마다 몬스터 제거
    public void MonsterDie()
    {

    }

    // 디버프
    // 기본데미지 감소
    public void DamageDown()
    {

    }
    // 몬스터 체력 증가
    public void MonsterHealthUP()
    {

    }
    // 제한시간 감소
    public void TimeDown()
    {

    }
    // 확률감소
    public void PercentDown()
    {

    }
    // 공속 감소
    public void AttackSpdDown()
    {

    }
    // 몬스터 데미지 증가
    public void MonsterDamageUp()
    {

    }
    // 몬스터 소환량 증가
    public void MonsterSpawnUp()
    {

    }
    // 몬스터 크기 감소
    public void MonsterSizeUp()
    {

    }

    public void BuffStatus()
    {
        List<GameObject> selectedList = (Random.Range(0, 2) == 0) ? buff : deBuff;

        if (selectedList.Count > 0)
        {
            int randomIndex = Random.Range(0, selectedList.Count);
            selectedEffect = selectedList[randomIndex];

            selectedEffect.transform.position = statusPos.transform.position;

            if (selectedList == buff)
            {
                Buff();
            }
            else
            {
                DeBuff();
            }
        }
    }

    public void ResetStatus()
    {
        if (selectedEffect != null)
        {
            selectedEffect.transform.position = new Vector3(100, 1500, 0);

            selectedEffect = null;
        }
    }

    void Buff()
    {
        stageStatus.color = new Color(0f, 0.49f, 1f);
    }

    void DeBuff()
    {
        stageStatus.color = Color.red;
    }
}
