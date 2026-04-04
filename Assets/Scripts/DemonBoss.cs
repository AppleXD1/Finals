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
        if (!isAttacking)
        {
            animator.SetBool("isMoving", agent.velocity.sqrMagnitude > 0.1f);
        }
        Debug.Log(currentHealth);
    }

    public override void BaseAttack()
    {
        base.BaseAttack();
        StartCoroutine(BaseAttackWait());
        
    }

    public override void RangeAttack()
    {
        base.RangeAttack();
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
        timer = 0;
        agent.velocity = Vector3.zero;

        animator.SetBool("isMoving", false);
        animator.ResetTrigger("BossThrow");
        animator.SetTrigger("BossThrow");

        yield return null;

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        float length = stateInfo.length;

        
        rangeAttack = false;
        agent.isStopped=false;

    }
}
