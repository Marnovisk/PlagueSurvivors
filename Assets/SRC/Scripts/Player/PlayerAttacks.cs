using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAttacks : MonoBehaviour
{
    public LayerMask EnemiesLayer;
    public List<WeaponXScriptable> Bullets;
    public List<WeaponXScriptable> _AllBullets;
    public GameObject GunHole;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        InvokeRepeating("GetTarget", 0, 1);
    }

    public void AddWeapon()
    {
        

    }


    public void GetTarget()
    {
        var nearbyEnemies = Physics.OverlapSphere(transform.position, 100.0f, EnemiesLayer);


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

            foreach (WeaponXScriptable weapon in Bullets)
            {
                StartCoroutine(UseGun(weapon, closestTarget));
            }           

        }
    }

    public IEnumerator UseGun(WeaponXScriptable Wweapon, Collider WclosestTarget)
    {
        var projectile = Instantiate(Wweapon.Prefab, GunHole.transform.position, Quaternion.identity);
        projectile.GetComponent<Weapon>().GetTarget(WclosestTarget.transform, Wweapon);

        yield return new WaitForSeconds(Wweapon.cooldown);
    }
}
