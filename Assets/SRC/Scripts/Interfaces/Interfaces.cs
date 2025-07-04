using UnityEngine;

public interface IDamagable
    {
        public void TakeDamage(int amount);
    }

    public interface IEnemy
    {

    }

    public interface ITargetWeapon
    {
        public void Init(Transform pTarget);
    }

