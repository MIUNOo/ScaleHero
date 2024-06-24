using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public EnemyScriptableObject enemy;

    private int currentHealth;
    private int currentAttackDamage;
    private float currentMovementSpeed;

    [SerializeField]private Transform playerPosition;



    // Start is called before the first frame update
    void Awake()
    {
        currentHealth = enemy.health;
        currentAttackDamage = enemy.attackDamage;
        currentMovementSpeed = enemy.moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        // ×·»÷Íæ¼Ò
        if (playerPosition != null)
        {
            Vector3 direction = playerPosition.position - transform.position;
            transform.Translate(direction.normalized * currentMovementSpeed * Time.deltaTime);
        }
    }
}
