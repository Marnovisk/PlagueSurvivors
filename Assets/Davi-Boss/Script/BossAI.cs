using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class BossAI : MonoBehaviour, IDamageable
{

    private bool isFlashing = false; // flag para evitar conflito de cores no FlashRed

    private Renderer[] renderers;
    private Color[] originalColors;
    private Transform player;
    private NavMeshAgent agent;
    private Rigidbody rb;
    public Animator animator;

    [Header("Audios")]
    public AudioSource AttackRange; // som do ataque
    public AudioSource AttackRain; // som do ataque

    public AudioSource AttackPool; // som do ataque


    [Header("Effects")]
    public GameObject deathSmokePrefab;
    public GameObject hitSmokePrefab;

    [Header("Status do Boss")]
    public float health = 500f;
    public float enragedThreshold = 200f;
    private bool isEnraged = false;
    public float attackDelay = 2f; // Tempo para esperar após a animação começar
    private bool isAttacking = false;

    public int xpValue = 100;
    public float movementSpeed = 3f;

    [Header("Ataques")]
    public float detectionRange = 60f;
    public float attackRange = 10f;
    public float fireRate = 2f;
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 20f;

    [Header("Air Attack")]
    public GameObject AirPrefab;
    public int AirCount = 8;
    public float AirRadius = 5f;
    public float airCooldown = 10f;
    private float airTimer;
    public float height = 20f;

    [Header("Warning Prefab")]
    public GameObject warningPrefab;  // Prefab do aviso visual no chão
    public float warningDuration = 2f; // Tempo que o aviso fica antes do ataque cair

    [Header("Explosion Prefab")]
    public GameObject explosionEffect;
    public float explosionRadius = 6f;
    public float explosionDamage = 30f;
    private bool explosionUsed = false;

    [Header("Pool Attack")]
    public GameObject PoolPrefab;
    public int PoolCount = 5;
    public float PoolRadius = 10f; // raio ao redor do jogador onde as poças podem aparecer
    public float PoolDuration = 5f;
    public float PoolDamagePerSecond = 10f;
    public float PoolCooldown = 5f;
    private float PoolTimer;

    [Header("Knockback")]
    public float knockbackForce = 8f;
    public float knockbackDuration = 1.2f;

    private float fireCooldown;
    private bool isKnockedBack = false;
    private float knockbackTimer;
    private Renderer bossRenderer;
    private Color originalColor;

    [Header("Congelamento")]
    public float freezeDuration = 3f;
    private bool isFrozen = false;
    private float freezeTimer;
    private Color frozenColor = Color.cyan;


    void Start()
    {
        PoolTimer = PoolCooldown;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        agent.speed = movementSpeed;
        fireCooldown = 0f;
        airTimer = airCooldown;

        bossRenderer = GetComponentInChildren<Renderer>();
        if (bossRenderer != null)
            originalColor = bossRenderer.material.color;

        renderers = GetComponentsInChildren<Renderer>();
            
            // Armazena todas as cores de todos os materiais de todos os renderers
        var colorList = new List<Color>();
        foreach (Renderer rend in renderers)
        {
            foreach (Material mat in rend.materials)
            {
                colorList.Add(mat.color);
            }
        }
        originalColors = colorList.ToArray();
    }


    void Update()
    {

        if (isAttacking == true)
        {
            movementSpeed = 0f;
        }

        else movementSpeed = 3f;


        if (player == null) return;

        if (isFrozen)
        {
            freezeTimer -= Time.deltaTime;
            if (freezeTimer <= 0f)
            {
                isFrozen = false;
                agent.isStopped = false;

                if (bossRenderer != null)
                    bossRenderer.material.color = originalColor;
            }
            return; // cancela ações enquanto congelado
        }


        if (isKnockedBack)
        {
            knockbackTimer -= Time.deltaTime;
            if (knockbackTimer <= 0f)
            {
                isKnockedBack = false;
                agent.enabled = true;
                if (bossRenderer) bossRenderer.material.color = originalColor;
            }
            return;
        }

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= detectionRange)
        {
            if (distance > attackRange)
            {
                agent.isStopped = false;
                agent.SetDestination(player.position);
                animator.SetBool("isWalking", true);
            }
            else
            {
                agent.isStopped = true;
                LookAtPlayer();
                AttackPlayer();
                animator.SetBool("isWalking", false);
            }
        }

        // Chuva de fogo (meteor shower) com aviso
        airTimer -= Time.deltaTime;
        if (airTimer <= 0f)
        {
            CastMeteorShower();
            airTimer = airCooldown;
        }

        fireCooldown -= Time.deltaTime;

        // Explosão flamejante
        if (!explosionUsed && health <= enragedThreshold)
        {
            CastExplosion();
            explosionUsed = true;
            isEnraged = true;
            agent.speed = movementSpeed + 1.5f; // Fica mais rápido
        }

        PoolTimer -= Time.deltaTime;
        if (PoolTimer <= 0f)
        {
            CastPools();
            PoolTimer = PoolCooldown;
        }
    }

    // Modificado para usar aviso antes de spawnar a pool
    void CastPools()
    {
        animator.SetTrigger("Attack 03"); //Posso alterar isso aqui depois do aviso.
        for (int i = 0; i < PoolCount; i++)
        {
            isAttacking = true;
            Vector2 randomCircle = Random.insideUnitCircle * PoolRadius;
            Vector3 warningPos = player.position + new Vector3(randomCircle.x, 0f, randomCircle.y);
            StartCoroutine(SpawnPoolWithWarning(warningPos));
            isAttacking = false;
        }
    }

    IEnumerator SpawnPoolWithWarning(Vector3 position)
    {
        // Instancia o aviso no chão
        Vector3 warningPos = position + Vector3.up * 0f;
        GameObject warning = Instantiate(warningPrefab, warningPos, Quaternion.identity);
        // Espera o tempo do aviso
        yield return new WaitForSeconds(warningDuration);

        // Remove o aviso
        Destroy(warning);

        // Instancia a poça na altura 0.5
        Vector3 poolPos = position + new Vector3(0f, 0f, 0f);
        float randomYRotation = Random.Range(0f, 360f);
        Quaternion randomRotation = Quaternion.Euler(0f, randomYRotation, 0f);
        if (AttackPool != null && AttackPool.clip != null)
            AttackPool.PlayOneShot(AttackPool.clip);

        GameObject pool = Instantiate(PoolPrefab, poolPos, randomRotation);

        // Destrói a poça após duração
        Destroy(pool, PoolDuration);
    }

    IEnumerator SpawnWithWarning(Vector3 position)
    {
        // Instancia o aviso no chão
       Vector3 warningPos = position + Vector3.up * 1.5f;
        GameObject warning = Instantiate(warningPrefab, warningPos, Quaternion.identity);

        // Espera o tempo do aviso
        yield return new WaitForSeconds(warningDuration);

        // Remove o aviso
        Destroy(warning);

        // Instancia o prefab do ataque vindo do céu (posição com altura)
        Vector3 spawnPosWithHeight = position + Vector3.up * height;
        Instantiate(AirPrefab, spawnPosWithHeight, Quaternion.identity);
    }

    void CastMeteorShower()
    {
        for (int i = 0; i < AirCount; i++)
        {
            isAttacking = true;
            animator.SetTrigger("Attack 02");
            Vector2 offset2D = Random.insideUnitCircle * AirRadius;
            Vector3 spawnPos = player.position + new Vector3(offset2D.x, 0, offset2D.y); // posição no chão
            if (AttackRain != null && AttackRain.clip != null)
            AttackRain.PlayOneShot(AttackRain.clip);

            StartCoroutine(SpawnWithWarning(spawnPos));
             isAttacking = false;
        }
    }

    public void Freeze()
    {
        if (isFrozen) return;

        isFrozen = true;
        freezeTimer = freezeDuration;
        agent.isStopped = true;

        if (rb != null)
            rb.linearVelocity = Vector3.zero;

        if (bossRenderer != null)
            bossRenderer.material.color = frozenColor;
    }

    void LookAtPlayer()
    {
        Vector3 dir = (player.position - transform.position).normalized;
        dir.y = 0;
        Quaternion rot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * 10f);
    }

    void AttackPlayer()
    {
        if (fireCooldown <= 0f && !isAttacking)
        {
             StartCoroutine(AttackWithDelay());
            
        }
    }

    private IEnumerator AttackWithDelay()
    {
        isAttacking = true;
        animator.SetTrigger("Attack 01");
        
       

        yield return new WaitForSeconds(attackDelay); // espera a animação iniciar


        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody rbProj = projectile.GetComponent<Rigidbody>();
         if (AttackRange != null && AttackRange.clip != null)
            AttackRange.PlayOneShot(AttackRange.clip);
        if (rbProj != null)
            rbProj.linearVelocity = firePoint.forward * projectileSpeed;

        fireCooldown = fireRate;
        isAttacking = false;
    }

    void CastExplosion()
    {
        if (explosionEffect != null)
            Instantiate(explosionEffect, transform.position, Quaternion.identity);

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (var hit in hitColliders)
        {
            if (hit.CompareTag("Player"))
            {
                IDamageable dmg = hit.GetComponent<IDamageable>();
                if (dmg != null)
                    dmg.TakeDamage(explosionDamage);
            }
        }
    }

    public void TakeDamage(float damage)
    {

        FlashRed();
         if (hitSmokePrefab != null)
        {
            Instantiate(hitSmokePrefab, transform.position, Quaternion.identity);
        }
        if (health <= 0f) return; // Evita múltiplas chamadas

        if (health >= 0f)animator.SetTrigger("Hurt");

        health -= damage;

        if (health <= 0f)
        {
           // Desativa IA e movimentação
        if (agent != null)
        {
            agent.isStopped = true;
        }

        // Toca animação final (pode ser Hurt ou criar um novo trigger "Die")
        animator.SetTrigger("Die"); // ou "Die" se tiver outra animação

        // Evita que ele cause mais dano ou receba XP múltiplas vezes
        this.enabled = false;

        // Executa destruição com atraso
        StartCoroutine(DestroyAfterAnimation());
        }
        else
        {
            ApplyKnockback();
        }
    }
private System.Collections.IEnumerator DestroyAfterAnimation()
{
     if (deathSmokePrefab != null)
    {
        Instantiate(deathSmokePrefab, transform.position, Quaternion.identity);
    }
    // Aguarda o tempo da animação (ajuste se necessário)
        yield return new WaitForSeconds(3f);


 //   FindObjectOfType<UIManager>()?.AddScore(1);

     

    Destroy(gameObject);
}

void FlashRed()
{
    if (isFlashing || isFrozen) return;

    isFlashing = true;

    foreach (Renderer rend in renderers)
    {
        foreach (Material mat in rend.materials)
        {
            mat.color = Color.red;
        }
    }

    CancelInvoke(nameof(RestoreToBaseColor));
    Invoke(nameof(RestoreToBaseColor), 0.2f);
}

void RestoreToBaseColor()
{
    int colorIndex = 0;

    foreach (Renderer rend in renderers)
    {
        for (int i = 0; i < rend.materials.Length; i++)
        {
            if (colorIndex < originalColors.Length)
            {
                rend.materials[i].color = originalColors[colorIndex];
                colorIndex++;
            }
        }
    }

    isFlashing = false;
}



    void ApplyKnockback()
    {
        isKnockedBack = true;
        knockbackTimer = knockbackDuration;
        agent.enabled = false;

        if (rb != null)
            rb.linearVelocity = -transform.forward * knockbackForce;

        if (bossRenderer != null)
            bossRenderer.material.color = Color.red;
    }
}
