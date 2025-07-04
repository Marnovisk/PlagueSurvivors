using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.AI;

public class IAKombat : MonoBehaviour
{
    [Header("MainData")]
    private EnemyScriptable brain;

    [Header("Attack Info")]
    [SerializeField] private bool canAttack;
    [SerializeField] private float currentAttackCooldown;

    private NavMeshAgent nav;

    public void Init(EnemyScriptable pBrain)
    {
        brain = pBrain;
        nav = GetComponent<NavMeshAgent>();
        nav.stoppingDistance = brain.AttackRange;
    }
   public bool checkAndAttack(Transform target)
    {
        CooldownRecovery();
        if(Vector3.Distance(this.transform.position, target.transform.position) <= brain.AttackRange){
            
             if(canAttack)
            {
                Attack(target);
            }

            return true;

        } 

        return false;

    }

    public void CooldownRecovery()
    {
        currentAttackCooldown -= Time.deltaTime;
                if (currentAttackCooldown <= 0)
                {
                    canAttack = true;
                    currentAttackCooldown = brain.AttackSpeed;
                }
    }

    void Attack(Transform target)
    {
        canAttack = false;
        if (brain.AttackKind == AttackKind.Range)
        {
            var projectile = Instantiate(brain.Bullet, transform.position, Quaternion.identity);
            projectile.GetComponent<ITargetWeapon>().Init(target.transform);
        }
        else
        {
            target.GetComponent<IDamagable>().TakeDamage(Random.Range(brain.AttackDamage[0],brain.AttackDamage[1]));
        }
                
    }
}
