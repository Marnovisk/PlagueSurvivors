using UnityEngine;

public class OnionProjectile : Projectile, ITargetWeapon
{

    public Transform target;
    // Start is called before the first frame update
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
        base.Update();
        //transform.position += targetDir * Time.deltaTime * brain.projectileSpeed;

        transform.position = target.position;
    }

    void OnTriggerEnter(Collider other)
    {
         if (other.gameObject.tag != "Enemy") return;

        other.gameObject.GetComponent<IDamagable>().TakeDamage(brain.Damage);

    }
}