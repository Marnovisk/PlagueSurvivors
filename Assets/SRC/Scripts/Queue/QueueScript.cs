using UnityEngine;

public class QueueScript : MonoBehaviour, ITargetWeapon
{
    public int Damage;

    public void Init(Transform ptarget)
    {
        
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Enemy") return;

        other.gameObject.GetComponent<IDamagable>().TakeDamage(Damage);
        Debug.Log("Queue Damage");

        
    }
}
