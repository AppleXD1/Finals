using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
public class DemonBoss : BaseBoss
{
   
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
        if (!isAttacking && !rangeAttack && !speicalAttack)
        {
            animator.SetBool("isMoving", agent.velocity.sqrMagnitude > 0.1f);
        }
        Debug.Log(currentHealth);
    }

    public override void BaseAttack()
    {
        base.BaseAttack();
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

    IEnumerator BaseAttackWait()
    {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;

        animator.SetBool("isMoving", false);
        animator.ResetTrigger("BossAttack");
        animator.SetTrigger("BossAttack");

        // small delay so animator can switch into attack state
        while(distanceToPlayer < 1.3f)
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

        while (!animator.GetCurrentAnimatorStateInfo(0).IsName("Throwing"))
        {
            yield return null;
        }

        float length = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(length);

        nextRangeTime = Time.time + rangeCooldown;

        isAttacking = false;
        agent.isStopped = false;

    }
}
