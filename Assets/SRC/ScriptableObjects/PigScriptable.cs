using UnityEngine;
[CreateAssetMenu(fileName = "Pig", menuName = "ScriptableObjects/PigData", order = 3)]
public class PigScriptable : ScriptableObject
{
    [Header("Kombat Data")]
    public WeaponScriptable Bullet;

    [Header("Data")]
    public GameObject GFX;
    public float speed;
}
