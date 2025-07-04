using UnityEngine;
using System.Collections.Generic;

public class PlayerWeapons : MonoBehaviour
{
   public  List <WeaponScriptable> Weapons;
   public List <WeaponScriptable> AllWeapons;
   public LayerMask EnemiesLayer;
   public GameObject segmentPrefab;
   public playerPigSpawner pigSpawner;
   public GameObject gunPosition;


    public void Start()
    {
        pigSpawner = GetComponent<playerPigSpawner>();
        InvokeRepeating("UseTargetWeapon", 0f, 1f);
        InvokeRepeating("UsePlayerPositionWeapon", 0f, 15f);
        InvokeRepeating("UseContinousSingleWeapon", 0f, 1f);
        InvokeRepeating("UseContinousConstantWeapon", 0f, 15f);
    }

   public void AddWeapon(WeaponScriptable _upgraded)
   {
        foreach (var weapon in AllWeapons)
        {
            if(_upgraded == weapon)
                Weapons.Add(weapon);
        }
        //Weapons.Add(AllWeapons[Random.Range(0, AllWeapons.Count)]);
        pigSpawner.AddSegment(segmentPrefab);
        for(int i = 0; i < Weapons.Count; i++)
        {
            WeaponScriptable weapon = Weapons[i];
            for(int j = 0; j < AllWeapons.Count; j++)
            {
                if(weapon == AllWeapons[j])
                {
                    AllWeapons.Remove(weapon);
                } 
            } 
        } 
            
              
   }

   public void UpgradeWeapon(WeaponScriptable weapon)
   {
        
   }

   public void UpgradePrimaryWeapon()
   {
        Weapons[0].Damage += 5;
   }

   public void UpgradeSecondaryWeapon()
   {
        Weapons[1].Damage += 5;
   }

   public void UseWeapon(WeaponScriptable weapon)
   {

   }

   

   public void UseTargetWeapon()
   {
        if (Weapons.Count <= 0) return;

        foreach(WeaponScriptable weapon in Weapons)
        {
            if (weapon.type != WeaponType.NeedTarget) continue;

            var nearbyEnemies = Physics.OverlapSphere(transform.position, weapon.Range, EnemiesLayer);
            
            if(nearbyEnemies.Length <= 0) continue;

            var closestTarget = nearbyEnemies[0];
            var closestDistance = 999999f;

            foreach (Collider enemy in nearbyEnemies)
            {
                var distance = Vector3.Distance(transform.position, enemy.transform.position);

                if(distance < closestDistance)
                {
                    closestTarget = enemy;
                    closestDistance = distance;
                }
            }

            var projectile = Instantiate(weapon.proj, gunPosition.transform.position, Quaternion.identity);
            projectile.GetComponent<ITargetWeapon>().Init(closestTarget.transform);

        }
   }

   public void UsePlayerPositionWeapon()
   {
        if (Weapons.Count <= 0) return;

        foreach(WeaponScriptable weapon in Weapons)
        {
            if (weapon.type != WeaponType.PlayerPosition) continue;

            var projectile = Instantiate(weapon.proj, gunPosition.transform.position, Quaternion.identity);
            projectile.GetComponent<ITargetWeapon>().Init(this.transform);

        }
   }

   public void UseContinousSingleWeapon()
   {
        if (Weapons.Count <= 0) return;

        foreach(WeaponScriptable weapon in Weapons)
        {
            if (weapon.type != WeaponType.ConstinuousSingle) continue;

            var projectile = Instantiate(weapon.proj, gunPosition.transform.position, Quaternion.LookRotation(this.transform.forward, this.transform.up));
            projectile.GetComponent<ITargetWeapon>().Init(this.transform);

        }
   }

   public void UseContinousConstantWeapon()
   {
        if (Weapons.Count <= 0) return;

        foreach(WeaponScriptable weapon in Weapons)
        {
            if (weapon.type != WeaponType.ContinuousConstant) continue;

            var projectile = Instantiate(weapon.proj, gunPosition.transform.position, Quaternion.LookRotation(this.transform.forward, this.transform.up));
            projectile.GetComponent<ITargetWeapon>().Init(this.transform);

        }
   }
}
