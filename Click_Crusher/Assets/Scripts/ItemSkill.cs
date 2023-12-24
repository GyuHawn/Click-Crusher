using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class ItemSkill : MonoBehaviour
{
    private SelectItem selectItem;

    // fire
    public GameObject fireEffect;
    public Transform firePos;
    public Vector2 fireBoxSize;
    public int fireNum;

    // fireShot
    public GameObject fireShotEffect;
    public GameObject fireShotSub;
    public bool fireShotReady;
    public int fireShotSubNum;

    // holyWave
    public GameObject holyWaveEffect;
    public Transform holyWavePos;

    // holyShot
    public GameObject holyShotEffect;
    public bool holyShotReady;

    // melee
    public GameObject meleeEffect;
    public bool meleeReady;
    public int meleeMaxNum;
    public int meleeNum;

    // posion
    public GameObject posionEffect;
    public bool posionReady;

    // rock
    public GameObject rockEffect;
    public bool rockReady;

    //sturn
    public GameObject sturnEffect;
    public GameObject sturnImage;

    void Start()
    {
        selectItem = GameObject.Find("Manager").GetComponent<SelectItem>();

        fireShotReady = false;
        holyShotReady = false;
        meleeReady = false;
        posionReady = false;
        rockReady = false;
    }

    void Update()
    {
        fireNum = 3 + selectItem.fireLv;
        fireShotSubNum = 3 + selectItem.fireShotLv;
        meleeMaxNum = 5 + selectItem.meleeLv;

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
                rockReady = false;
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
                Melee(hit.point);

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

    // fire --------------------------------
    public void Fire()
    {
        if (!selectItem.itemSelecting)
        {
            for (int i = 0; i < fireNum; i++)
            {
                // 랜덤한 위치 계산
                float randomX = Random.Range(-fireBoxSize.x / 2, fireBoxSize.x / 2);
                float randomY = Random.Range(-fireBoxSize.y / 2, fireBoxSize.y / 2);

                // firePos를 기준으로 랜덤한 위치에 불 효과 생성
                Vector3 randomPos = firePos.position + new Vector3(randomX, randomY, 0);
                GameObject fireInstance = Instantiate(fireEffect, randomPos, Quaternion.Euler(-90, 0, 0));

                // 생성한 불 효과를 3초 후에 제거
                Destroy(fireInstance, 3f);
            }
        }
    }

    // fireShot --------------------------------
    public void FireShotReady()
    {
        if (!selectItem.itemSelecting)
        {
            fireShotReady = true;
        }
    }

    IEnumerator FireShot(Vector3 targetPosition)
    {
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
        if (!selectItem.itemSelecting)
        {
            GameObject WaveInstance = Instantiate(holyWaveEffect, holyWavePos.position, Quaternion.identity);

            Destroy(WaveInstance, 3f);
        }
    }

    // holyShot --------------------------------
    public void HolyShotReady()
    {
        if (!selectItem.itemSelecting)
        {
            holyShotReady = true;
        }
    }

    public void HolyShot(Vector3 targetPosition)
    {
        GameObject holyShotInstance = Instantiate(holyShotEffect, targetPosition, Quaternion.identity);

        Destroy(holyShotInstance, 5f);
    }

    // melee --------------------------------
    public void MeleeReady()
    {
        if (!selectItem.itemSelecting)
        {
            meleeReady = true;
            meleeNum = meleeMaxNum;
        }
    }

    public void Melee(Vector3 targetPosition)
    {
        GameObject meleeInstance = Instantiate(meleeEffect, targetPosition, Quaternion.identity);

        meleeNum--;
        Destroy(meleeInstance, 0.5f);
    }

    // posion --------------------------------
    public void PosiontReady()
    {
        if (!selectItem.itemSelecting)
        {
            posionReady = true;
        }
    }

    public void Posion(Vector3 targetPosition)
    {
        GameObject posionInstance = Instantiate(posionEffect, targetPosition, Quaternion.identity);

        Destroy(posionInstance, 5f);
    }

    // rock --------------------------------
    public void Rockeady()
    {
        if (!selectItem.itemSelecting)
        {
            rockReady = true;
        }
    }

    public void Rock(Vector3 targetPosition)
    {
        GameObject rockInstance = Instantiate(rockEffect, targetPosition, Quaternion.identity);

        Destroy(rockInstance, 5f);
    }

    // sturn --------------------------------
    private Dictionary<GameObject, GameObject> monsterToSturnImage = new Dictionary<GameObject, GameObject>();

    public void Sturn()
    {
        if (!selectItem.itemSelecting)
        {
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
        yield return new WaitForSeconds(3f);

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
