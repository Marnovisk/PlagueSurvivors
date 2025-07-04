using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : Projectile, ITargetWeapon
{
    public Transform target;
    private Vector3 targetPosition;
    public float speed = 15f;

    // Start is called before the first frame update
    public override void Init(Transform ptarget)
    {
        base.Init();
        target = ptarget;
        isReady = true;
        

        Destroy(this.gameObject, 2f);
    }

    public void SetTargetPosition(Vector3 pos)
    {
        targetPosition = pos;
        isReady = true;
    }

    public override void Update()
    {
        base.Update();
        
        if (!isReady) return;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player") return;

        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<IDamagable>().TakeDamage(DamageValue);
        }
        
    }
}
