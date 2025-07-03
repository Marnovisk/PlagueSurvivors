using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    public float speed;
    public float damage;
    public float lifeTime;

    private Vector3 direction;

    void Start()
    {
        // Força a rotação do projétil no momento em que ele aparece
        transform.rotation = Quaternion.Euler(-90f, transform.rotation.eulerAngles.y, -180f);

        // Define a direção com base na nova rotação
        direction = transform.forward;

        // Destroi o projétil após o tempo de vida
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // Move o projétil na direção definida
        transform.position += direction * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Props"))
        {
            Destroy(gameObject);
        }

        if (other.CompareTag("Player"))
        {
            PlayerHealth player = other.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
    }
}
