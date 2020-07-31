using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.AI;

public class FreezeIfBossIsDead : MonoBehaviour
{
    [SerializeField]
    private CharacterStats _boss;

    // Update is called once per frame
    void Update()
    {
        if (_boss.GetCurrentHealth() <= 0)
        {
            gameObject.GetComponent<Animator>().enabled = false;
            gameObject.GetComponent<NavMeshAgent>().enabled = false;
            gameObject.GetComponent<CharacterStats>().enabled = false;
            gameObject.GetComponent<EnemyInteraction>().enabled = false;
            gameObject.GetComponent<CharacterController>().enabled = false;
            this.enabled = false;
        }
    }
}
