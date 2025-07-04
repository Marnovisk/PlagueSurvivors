using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinuosSingleProjectile : Projectile, ITargetWeapon
{
   public Transform target;

    public override void Awake()
    {
        base.Awake();
    }


    public override void Init(Transform ptarget)
    {
        base.Init();
        //target = ptarget;
        isReady = true;
        

        Destroy(this.gameObject, 10f);
    }

    // Update is called once per frame
    public override void Update()
    {
        MoveForward();
        base.Update();
        
    }

    public void MoveForward()
    {
        //Quaternion.identity;
        transform.position += transform.forward * Time.deltaTime * brain.projectileSpeed;
        //(transform.position, target.position, Time.deltaTime * brain.projectileSpeed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Enemy") return;

        if(other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<IDamagable>().TakeDamage(brain.Damage);

            Destroy(this.gameObject);
        }
        
    }
}
