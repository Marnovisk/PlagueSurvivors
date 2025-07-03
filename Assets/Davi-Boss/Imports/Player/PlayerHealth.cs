using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
        public float maxHealth = 100f;
    private float currentHealth;

    public AudioSource DeathAudio; // som do ataque
   // private UIManager ui;
    //private DamageEffect damageEffect;

    void Start()
    {
        currentHealth = maxHealth;
       // ui = FindObjectOfType<UIManager>();
       // damageEffect = FindObjectOfType<DamageEffect>();
      //  ui.UpdateHealth(currentHealth, maxHealth);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
       // ui.UpdateHealth(currentHealth, maxHealth);

      //  if (damageEffect != null)
      //      damageEffect.Flash();

        if (currentHealth <= 0f)
        {
            if (DeathAudio != null && DeathAudio.clip != null)
            DeathAudio.PlayOneShot(DeathAudio.clip);
            Die();
        }
    }
    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
       // ui.UpdateHealth(currentHealth, maxHealth);
    }

    public void IncreaseMaxHealth(float amount)
    {
        maxHealth += amount;
        currentHealth = maxHealth;  // Opcional: Restaurar saúde máxima ao aumentar
     //   ui.UpdateHealth(currentHealth, maxHealth);
    }

    void Die()
    {
        
        Debug.Log("Player morreu!");
        Destroy(gameObject);
        
        // Aqui você pode reiniciar, mostrar game over, etc.
    }
}
