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
    protected float currentHealth;
    public bool isAnger;
    public float damage;
    protected Animator animator;
    public float normalSpeed = 1.0f;
    public float enragedSpeed = 1.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerObj = GameObject.FindWithTag("Player");
        animator = GetComponent<Animator>();
        animator.speed = normalSpeed;


    }

    // Update is called once per frame
    void Update()
    {
        
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
            Debug.Log("Stage 2");
        }
    }

    public virtual void Death()
    {
        Debug.Log("Death");
    }

    public virtual void SpeicalAttack()
    {
        Vector3 enemyLocation = transform.position;
        Vector3 playerTarget = playerObj.transform.position;

        float distanceToPlayer = Vector3.Distance(enemyLocation, playerTarget);
    }
  
}
