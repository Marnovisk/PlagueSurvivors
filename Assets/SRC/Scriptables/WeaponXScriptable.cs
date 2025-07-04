using UnityEngine;

[CreateAssetMenu(fileName = "WeaponX", menuName = "ScriptableObjects/WeaponXData", order = 5)]
public class WeaponXScriptable : ScriptableObject
{
    public WeaponTypeX Type;
    public int Damage;
    public float cooldown;
    public float Range;
    public float Speed;
    public GameObject Prefab;
}
