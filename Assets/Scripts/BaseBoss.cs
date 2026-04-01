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
    public float currentHealth;
    public bool isAnger;
    public float damage = 10;
    [Header("Animation/Speed")]
    protected Animator animator;
    public float normalSpeed = 1.0f;
    public float enragedSpeed = 1.5f;
    [Header("DistanceBetweenPlayer")]
    public Vector3 enemyLocation;
    public Vector3 playerTarget;
    public float distanceToPlayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        playerObj = GameObject.FindWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
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
        currentHealth -= damage;
        if(currentHealth < 0)
        {
            Death();
        }

        if (currentHealth <= 50 && currentHealth > 1)
        {
            Stage2();
        }
    }

    public virtual void Death()
    {
        Debug.Log("BaseBosee Death");
    }

    public virtual void SpeicalAttack()
    {
        Debug.Log("BaseBoss Speical");

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
        float timer = 0;
        timer += Time.deltaTime;
        if (distanceToPlayer < 1.3f)
        {
            BaseAttack();
        }
        else if(distanceToPlayer < 3f && timer > 3)
        {
            RangeAttack();
        }
        else if(distanceToPlayer < 2f && timer > 5)
        {
            SpeicalAttack();
        }
    }

    public virtual void Stage2()
    {
        Debug.Log("BaseBosee Stage2");
    }
  
}
