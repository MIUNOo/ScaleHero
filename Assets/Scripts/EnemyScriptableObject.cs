using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Enemy", menuName = "SciptableObject/ScriptableEnemy")]
public class EnemyScriptableObject : ScriptableObject
{
   // public new string name;
    public int health;
    public int attackDamage;
    public float moveSpeed;
}
