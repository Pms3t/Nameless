using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(EnemyLocomotionAgent))]
public class EnemyInteraction : MonoBehaviour
{
    [SerializeField]
    private float _movementSpeed = 10f;
    [SerializeField]
    private float _gravity = 15f;

    // Interaction range(s)
    [SerializeField] 
    private float _aggroRange = 10f;
    [SerializeField]
    private float _attackRange = 5f;
    [SerializeField]
    private float _rangedAttackRange = 15f;

    // Options
    [SerializeField] 
    private bool _hitStunOn = false;
    [SerializeField]
    private bool _followOutOfRange = false;
    [SerializeField]
    private bool _stationary = false;

    // Gizmo colors
    [SerializeField]
    private Color _aggroRangeColor = Color.magenta;
    [SerializeField]
    private Color _attackRangeColor = Color.red;
    [SerializeField]
    private Color _rangedAttackRangeColor = Color.yellow;

    // Instance of the player's location;
    [SerializeField]
    private Transform _target;

    // Components from the object
    private CharacterController _controller;
    private NavMeshAgent _agent;
    private Animator _animator;

    private EnemyLocomotionAgent _locomotion;

    // Temp
    private float _agentSpeed;
    private bool _attacking = false;

    // Triggers
    private bool _continueTracking = false;

    // Start is called before the first frame update
    void Start()
    {
        _locomotion = GetComponent<EnemyLocomotionAgent>();
        _controller = GetComponent<CharacterController>();
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();

        //_target = FindObjectOfType<PlayerCombat>().transform;
        _agentSpeed = _agent.speed;
    }

    // Update is called once per frame
    void Update()
    {
        Gravity();

        if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") && _agent.speed == 0 && _controller.isGrounded)
        {
            _attacking = false;
            _agent.speed = _agentSpeed;
        }

        if (Vector3.Distance(_target.position, transform.position) <
            _aggroRange && _controller.isGrounded || _continueTracking)
        {
            _agent.enabled = true;

            if (_followOutOfRange)
                _continueTracking = true;

            Vector3 direction = _target.position - transform.position;

            TrackTheTarget(direction);

            if (!_stationary && direction.magnitude > _attackRange)
                AgentChaseTheTarget();
                //ChaseTheTarget(direction);

            else
                AttackTheTarget();
        }

        else
            _locomotion.Idling(_animator);
    }

    void TrackTheTarget(Vector3 direction)
    {
        direction.y = 0;

        if (!_attacking)
        {
            transform.rotation = Quaternion.Slerp(
                transform.rotation, Quaternion.LookRotation(direction), 0.04f
            );
        }
    }

    void AgentChaseTheTarget()
    {
        if (!_attacking)
        {
            _agent.SetDestination(_target.position);

            if (_agent.speed <= 2)
            {
                _locomotion.Walking(_movementSpeed, _animator);
                _locomotion.StopAttacking(_animator);
            }

            else if (_agent.speed > 2.1)
            {
                _locomotion.Running(_animator);
                _locomotion.StopAttacking(_animator);
            }
        }
    }

    /*void ChaseTheTarget(Vector3 direction)
    {
        transform.Translate(0,0,0.05f);
        _locomotion.Running(_animator);
    }*/

    void AttackTheTarget()
    {
        _agent.speed = 0;
        _attacking = true;
        _locomotion.Attacking(_animator);
    }

    void Gravity()
    {
        Vector3 direction = Vector3.zero;

        if (_controller.isGrounded)
            direction = Vector3.zero;

        else
        {
            direction.y -= _gravity * Time.deltaTime;
            _controller.Move(direction * Time.deltaTime);
        }
    }

    public bool GetStunHitOnValue()
    {
        return _hitStunOn;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = _aggroRangeColor;
        Gizmos.DrawWireSphere(transform.position, _aggroRange);

        Gizmos.color = _attackRangeColor;
        Gizmos.DrawWireSphere(transform.position, _attackRange);

        Gizmos.color = _rangedAttackRangeColor;
        Gizmos.DrawWireSphere(transform.position, _rangedAttackRange);
    }
}
