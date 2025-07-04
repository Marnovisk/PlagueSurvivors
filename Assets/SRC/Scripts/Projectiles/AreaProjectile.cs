using UnityEngine;

public class AreaProjectile : Projectile, ITargetWeapon
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
        this.transform.position = targetPos + new Vector3(0,-1,0);
        isReady = true;

        Destroy(this.gameObject, 10f);
    }

    public override void Update()
    {
        if(isReady == false) return;
        base.Update();
    }

    void OnTriggerEnter(Collider other)
    {
         if (other.gameObject.tag != "Enemy") return;

        other.gameObject.GetComponent<IDamagable>().TakeDamage(brain.Damage);

        
    }
}
