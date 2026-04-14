using System.Collections;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using Debug = UnityEngine.Debug;
public class DemonBoss : BaseBoss
{
    public ParticleSystem PSclouds;
   
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

        speicalAttack = true;
        isAttacking = true;
        StartCoroutine(BossSpeicalWait());
    }

    IEnumerator BaseAttackWait()
    {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;

        animator.SetBool("isMoving", false);
        animator.ResetTrigger("BossAttack");
        animator.SetTrigger("BossAttack");

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

    IEnumerator BossSpeicalWait()
    {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
        animator.SetBool("isMoving", false);
        animator.ResetTrigger("BossSpeical");
        animator.SetTrigger("BossSpeical");
        groundSmashHB.enabled = true;
       
        yield return null;

        while (!animator.GetCurrentAnimatorStateInfo(0).IsName("DownSlamDemon"))
        {
            yield return null;
        }

        float length = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(length);

        nextSpecialTime = Time.time + specialCooldown;
        groundSmashHB.enabled = false;
        isAttacking = false;
        agent.isStopped = false;
    }

    IEnumerator CloudsDestroy()
    {
        var clouds = Instantiate(PSclouds, groundSmashHB.transform.position, Quaternion.Euler(-90f, 0f, 0f));
        yield return new WaitForSeconds(2f);
        Destroy(clouds);
    }

    public void CreateClouds()
    {
        StartCoroutine(CloudsDestroy());
    }

    public override void Stage2()
    {
        base.Stage2();
        specialCooldown = 4.5f;
        rangeCooldown = 2.5f;
    }
}
