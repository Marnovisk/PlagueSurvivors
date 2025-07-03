using System.Collections.Generic;
using UnityEngine;


public class PlayerAttacks : MonoBehaviour
{
    public LayerMask EnemiesLayer;
    public List<WeaponScriptable> Bullets;
    public GameObject GunHole;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("GetClosestTarget", 0, 0);
    }

    public void AddWeapon()
    {
        
    }

    void UseGun(Collider closestTarget)
    {       

        var projectile = Instantiate(Bullets[0].Prefab, GunHole.transform.position, Quaternion.identity);
        projectile.GetComponent<Weapon>().GetTarget(closestTarget.transform);
        
    }

    void GetClosestTarget()
    {
        var nearbyEnemies = Physics.OverlapSphere(transform.position, 5.0f, EnemiesLayer);

        if (!(nearbyEnemies.Length <= 0))
        {
            var closestTarget = nearbyEnemies[0];
            var closestDistance = 999999f;

            foreach (Collider enemy in nearbyEnemies)
            {
                var distance = Vector3.Distance(transform.position, enemy.transform.position);

                if (distance < closestDistance)
                {
                    closestTarget = enemy;
                    closestDistance = distance;
                }
            }

            UseGun(closestTarget);
        }
    }
}
