using UnityEngine;

public class Projectile : MonoBehaviour
{
    public WeaponScriptable brain;

    public int DamageValue;

    public bool isReady = false;

    public virtual void Awake() { }

    public virtual void Init(Transform target = null) { }

    public virtual void Update() { }

    public virtual void upgradeDamage(int DMGValue)
    {
        DamageValue = DMGValue;
    }
}
