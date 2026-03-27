using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CombotSystem : MonoBehaviour
{
    public PlayerInput playerInput;
    private InputAction attackAction;

    public List<AttackSO> combo;
    float lastClickTime;
    float timeBetweenCombo = 0.3f;
    float lastComboEnd;
    int comboCounter;

    Animator animator;
    [SerializeField] GreatSword sword;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        attackAction = playerInput.actions.FindAction("Attack");
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(attackAction.triggered)
        {
            Attack();
        }    
        ExitAttack();
    }

    void Attack()
    {
        if(Time.time - lastComboEnd > timeBetweenCombo && comboCounter <= combo.Count)
        {
            CancelInvoke("EndCombo");
            if(Time.time - lastComboEnd >= 0.2f)
            {
                animator.runtimeAnimatorController = combo[comboCounter].animatorOV;
                animator.Play("Attack",0,0);
                animator.SetBool("Attack", true);
                sword.Damage = combo[comboCounter].Damage;
                comboCounter++;
                lastClickTime = Time.time;

                if(comboCounter > combo.Count)
                {
                    comboCounter = 0;
                }
            }
        }
    }

    void ExitAttack()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f && animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            Invoke("EndCombo", 1);
            animator.SetBool("Attack", false);
        }
    }

    void EndCombo()
    {
        comboCounter = 0;
        lastComboEnd = Time.time;
    }
}
