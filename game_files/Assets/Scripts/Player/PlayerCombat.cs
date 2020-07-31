using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private Animator animator;
    [SerializeField]
    private float attackRate = 2f;
    /*[SerializeField]
    private float attackRange = 1f;
    [SerializeField]
    private LayerMask enemyLayers;
    [SerializeField]
    private Collider[] attackHitboxes;*/

    private float nextAttackTime = 0f;
    private int numberOfPresses;
    private bool allowCombatInputs; // Determing if user can give input during an action

    void Start()
    {
        animator = GetComponent<Animator>();
        numberOfPresses = 0;
        allowCombatInputs = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                StartCombo();

                // add half a second to current time. Meaning player can attack once in half a second.
                nextAttackTime = Time.time + 2f / attackRate;
            }
        }
    }

    // Start combo chain
    void StartCombo()
    {
        if (allowCombatInputs)
        {
            numberOfPresses++;
        }

        if (numberOfPresses == 1)
        {
            animator.SetInteger("AttackAnimation", 1);
        }
    }

    // CheckComboState is used in animations as a trigger
    public void CheckComboState()
    {
        allowCombatInputs = false;

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Attack01") && numberOfPresses == 1)
        {
            // Reset combo chain state
            animator.SetInteger("AttackAnimation", 0);
            allowCombatInputs = true;
            numberOfPresses = 0;
        }

        // If animation (Player_Attack01) is playing and number of presses is 2 or more -> Execute next animation.
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Attack01") && numberOfPresses >= 2)
        {
            animator.SetInteger("AttackAnimation", 2);
            allowCombatInputs = true;
        }

        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Attack02") && numberOfPresses == 2)
        {
            // Reset combo chain state
            animator.SetInteger("AttackAnimation", 0);
            allowCombatInputs = true;
            numberOfPresses = 0;
        }

        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Attack02") && numberOfPresses >= 3)
        {
            animator.SetInteger("AttackAnimation", 3);
            allowCombatInputs = true;
            numberOfPresses = 0;
        }

        else
        {
            animator.SetInteger("AttackAnimation", 0);
            allowCombatInputs = true;
            numberOfPresses = 0;
        }
    }
    /*
    private void ToggleCollider(Collider col)
    {
        col.enabled = !col.enabled;
        Debug.Log("col.enabled = " + col.enabled); // comment out this line later
    }

    // Activating hitboxes for attacks should be done through events inside of animation editor in Animator
    public void ActivateAttackCollider()
    {
        ToggleCollider(attackHitboxes[0]);
        //attackPoint.position
        Collider[] hitEnemies = Physics.OverlapSphere(attackHitboxes[0].transform.position, attackRange, enemyLayers);

        foreach (Collider enemy in hitEnemies)
        {
            CharacterStats stats = enemy.GetComponent<CharacterStats>();
            stats.TakeDamage();
        }
    }*/

    /*void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackHitboxes[0].transform.position, attackRange);
    }*/
}
