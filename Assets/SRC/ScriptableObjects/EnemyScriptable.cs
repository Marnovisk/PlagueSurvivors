using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/EnemyData", order = 0)]
public class EnemyScriptable : ScriptableObject
{
   [Header("Data")]
   public Status Status;
   public float speed;
   public GameObject Bullet;

   [Header("Kombat Data")]
    public float AttackRange;
    public float AttackSpeed;
    public int[] AttackDamage;
   public AttackKind AttackKind;
    

   [Header("Data")]
   public GameObject GFX;



}
