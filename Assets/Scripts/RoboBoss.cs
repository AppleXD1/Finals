using System.Collections;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using Debug = UnityEngine.Debug;

public class RoboBoss : BaseBoss
{
    public ParticleSystem PSclouds;
    [Header("Explosive")]
    public GameObject explosionPrefab;
    public GameObject warningPrefab;
    public float warningDelay = 0.8f;
    public float spawnRadius = 3f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        base.Attack();
        if (!isAttacking)
        {
            animator.SetBool("isMoving", agent.velocity.sqrMagnitude > 0.1f);
        }

    }

    public override void BaseAttack()
    {
        base.BaseAttack();
        Debug.Log("Overide attack");
        if (isAttacking) return;

        isAttacking = true;
        StartCoroutine(BaseAttackWait());

    }
   
        public override void RangeAttack()
        {
            base.RangeAttack();
            if (isAttacking) return;

            isAttacking = true;
            StartCoroutine(RangeAttackWait());
        }

    public override void SpeicalAttack()
    {
        if (isAttacking) return;

        base.SpeicalAttack();
        Debug.Log("demonBoss Speical");

        isAttacking = true;
        agent.isStopped = true;
        agent.velocity = Vector3.zero;

        animator.SetTrigger("BossSpecial");
    }

    IEnumerator BaseAttackWait()
    {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;

        animator.SetBool("isMoving", false);
        animator.ResetTrigger("BossAttack");
        animator.SetTrigger("BossAttack");

        while (distanceToPlayer < 1.3f)
        {
            timer = 0f;
            break;
        }
        yield return null;

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        float length = stateInfo.length;

        yield return new WaitForSeconds(length);

        isAttacking = false;
        agent.isStopped = false;
    }

    IEnumerator RangeAttackWait()
    {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;

        animator.SetBool("isMoving", false);
        animator.ResetTrigger("BossThrow");
        animator.SetTrigger("BossThrow");

        yield return null;

        while (!animator.GetCurrentAnimatorStateInfo(0).IsName("RoboRange"))
        {
            yield return null;
        }

        float length = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(length);

        nextRangeTime = Time.time + rangeCooldown;

        isAttacking = false;
        agent.isStopped = false;

    }
    IEnumerator WarningThenExplosion(Vector3 spawnPos)
    {
        GameObject warning = Instantiate(warningPrefab, spawnPos, Quaternion.identity);

        yield return new WaitForSeconds(warningDelay);

        if (warning != null)
        {
            Destroy(warning);
        }

        Instantiate(explosionPrefab, spawnPos, Quaternion.identity);
    }

    public void SpawnWarningExplosion()
    {
        Vector3 randomOffset = new Vector3(Random.Range(-spawnRadius, spawnRadius), 0f, Random.Range(-spawnRadius, spawnRadius));

        Vector3 spawnPos = playerObj.transform.position + randomOffset;

        StartCoroutine(WarningThenExplosion(spawnPos));
    }

    public void EndSpecialAttack()
    {
        isAttacking = false;
        agent.isStopped = false;
    }

    public override void Stage2()
    {
        base.Stage2();
        specialCooldown = 4.5f;
        rangeCooldown = 2.5f;
    }
}
