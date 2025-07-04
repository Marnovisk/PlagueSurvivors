using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoamingProjectile : Projectile, ITargetWeapon
{
    public Transform target;
    public ParticleSystem explosion;
    public override void Awake()
    {
        base.Awake();
    }

    public override void Init(Transform ptarget)
    {
        base.Init();
        target = ptarget;
        isReady = true;

        Destroy(this.gameObject, 10f);
    }

    public override void Update()
    {
        if(isReady == false) return;
        if(target == null)
        {
            Destroy(this.gameObject);
            return;
        }
        base.Update();
        //transform.position += targetDir * Time.deltaTime * brain.projectileSpeed;

        transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * brain.projectileSpeed);
    }

    void OnTriggerEnter(Collider other)
    {
         if (other.gameObject.tag != "Enemy") return;

        other.gameObject.GetComponent<IDamagable>().TakeDamage(brain.Damage);

        Destroy(this.gameObject);
    }
    private void OnDestroy()
    {
        //Instantiate(explosion, transform.position, Quaternion.identity);
    }
}
