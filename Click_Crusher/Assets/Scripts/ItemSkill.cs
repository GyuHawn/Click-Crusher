using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemSkill : MonoBehaviour
{
    private SelectItem selectItem;
    private PlayerController playerController;
    private AudioManager audioManager;
    private Character character;

    // fire
    private GameObject fireInstance;
    public GameObject fireEffect;
    public float fireDamage;
    public float fireDamagePercent;
    public float fireDuration;
    public float firePercent;
    public bool isFire;

    // fireShot
    public GameObject fireShotEffect;
    public GameObject fireShotSub;
    public int fireShotSubNum;
    public float fireShotDamage;
    public float fireShotDamagePercent;
    public float fireShotSubDamage;
    public float fireShotSubDamagePercent;
    public float fireShotPercent;
    public bool isFireShot;

    // holyWave
    private GameObject WaveInstance;
    public GameObject holyWaveEffect;
    public Transform holyWavePos;
    public bool holyWave;
    public float holyWaveDuration;
    public float holyWaveDamage;
    public float holyWaveDamagePercent;
    public float holyWavePercent;
    public bool isHolyWave;

    // holyShot
    public GameObject holyShotEffect;
    public float holyShotDuration;
    public float holyShotDamage;
    public float holyShotDamagePercent;
    public float holyShotPercent;
    public bool isHolyShot;

    // melee
    public GameObject meleeEffect;
    public int meleeNum;
    public float meleePercent;
    public bool isMelee;

    // posion
    public GameObject posionEffect;
    public float posionDuration;
    public float poisonDamage;
    public float poisonDamagePercent;
    public float posionPercent;
    public bool isPosion;

    // rock
    public GameObject rockEffect;
    public float rockDamage;
    public float rockDamagePercent;
    public float rockPercent;
    public bool isRock;

    // sturn
    public GameObject sturnEffect;
    public GameObject sturnImage;
    public float sturnDuration;
    public float sturnPercent;
    public bool isSturn;
    private GameObject currentAttackedMonster;

    void Start()
    {
        selectItem = GameObject.Find("Manager").GetComponent<SelectItem>();
        playerController = GameObject.Find("Manager").GetComponent<PlayerController>();
        audioManager = GameObject.Find("Manager").GetComponent<AudioManager>();
        character = GameObject.Find("Manager").GetComponent<Character>();

        // 사용중인지
        holyWave = false;

        BasicSettings();
    }

    public void BasicSettings()
    {
        // 공격력 퍼센트
        fireDamagePercent = 0.5f;
        fireShotDamagePercent = 1.5f;
        fireShotSubDamagePercent = 0.5f;
        holyWaveDamagePercent = 0.3f;
        holyShotDamagePercent = 0.7f;
        poisonDamagePercent = 0.4f;
        rockDamagePercent = 2f;

        // 공격력
        fireDamage = playerController.damage * fireDamagePercent;
        fireShotDamage = playerController.damage * fireShotDamagePercent;
        fireShotSubDamage = playerController.damage * fireShotSubDamagePercent;
        holyWaveDamage = playerController.damage * holyWaveDamagePercent;
        holyShotDamage = playerController.damage * holyShotDamagePercent;
        poisonDamage = playerController.damage * poisonDamagePercent;
        rockDamage = playerController.damage * rockDamagePercent;

        // 갯수
        fireShotSubNum = 3;
        meleeNum = 2;

        // 시간
        fireDuration = 3f;
        holyShotDuration = 2f;
        holyWaveDuration = 4f;
        posionDuration = 5f;
        sturnDuration = 3f;

        // 확률      
        if (character.currentCharacter == 4)
        {
            firePercent = 20f;
            fireShotPercent = 30f;
            holyShotPercent = 20f;
            holyWavePercent = 15f;
            rockPercent = 40f;
            posionPercent = 20f;
            meleePercent = 70f;
            sturnPercent = 40f;
        }
        else
        {
            firePercent = 10f;
            fireShotPercent = 20f;
            holyShotPercent = 10f;
            holyWavePercent = 5f;
            rockPercent = 30f;
            posionPercent = 10f;
            meleePercent = 60f;
            sturnPercent = 30f;
        }
    }

    public void ItemValueUp()
    {
        // fire
        fireDamagePercent += 0.1f;
        fireDuration += 0.5f;

        // fireShot
        fireShotDamagePercent += 0.2f;
        fireShotSubDamagePercent += 0.2f;
        fireShotSubNum++;

        // holyWave
        holyWaveDamagePercent += 0.05f;
        holyWaveDuration += 0.5f;

        // holyShot
        holyShotDamagePercent += 0.2f;
        holyShotDuration += 0.5f;

        // rock
        rockDamagePercent += 0.5f;

        // poison
        poisonDamagePercent += 0.05f;

        // melee
        meleeNum++;

        // sturn
        sturnDuration += 1f;
    }

    public void GetItem()
    {
        GameObject fireObj = GameObject.Find("FirePltem");
        GameObject fireShotObj = GameObject.Find("Fire ShotPltem");
        GameObject holyWaveObj = GameObject.Find("Holy WavePltem");
        GameObject holyShotObj = GameObject.Find("Holy ShotPltem");
        GameObject rockObj = GameObject.Find("RockPltem");
        GameObject posionObj = GameObject.Find("PosionPltem");
        GameObject meleeObj = GameObject.Find("MeleePltem");
        GameObject sturnObj = GameObject.Find("SturnPltem");

        if (fireObj != null)
        {
            isFire = true;
        }
        else if (fireShotObj != null)
        {
            isFireShot = true;
        }
        else if (holyWaveObj != null)
        {
            isHolyWave = true;
        }
        else if (holyShotObj != null)
        {
            isHolyShot = true;
        }
        else if (rockObj != null)
        {
            isRock = true;
        }
        else if (posionObj != null)
        {
            isPosion = true;
        }
        else if (meleeObj != null)
        {
            isMelee = true;
        }
        else if (sturnObj != null)
        {
            isSturn = true;
        }
    }

    // fire --------------------------------
    public void Fire(Vector3 targetPosition)
    {
        if(isFire)
        {
            audioManager.FireAudio();

            Vector3 firePos = new Vector3(targetPosition.x, targetPosition.y, targetPosition.z + 3f);
            fireInstance = Instantiate(fireEffect, firePos, Quaternion.Euler(-90, 0, 0));
            fireInstance.name = "PlayerSkill";

            Destroy(fireInstance, 3f);
        }      
    }

    // fireShot --------------------------------
    public void FireShot(Vector3 targetPosition)
    {
        if (isFireShot)
        {
            audioManager.FireShotAudio();

            GameObject fireShotInstance = Instantiate(fireShotEffect, targetPosition, Quaternion.identity);

            List<GameObject> sub = new List<GameObject>();

            for (int i = 0; i < fireShotSubNum; i++)
            {
                GameObject subShot = Instantiate(fireShotSub, targetPosition, Quaternion.identity);
                subShot.name = "PlayerSkill";
                Vector2 randomDirection = Random.insideUnitCircle.normalized;
                subShot.GetComponent<Rigidbody2D>().velocity = randomDirection * 5f;

                sub.Add(subShot);
            }

            StartCoroutine(DestroySubShots(sub, 3f));

            Destroy(fireShotInstance, 1f);
        }
    } 

    private IEnumerator DestroySubShots(List<GameObject> subShots, float delay)
    {
        yield return new WaitForSeconds(delay);

        foreach (GameObject subShot in subShots)
        {
            Destroy(subShot);
        }
    }

    // holyWave --------------------------------

    public void HolyWave()
    {
        if(isHolyWave)
        {
            audioManager.HolyWaveAudio();

            WaveInstance = Instantiate(holyWaveEffect, holyWavePos.position, Quaternion.identity);
            WaveInstance.name = "PlayerSkill";
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

    public void HolyShot(Vector3 targetPosition)
    {
        if (isHolyShot)
        {
            audioManager.HolyShotAudio();

            GameObject holyShotInstance = Instantiate(holyShotEffect, targetPosition, Quaternion.identity);
            holyShotInstance.name = "PlayerSkill";

            if (holyShotInstance != null) 
            {
                StartCoroutine(RotateHolyShot(holyShotInstance, 5f));       
            }
        }       
    }

    private IEnumerator RotateHolyShot(GameObject holyShotInstance, float duration)
    {
        if (holyShotInstance == null)
        {
            yield break;
        }

        float elapsedTime = 0f;
        float rotationSpd = 360f / duration;

        while (elapsedTime < duration)
        {
            if (holyShotInstance == null)
            {
                yield break;
            }

            holyShotInstance.transform.Rotate(rotationSpd * Time.deltaTime, 0f, 0f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }



    // melee --------------------------------
    public void Melee(Vector3 targetPosition, int numEffects)
    {
        if (isMelee)
        {
            StartCoroutine(MeleeInstantiate(targetPosition, numEffects));
        }     
    }

    IEnumerator MeleeInstantiate(Vector3 targetPosition, int numEffects)
    {
        for (int i = 0; i < numEffects; i++)
        {
            audioManager.MeleeAudio();

            Vector3 randomOffset = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
            Vector3 spawnPosition = targetPosition + randomOffset;

            GameObject meleeInstance = Instantiate(meleeEffect, spawnPosition, Quaternion.identity);
            meleeInstance.name = "PlayerSkill";
            Destroy(meleeInstance, 0.5f);

            yield return new WaitForSeconds(0.1f);
        }
    }

    // posion --------------------------------
    public void Posion(Vector3 targetPosition)
    {
        if(isPosion)
        {
            audioManager.PosionAudio();

            GameObject posionInstance = Instantiate(posionEffect, targetPosition, Quaternion.identity);

            Destroy(posionInstance, 5f);
        }       
    }

    // rock --------------------------------
    public void Rock(Vector3 targetPosition)
    {
        if(isRock)
        {
            audioManager.RockAudio();

            GameObject rockInstance = Instantiate(rockEffect, targetPosition, Quaternion.identity);

            Destroy(rockInstance, 5f);
        }       
    }

    // sturn --------------------------------
    private Dictionary<GameObject, GameObject> monsterToSturnImage = new Dictionary<GameObject, GameObject>();

    public void Sturn()
    {
        if (isSturn)
        {
            audioManager.SturnAudio();

            if (currentAttackedMonster != null)
            {
                MonsterController monsterController = currentAttackedMonster.GetComponent<MonsterController>();
                if (monsterController != null)
                {
                    GameObject sturnInstance = Instantiate(sturnEffect, currentAttackedMonster.transform.position, Quaternion.identity);
                    Vector3 imagePos = new Vector3(monsterController.sturn.transform.position.x, monsterController.sturn.transform.position.y, monsterController.sturn.transform.position.z - 0.5f);
                    GameObject sturnimageInstance = Instantiate(sturnImage, monsterController.sturn.transform.position, Quaternion.identity);
                    sturnimageInstance.name = "PlayerSkill";


                    monsterController.stop = true;
                    monsterController.attackTime += 5;

                    monsterToSturnImage[currentAttackedMonster] = sturnimageInstance;

                    Destroy(sturnimageInstance, 2f);
                }
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

    public void SetCurrentAttackedMonster(GameObject monster)
    {
        currentAttackedMonster = monster;
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
}
