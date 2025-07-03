using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/WeaponData", order = 0)]
public class WeaponScriptable : ScriptableObject
{
    public WeaponType Type;
    public int Damage;
    public float cooldown;
    public float Range;
    public float Speed;
    public GameObject Prefab;
}
