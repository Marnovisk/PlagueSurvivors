using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ContinuosConstantProjectile : Projectile, ITargetWeapon
{
   public Transform target;
   public Vector3 InitialPosition;
   public Vector3 SecondPosition;
   public Vector3 ThirdPosition;
   public Vector3 FinalPosition;

   public Vector3 posToGo;

    public override void Awake()
    {
        base.Awake();
    }


    public override void Init(Transform ptarget)
    {
        base.Init();
        target = ptarget;
        InitialPosition = new Vector3(0,0,brain.Range);
        SecondPosition = new Vector3(brain.Range,0,0);
        ThirdPosition = new Vector3(0,0,-brain.Range);
        FinalPosition = new Vector3(-brain.Range,0,0);
        this.transform.position = InitialPosition + target.position;
        posToGo = SecondPosition;
        isReady = true;
        

        Destroy(this.gameObject, 10f);
    }

    // Update is called once per frame
    public override void Update()
    {
        //MoveAround();
        transform.position = target.position;
        base.Update();
        
    }

    public void MoveAround()
    {
        transform.position = Vector3.MoveTowards(transform.position, posToGo + target.position, Time.deltaTime * brain.projectileSpeed);
        //Quaternion.identity;
        if(transform.position == InitialPosition + target.position)
        {
            posToGo = SecondPosition;
        }
        if(transform.position == SecondPosition + target.position)
        {
            posToGo = ThirdPosition;
        }
        if(transform.position == ThirdPosition + target.position)
        {
            posToGo = FinalPosition;
        }
        if(transform.position == FinalPosition + target.position)
        {
            posToGo = InitialPosition;
        }
        
    }

    public void MoveToSecond()
    {
        transform.position = Vector3.MoveTowards(transform.position, SecondPosition, Time.deltaTime * brain.projectileSpeed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Enemy") return;

        if(other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<IDamagable>().TakeDamage(brain.Damage);

            //Destroy(this.gameObject);
        }
        
    }
}