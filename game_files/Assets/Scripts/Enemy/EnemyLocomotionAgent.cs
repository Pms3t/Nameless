using UnityEngine;
using UnityEngine.AI;

class EnemyLocomotionAgent : MonoBehaviour
{
    public void Idling(Animator animator)
    {
        animator.SetFloat("speed", 0);
        StopAttacking(animator);
    }

    public void Walking(float speed, Animator animator)
    {
        animator.SetFloat("speed", (speed / speed) / 2);
        StopAttacking(animator);
    }

    public void Running(Animator animator)
    {
        animator.SetFloat("speed", 1);
        StopAttacking(animator);
    }

    public void Attacking(Animator animator)
    {
        animator.SetBool("attacking", true);
    }

    public void StopAttacking(Animator animator)
    {
        animator.SetBool("attacking", false);
    }

    // Exclusive boss animations
    public void BossNonActive(Animator animator)
    {
        animator.SetBool("NonActive", true);
    }

    public void BossCasting(Animator animator)
    {
        animator.SetTrigger("Casting");
    }

    public void CancelAnimation(Animator animator, string animationName)
    {
        animator.SetBool(animationName, false);
    }
}