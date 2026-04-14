using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CombotSystem : MonoBehaviour
{   
    public Player player;
    
    public List<AttackSO> combo;
    float lastClickTime;
    float timeBetweenCombo = 0.3f;
    float nextAttackTime = 0f;
    float lastComboEnd;
    int comboCounter;

    Animator animator;
    [SerializeField] GreatSword sword;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Attack();
        }    
        ExitAttack();
    }

    void Attack()
    {
        if (Time.time - lastComboEnd > timeBetweenCombo && comboCounter < combo.Count)
        {
            CancelInvoke("EndCombo");
            player.isAttacking = true;
            var m_clipInfo = animator.GetCurrentAnimatorClipInfo(0);
            float clipLength = m_clipInfo[0].clip.length;
            if (Time.time - lastClickTime >= clipLength - 0.1)
            {
                animator.runtimeAnimatorController = combo[comboCounter].animatorOV;
                animator.Play("Attack", 1, 0);
                animator.ResetTrigger("Attack");
                animator.SetTrigger("Attack");
                sword.Damage = combo[comboCounter].Damage;
                comboCounter++;
                lastClickTime = Time.time;

                if (comboCounter >= combo.Count)
                {
                    comboCounter = 0;
                }
                if (comboCounter < combo.Count)
                {
                    Debug.Log("Current Combo Index: " + comboCounter);
                    Debug.Log("Attack Name: " + combo[comboCounter].name);
                }
                
            }
        }
    }

    void ExitAttack()
    {
        if(animator.GetCurrentAnimatorStateInfo(1).normalizedTime > 0.9f && animator.GetCurrentAnimatorStateInfo(1).IsTag("Attack"))
        {
            Invoke("EndCombo", 1);
            sword.EnableBoxTrigger();
        }
    }

    void EndCombo()
    {
        comboCounter = 0;
        player.isAttacking = false;
        lastComboEnd = Time.time;
    }
}
