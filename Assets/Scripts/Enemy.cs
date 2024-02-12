using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    public int attackDamage = 20;
    public float movementSpeed = 5f;

    [SerializeField]private GameObject player;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // ×·»÷Íæ¼Ò
        if (player != null)
        {
            Vector3 direction = player.transform.position - transform.position;
            transform.Translate(direction.normalized * movementSpeed * Time.deltaTime);
        }
    }
}
