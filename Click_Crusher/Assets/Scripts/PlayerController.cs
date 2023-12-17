using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;

    public float damage;

    public GameObject gameover;

    void Start()
    {
        currentHealth = maxHealth;
    }

    
    void Update()
    {
        if(currentHealth <= 0)
        {
            gameover.SetActive(true);
        }
    }
}
