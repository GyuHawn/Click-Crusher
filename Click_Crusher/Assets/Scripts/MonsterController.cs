/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    private StageManager stagerManager;
    private PlayerController playerController;

    public float maxHealth;
    public float currentHealth;

    void Start()
    {
        stagerManager = GameObject.Find("Manager").GetComponent<StageManager>();
        playerController = GameObject.Find("Manager").GetComponent<PlayerController>();

        maxHealth = currentHealth;
    }

    void Update()
    {
        if(currentHealth <= 0)
        {
            Die();
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(touch.position);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            if (hit.collider != null)
            {
                currentHealth -= playerController.damage;
            }
        }
    }

    public void Die()
    {
        stagerManager.monsterCount--;
        stagerManager.NextStage();

        Destroy(gameObject);
    }
}
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    private StageManager stagerManager;
    private PlayerController playerController;

    public float maxHealth;
    public float currentHealth;

    void Start()
    {
        stagerManager = GameObject.Find("Manager").GetComponent<StageManager>();
        playerController = GameObject.Find("Manager").GetComponent<PlayerController>();

        currentHealth = maxHealth;
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            Die();
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                currentHealth -= playerController.damage;
            }
        }
    }

    public void Die()
    {
        stagerManager.monsterCount--;
        stagerManager.NextStage();

        Destroy(gameObject);
    }
}
