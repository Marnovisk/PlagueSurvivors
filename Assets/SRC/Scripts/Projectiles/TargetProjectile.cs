using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TargetProjectile : Projectile, ITargetWeapon
{

    public Vector3 targetPos;
    public Vector3 targetDir;

    public override void Awake()
    {
        base.Awake();
    }

    public override void Init(Transform ptarget)
    {
        base.Init();
        targetPos = ptarget.position;
        targetDir = (targetPos - transform.position).normalized;
        transform.Rotate(targetDir, Space.World);
        isReady = true;

        Destroy(this.gameObject, 10f);
    }

    public override void Update()
    {
        if(isReady == false) return;
        base.Update();
        transform.position += targetDir * Time.deltaTime * brain.projectileSpeed;
    }

    void OnTriggerEnter(Collider other)
    {
         if (other.gameObject.tag != "Enemy") return;

        other.gameObject.GetComponent<IDamagable>().TakeDamage(brain.Damage);

        Destroy(this.gameObject);
    }
}
