using UnityEngine;

public class PoolAttack : MonoBehaviour
{
      public float damagePerSecond;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IDamageable dmg = other.GetComponent<IDamageable>();
            if (dmg != null)
            {
                dmg.TakeDamage(damagePerSecond * Time.deltaTime);
            }
        }
    }
}
