using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossKombat : MonoBehaviour
{

    [Header("MainData")]
    private EnemyScriptable brain;

    [Header("Attack Info")]
    //[SerializeField] private bool canAttack;
    [SerializeField] private float currentAttackCooldown;
    public WeaponScriptable Weapon;
    private float AttackTime = 0f;

    private NavMeshAgent nav;

    public void Init(EnemyScriptable pBrain)
    {
        brain = pBrain;
        nav = GetComponent<NavMeshAgent>();
    }

    public bool checkAndAttack(Transform target)
    {
        if (target == null) return false;

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance <= brain.AttackRange && Time.time >= AttackTime + brain.AttackSpeed)
        {
            AttackTime = Time.time;
            Attack(target);
            return true;
        }

        return false;
    }

    void Attack(Transform target)
    {
        //canAttack = false;
        Vector3 targetPosition = target.position;
        Vector3 spawnPosition = transform.position + transform.forward * 1.5f + Vector3.up * 1f;

        var projectile = Instantiate(Weapon.proj,spawnPosition,Quaternion.identity);
        projectile.GetComponent<Projectile>().upgradeDamage(Weapon.Damage);

        BossProjectile bossProj = projectile.GetComponent<BossProjectile>();
        if (bossProj != null)
        {
            bossProj.SetTargetPosition(targetPosition);
            bossProj.Init(target);
        }

        Debug.Log("Atacando");
        Destroy(projectile, 2f);      
    }
}
