using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/WeaponData", order = 1)]
public class WeaponScriptable : ScriptableObject
{
    public int Damage;
    public float Range;
    public float AttackSpeed;
    public float projectileSpeed;
    public float currentCooldown;
    
    public GameObject proj;

    public WeaponType type;
}
