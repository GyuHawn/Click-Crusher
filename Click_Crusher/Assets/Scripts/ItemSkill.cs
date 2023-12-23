using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSkill : MonoBehaviour
{
    private SelectItem selectItem;

    public GameObject fireEffect;
    public Transform firePos;
    public Vector2 fireBoxSize;
    public int fireNum;

    public GameObject fireShotEffect;
    public GameObject fireShotSub;
    public bool fireShotReady;
    public int fireShotSubNum;

    public GameObject holyShiledEffect;
    public GameObject holyShotEffect;
    public GameObject meleeEffect;
    public GameObject posionEffect;
    public GameObject rockEffect;
    public GameObject sturnEffect;

    void Start()
    {
        selectItem = GameObject.Find("Manager").GetComponent<SelectItem>();

        fireShotReady = false;
    }

    void Update()
    {
        fireNum = 3 + selectItem.fireLv;
        fireShotSubNum = 3 + selectItem.fireShotLv;

        if (fireShotReady)
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            if (hit.collider != null)
            {
                StartCoroutine(FireShot(hit.point));
                fireShotReady = false;
            }
        }
    }

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

        for (int i = 0; i < fireShotSubNum; i++)
        {
            Debug.Log("");
            GameObject subShot = Instantiate(fireShotSub, targetPosition, Quaternion.identity);
            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            subShot.GetComponent<Rigidbody2D>().velocity = randomDirection * 5f;

            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(1f);

        Destroy(fireShotInstance, 3f);
        fireShotEffect.SetActive(true);
    }

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


    public void HolyShiled()
    {
        if (!selectItem.itemSelecting)
        {

        }
    }
    public void HolyShot()
    {
        if (!selectItem.itemSelecting)
        {

        }
    }
    public void Melee()
    {
        if (!selectItem.itemSelecting)
        {

        }
    }
    public void Posion()
    {
        if (!selectItem.itemSelecting)
        {

        }
    }
    public void Rock()
    {
        if (!selectItem.itemSelecting)
        {

        }
    }
    public void Sturn()
    {
        if (!selectItem.itemSelecting)
        {

        }
    }

    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(firePos.transform.position, fireBoxSize);
    }*/
}
