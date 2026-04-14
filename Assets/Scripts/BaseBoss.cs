using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class BaseBoss : MonoBehaviour
{
    public NavMeshAgent agent;
    public float maxHealth = 100;
    public GameObject playerObj;
    public Transform projectileSpawnPoint;
    public float currentHealth;
    public bool isAnger;
    public float damage = 10;
    public float timer = 0;
    [Header("Animation/Speed")]
    protected Animator animator;
    public float normalSpeed = 1.0f;
    public float enragedSpeed = 1.5f;
    [Header("DistanceBetweenPlayer")]
    public Vector3 enemyLocation;
    public Vector3 playerTarget;
    public float distanceToPlayer;
    [Header("Attacks Bools")]
    public bool isAttacking;
    public bool rangeAttack;
    public bool speicalAttack;
    public bool isTakenDamage;
    [Header("Cooldowns")]
    public float rangeCooldown = 3f;
    public float specialCooldown = 6f;
    protected float nextRangeTime;
    protected float nextSpecialTime;
    [Header("Range Attack")]
    public GameObject projectilePrefab;
    public float projectileSpeed = 24f;
    [Header("HitBoxs")]
    public BoxCollider groundSmashHB;

    protected virtual void Awake()
    {
        currentHealth = maxHealth;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        playerObj = GameObject.FindWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        groundSmashHB = GameObject.FindWithTag("GroundSmashHitBox").GetComponent<BoxCollider>();
        groundSmashHB.enabled = false;
        animator.speed = normalSpeed;
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        enemyLocation = transform.position;
        playerTarget = playerObj.transform.position;

        Vector3 direction = (playerTarget - enemyLocation).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 12f);

        distanceToPlayer = Vector3.Distance(enemyLocation, playerTarget);
        agent.SetDestination(playerTarget);
    }

    public virtual void TakeDamage(float damage)
    {
        isTakenDamage = true;
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            Death();
        }

        if (currentHealth <= 50 && currentHealth > 1)
        {
            Stage2();
            isAnger = true;
        }
    }

    public virtual void Death()
    {
        Debug.Log("BaseBosee Death");
        animator.SetBool("isDead", true);
        agent.enabled = false;
    }

    public virtual void SpeicalAttack()
    {
        Debug.Log("BaseBosee Speical");

    }

    public virtual void RangeAttack()
    {
        Debug.Log("BaseBoss Range");
    }

    public virtual void BaseAttack()
    {
        Debug.Log("BaseBoss BaseAttack");
    }

    public virtual void Attack()
    {


        if (!isAttacking)
        {
            if (distanceToPlayer < 1.3f && Time.time >= nextSpecialTime)
            {
                SpeicalAttack();
                nextSpecialTime = Time.time + specialCooldown;
            }
            else if (distanceToPlayer > 3f && Time.time >= nextRangeTime)
            {
                RangeAttack();
                nextRangeTime = Time.time + rangeCooldown;
            }
            else if (distanceToPlayer < 1.3f)
            {
                BaseAttack();
            }
        }

    } 
    public virtual void Stage2()
    {
        animator.speed = 1.5f;
        agent.speed *= 1.2f;
        
    }

    public virtual void SpawnProjectile()
    {

        GameObject proj = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);

        Vector3 dir = (playerObj.transform.position - projectileSpawnPoint.position).normalized;

        Rigidbody rb = proj.GetComponent<Rigidbody>();
        rb.linearVelocity = dir * projectileSpeed;
        

        proj.transform.forward = dir;
    }

}
