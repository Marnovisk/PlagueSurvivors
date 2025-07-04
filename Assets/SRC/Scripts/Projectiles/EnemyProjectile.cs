using UnityEngine;

public class EnemyProjectile : Projectile, ITargetWeapon
{
    public Vector3 targetPos;
    public Vector3 targetDir;
    public float projectileSpeed;

    public int Damage;

    public override void Awake()
    {
        base.Awake();
    }

    public override void Init(Transform ptarget)
    {
        base.Init();
        //projectileSpeed = 6f;
        //Damage = 2;
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
        transform.position += targetDir * Time.deltaTime * projectileSpeed;
        transform.LookAt(targetPos);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player") return;

        other.gameObject.GetComponent<IDamagable>().TakeDamage(Damage);
        Destroy(this.gameObject);
    }
}