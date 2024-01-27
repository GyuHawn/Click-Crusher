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

    // ����
    public GameObject damageUp; // �⺻������ ����
    public GameObject monsterHealthDown; // ���� ü�� ����
    public GameObject timeUp; // ���ѽð� ����
    public GameObject percentUp; // Ȯ�� ����
    public GameObject monsterDie; // �����ð� ���� ���� ����
    private List<GameObject> buff = new List<GameObject>(); // ���� ����Ʈ

    // �����
    public GameObject damageDown; // �⺻������ ����
    public GameObject monsterHealthUP; // ���� ü�� ����
    public GameObject timeDown; // ���ѽð� ����
    public GameObject percentDown; // Ȯ������
    public GameObject attackSpdDown; // ���� ����
    public GameObject monsterDamageUp; // ���� ������ ����
    public GameObject monsterSpawnUp;// ���� ��ȯ�� ����
    public GameObject monsterSizeUp;// ���� ũ�� ����
    private List<GameObject> deBuff = new List<GameObject>(); // ����� ����Ʈ

    private GameObject selectedEffect; // ���õ� ����

    private void Awake()
    {
        stageTimeLimit = GameObject.Find("Manager").GetComponent<StageTimeLimit>();
        playerController = GameObject.Find("Manager").GetComponent<PlayerController>();
        itemSkill = GameObject.Find("Manager").GetComponent<ItemSkill>();
    }

    void Start()
    {
        // ����Ʈ�� �߰�
        // ����
        buff.Add(damageUp);
        buff.Add(monsterHealthDown);
        buff.Add(timeUp);
        buff.Add(percentUp);
        buff.Add(monsterDie);

        // �����
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

    // ����
    // �⺻����������
    public void DamageUP()
    {

    }
    // ���� ü�� ����
    public void MonsterHealthDown()
    {

    }
    // ���ѽð� ����
    public void TimeUp()
    {

    }
    // Ȯ�� ����
    public void PercentUp()
    {

    }
    // �����ð� ���� ���� ����
    public void MonsterDie()
    {

    }

    // �����
    // �⺻������ ����
    public void DamageDown()
    {

    }
    // ���� ü�� ����
    public void MonsterHealthUP()
    {

    }
    // ���ѽð� ����
    public void TimeDown()
    {

    }
    // Ȯ������
    public void PercentDown()
    {

    }
    // ���� ����
    public void AttackSpdDown()
    {

    }
    // ���� ������ ����
    public void MonsterDamageUp()
    {

    }
    // ���� ��ȯ�� ����
    public void MonsterSpawnUp()
    {

    }
    // ���� ũ�� ����
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
