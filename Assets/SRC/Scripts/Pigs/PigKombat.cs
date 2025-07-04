using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class PigKombat : MonoBehaviour
{
    public PigScriptable brain;
    public WeaponType _weaponType;
    public WeaponScriptable Weapon;
    public GameObject gunPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Weapon = brain.Bullet;
        _weaponType = Weapon.type;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UseWeapon()
    {
        switch(_weaponType)
        {
            case WeaponType.ContinuousConstant:
                UseContinousConstantWeapon();
                break;
            //case WeaponType.ConstinuousSingle:
            //    _weaponType = playerAttacks.Weapons[1];
            //    break;
            //case WeaponType.NeedTarget:
            //    _weaponType = playerAttacks.Weapons[2];
            //    break;
            //case WeaponType.PlayerPosition:

        }
    }

    public void UseContinousConstantWeapon()
    {
        if (!Weapon) return;  

        var projectile = Instantiate(Weapon.proj, gunPosition.transform.position, Quaternion.LookRotation(this.transform.forward, this.transform.up));
        projectile.GetComponent<ITargetWeapon>().Init(this.transform);

    }

}