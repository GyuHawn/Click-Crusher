using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class ItemSkill : MonoBehaviour
{
    private SelectItem selectItem;

    // fire
    private GameObject fireInstance;
    public GameObject fireEffect;
    public Transform firePos;
    public Vector2 fireBoxSize;
    public int fireNum;
    public float fireDamage;
    public float fireTime;
    public GameObject fireCoolTime;
    public TMP_Text fireCoolTimeText;

    // fireShot
    public GameObject fireShotEffect;
    public GameObject fireShotSub;
    public bool fireShotReady;
    public int fireShotSubNum;
    public float fireShotDamage;
    public float fireShoSubDamage;
    public float fireShotTime;
    public GameObject fireShotCoolTime;
    public TMP_Text fireShotCoolTimeText;

    // holyWave
    private GameObject WaveInstance;
    public GameObject holyWaveEffect;
    public Transform holyWavePos;
    public bool holyWave;
    public float holyWaveDuration;
    public float holyWaveDamage;
    public float holyWaveTime;
    public GameObject holyWaveCoolTime;
    public TMP_Text holyWaveCoolTimeText;

    // holyShot
    public GameObject holyShotEffect;
    public bool holyShotReady;
    public float holyShotDuration;
    public float holyShotDamage;
    public float holyShotTime;
    public GameObject holyShotCoolTime;
    public TMP_Text holyShotCoolTimeText;

    // melee
    public GameObject meleeEffect;
    public bool meleeReady;
    public int meleeMaxNum;
    public int meleeNum;
    public float meleeTime;
    public GameObject meleeCoolTime;
    public TMP_Text meleeCoolTimeText;

    // posion
    public GameObject posionEffect;
    public bool posionReady;
    public float posionDuration;
    public float poisonDamage;
    public float posionTime;
    public GameObject posionCoolTime;
    public TMP_Text posionCoolTimeText;

    // rock
    public GameObject rockEffect;
    public bool rockReady;
    public float rockDamage;
    public float rockTime;
    public GameObject rockCoolTime;
    public TMP_Text rockCoolTimeText;

    // sturn
    public GameObject sturnEffect;
    public GameObject sturnImage;
    public float sturnDuration;
    public float sturnTime;
    public GameObject sturnCoolTime;
    public TMP_Text sturnCoolTimeText;

    void Start()
    {
        selectItem = GameObject.Find("Manager").GetComponent<SelectItem>();

        // 사용준비
        fireShotReady = false;
        holyShotReady = false;
        meleeReady = false;
        posionReady = false;
        rockReady = false;

        // 사용중인지
        holyWave = false;
    }

    void Update()
    {
        SkillCoolTime();

        fireNum = 3 + selectItem.fireLv;
        fireShotSubNum = 3 + selectItem.fireShotLv;
        meleeMaxNum = 5 + selectItem.meleeLv;
        holyWaveDuration = 3 + selectItem.holyWaveLv;
        holyShotTime = 3 + selectItem.holyShotLv;
        posionTime = 3 + selectItem.posionLv;
        rockDamage = 5 * selectItem.rockLv;
        sturnTime = 3 + selectItem.sturnLv;

        // fireShot
        if (fireShotReady && (Input.GetMouseButtonDown(0) || Input.touchCount > 0))
        {
            Vector2 inputPosition = Input.GetMouseButtonDown(0) ? Input.mousePosition : Input.GetTouch(0).position;
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(inputPosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            if (hit.collider != null)
            {
                StartCoroutine(FireShot(hit.point));
                fireShotReady = false;
            }
        }

        // holyShot
        if (holyShotReady && Input.GetMouseButtonDown(0))
        {
            Vector2 inputPosition = Input.GetMouseButtonDown(0) ? Input.mousePosition : Input.GetTouch(0).position;
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(inputPosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            if (hit.collider != null)
            {
                HolyShot(hit.point);
                holyShotReady = false;
            }
        }

        // rock
        if (rockReady && Input.GetMouseButtonDown(0))
        {
            Vector2 inputPosition = Input.GetMouseButtonDown(0) ? Input.mousePosition : Input.GetTouch(0).position;
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(inputPosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            if (hit.collider != null)
            {
                Rock(hit.point);
            }
        }

        // melee
        if (meleeReady && Input.GetMouseButtonDown(0))
        {
            Vector2 inputPosition = Input.GetMouseButtonDown(0) ? Input.mousePosition : Input.GetTouch(0).position;
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(inputPosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            if (hit.collider != null)
            {
                Melee(hit.point, 4);

                if (meleeNum <= 0)
                {
                    meleeReady = false;
                }
            }
        }

        // posion
        if (posionReady && Input.GetMouseButtonDown(0))
        {
            Vector2 inputPosition = Input.GetMouseButtonDown(0) ? Input.mousePosition : Input.GetTouch(0).position;
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(inputPosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            if (hit.collider != null)
            {
                Posion(hit.point);
                posionReady = false;
            }
        }
    }

    void SkillCoolTime()
    {

        // fire
        if (fireTime >= 0)
        {
            fireCoolTime.SetActive(true);
            fireCoolTimeText.text = ((int)fireTime).ToString();
            fireTime -= Time.deltaTime;
        }
        else
        {
            fireCoolTime.SetActive(false);
        }

        // fireShot
        if (fireShotTime >= 0)
        {
            fireShotCoolTime.SetActive(true);
            fireShotCoolTimeText.text = ((int)fireShotTime).ToString();
            fireShotTime -= Time.deltaTime;
        }
        else
        {
            fireShotCoolTime.SetActive(false);
        }

        // holyWave
        if (holyWaveTime >= 0)
        {
            holyWaveCoolTime.SetActive(true);
            holyWaveCoolTimeText.text = ((int)holyWaveTime).ToString();
            holyWaveTime -= Time.deltaTime;
        }
        else
        {
            holyWaveCoolTime.SetActive(false);
        }

        // holyShot
        if (holyShotTime >= 0)
        {
            holyShotCoolTime.SetActive(true);
            holyShotCoolTimeText.text = ((int)holyShotTime).ToString();
            holyShotTime -= Time.deltaTime;
        }
        else
        {
            holyShotCoolTime.SetActive(false);
        }

        // melee
        if (meleeTime >= 0)
        {
            meleeCoolTime.SetActive(true);
            meleeCoolTimeText.text = ((int)meleeTime).ToString();
            meleeTime -= Time.deltaTime;
        }
        else
        {
            meleeCoolTime.SetActive(false);
        }

        // posion
        if (posionTime >= 0)
        {
            posionCoolTime.SetActive(true);
            posionCoolTimeText.text = ((int)posionTime).ToString();
            posionTime -= Time.deltaTime;
        }
        else
        {
            posionCoolTime.SetActive(false);
        }

        // rock
        if (rockTime >= 0)
        {
            rockCoolTime.SetActive(true);
            rockCoolTimeText.text = ((int)rockTime).ToString();
            rockTime -= Time.deltaTime;
        }
        else
        {
            rockCoolTime.SetActive(false);
        }

        // sturn
        if (sturnTime >= 0)
        {
            sturnCoolTime.SetActive(true);
            sturnCoolTimeText.text = ((int)sturnTime).ToString();
            sturnTime -= Time.deltaTime;
        }
        else
        {
            sturnCoolTime.SetActive(false);
        }
    }

    // fire --------------------------------
    public void Fire()
    {
        if (!selectItem.itemSelecting && fireTime <= 0)
        {
            fireTime = 5;

            for (int i = 0; i < fireNum; i++)
            {
                // 랜덤한 위치 계산
                float randomX = Random.Range(-fireBoxSize.x / 2, fireBoxSize.x / 2);
                float randomY = Random.Range(-fireBoxSize.y / 2, fireBoxSize.y / 2);

                // firePos를 기준으로 랜덤한 위치에 불 효과 생성
                Vector3 randomPos = firePos.position + new Vector3(randomX, randomY, 0);
                fireInstance = Instantiate(fireEffect, randomPos, Quaternion.Euler(-90, 0, 0));

                // 생성한 불 효과를 3초 후에 제거
                Destroy(fireInstance, 3f);
            }
        }
    }

    // fireShot --------------------------------
    public void FireShotReady()
    {
        if (!selectItem.itemSelecting && fireShotTime <= 0)
        {
            fireShotReady = true;
        }
    }

    IEnumerator FireShot(Vector3 targetPosition)
    {
        fireShotTime = 5;

        GameObject fireShotInstance = Instantiate(fireShotEffect, targetPosition, Quaternion.identity);

        List<GameObject> sub = new List<GameObject>();

        for (int i = 0; i < fireShotSubNum; i++)
        {
            GameObject subShot = Instantiate(fireShotSub, targetPosition, Quaternion.identity);
            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            subShot.GetComponent<Rigidbody2D>().velocity = randomDirection * 5f;

            sub.Add(subShot);
        }
        yield return new WaitForSeconds(2f);

        foreach(GameObject delete in sub)
        {
            Destroy(delete);
        }

        Destroy(fireShotInstance, 3f);
    }

    // holyWave --------------------------------
    
    public void HolyWave()
    {
        if (!selectItem.itemSelecting && holyWaveTime <= 0)
        {
            holyWaveTime = 5;

            WaveInstance = Instantiate(holyWaveEffect, holyWavePos.position, Quaternion.identity);
            holyWave = true;

            Destroy(WaveInstance, holyWaveDuration);
            StartCoroutine(DestroyWave());
        }
    }

    IEnumerator DestroyWave()
    {
        yield return new WaitForSeconds(holyWaveDuration);
        Destroy(WaveInstance);
        holyWave = false;
    }

    // holyShot --------------------------------
    public void HolyShotReady()
    {
        if (!selectItem.itemSelecting && holyShotTime <= 0)
        {
            holyShotReady = true;
        }
    }

    public void HolyShot(Vector3 targetPosition)
    {
        holyShotTime = 5;

        GameObject holyShotInstance = Instantiate(holyShotEffect, targetPosition, Quaternion.identity);

        StartCoroutine(RotateHolyShot(holyShotInstance, holyShotTime));
    }

    private IEnumerator RotateHolyShot(GameObject holyShotInstance, float duration)
    {
        float elapsedTime = 0f;
        float rotationSpeed = 360f / duration;

        while (elapsedTime < duration)
        {
            holyShotInstance.transform.Rotate(rotationSpeed * Time.deltaTime, 0f, 0f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(holyShotInstance); 
    }
  
    // melee --------------------------------
    public void MeleeReady()
    {
        if (!selectItem.itemSelecting && meleeTime <= 0)
        {
            meleeReady = true;
            meleeNum = meleeMaxNum;
        }
    }

    public void Melee(Vector3 targetPosition, int numEffects)
    {
        meleeTime = 5;

        StartCoroutine(MeleeInstantiate(targetPosition , numEffects));
        meleeNum--;
    }

    IEnumerator MeleeInstantiate(Vector3 targetPosition, int numEffects)
    {
        for (int i = 0; i < numEffects; i++)
        {
            Vector3 randomOffset = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
            Vector3 spawnPosition = targetPosition + randomOffset;

            GameObject meleeInstance = Instantiate(meleeEffect, spawnPosition, Quaternion.identity);

            Destroy(meleeInstance, 0.5f);

            yield return new WaitForSeconds(0.1f);
        }
    }

    // posion --------------------------------
    public void PosiontReady()
    {
        if (!selectItem.itemSelecting && posionTime <= 0)
        {
            posionReady = true;
        }
    }

    public void Posion(Vector3 targetPosition)
    {
        posionTime = 5;

        GameObject posionInstance = Instantiate(posionEffect, targetPosition, Quaternion.identity);

        Destroy(posionInstance, 5f);
    }

    // rock --------------------------------
    public void Rockeady()
    {
        if (!selectItem.itemSelecting && fireTime <= 0)
        {
            rockReady = true;
        }
    }

    public void Rock(Vector3 targetPosition)
    {
        fireShotTime = 5;

        GameObject rockInstance = Instantiate(rockEffect, targetPosition, Quaternion.identity);

        Destroy(rockInstance, 5f);
    }

    // sturn --------------------------------
    private Dictionary<GameObject, GameObject> monsterToSturnImage = new Dictionary<GameObject, GameObject>();

    public void Sturn()
    {
        if (!selectItem.itemSelecting && sturnTime <= 0)
        {
            sturnTime = 5;

            GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");

            foreach (GameObject monster in monsters)
            {
                MonsterController monsterController = monster.GetComponent<MonsterController>();
                GameObject sturnInstance = Instantiate(sturnEffect, monster.transform.position, Quaternion.identity);
                GameObject sturnimageInstance = Instantiate(sturnImage, monsterController.sturn.transform.position, Quaternion.identity);

                if (monsterController != null)
                {
                    monsterController.stop = true;
                    monsterController.attackTime += 5;
                }

                monsterToSturnImage[monster] = sturnimageInstance;

                Destroy(sturnimageInstance, 3f);
            }

            StartCoroutine(Removestun());
        }
    }

    IEnumerator Removestun()
    {
        yield return new WaitForSeconds(sturnTime);

        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
        foreach (GameObject monster in monsters)
        {
            MonsterController monsterController = monster.GetComponent<MonsterController>();

            if (monsterController != null)
            {
                monsterController.stop = false;
            }
        }
    }

    public void DestroyMonster(GameObject monster)
    {
        if (monsterToSturnImage.ContainsKey(monster))
        {
            Destroy(monsterToSturnImage[monster]);
            monsterToSturnImage.Remove(monster);
        }

        Destroy(monster);
    }

    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(firePos.transform.position, fireBoxSize);
    }*/
}
