using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "ScriptableObjects/UpgradeData", order = 2)]
public class Upgradecriptable : ScriptableObject
{
   [Header("Data")]
    public float value; 
    public UpgradeType upgradeType;
    private WeaponScriptable weapon;
    public WeaponScriptable upgradedWeapon;

    [Header("Info Data")]
    public string description;
    public WeaponType _upgradeWeapon;
    public void ApplyUpgrade(GameObject player)
    {
        var playerAttacks = player.GetComponent<PlayerWeapons>();        
        switch(_upgradeWeapon)
        {
            case WeaponType.ContinuousConstant:
                weapon = playerAttacks.Weapons[0];
                break;
            case WeaponType.ConstinuousSingle:
                weapon = playerAttacks.Weapons[1];
                break;
            case WeaponType.NeedTarget:
                weapon = playerAttacks.Weapons[2];
                break;    
        }

        switch (upgradeType)
        {
            case UpgradeType.IncreaseDamage:
                weapon.Damage += (int)value;
                break;
            case UpgradeType.IncreaseRange:
                weapon.Range += value;
                break;
            case UpgradeType.IncreaseAttackSpeed:
                weapon.AttackSpeed -= value;
                break;
            case UpgradeType.NewWeapon:
                playerAttacks.AddWeapon(upgradedWeapon);
                break;
        }
    }
}
