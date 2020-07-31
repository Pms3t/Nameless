using System;
using Assets.Scripts.EventMaster;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(EnemyLocomotionAgent))]
public class AggressionOnLineOfSight : MonoBehaviour
{
    // Change player rotation origin. Origin should come from GameManager gameobject for preventing
    // multiple enemies trying to find player at the same time which could be seen in the performance!
    [SerializeField] 
    private Transform _player;
    [SerializeField]
    private float _rotationSpeed = 5f;
    [SerializeField]
    private float _movementSpeed = 5f;
    [SerializeField] 
    private float _aggroDistance = 10f;
    [SerializeField] 
    private float _attackRange = 5f;
    [SerializeField] 
    private bool _stationary = false;

    // Animator
    private Animator _animator;
    private EnemyLocomotionAgent _anims;

    CharacterController _controller;

    // Gizmo colors
    [SerializeField]
    private Color _aggroDistanceColor = Color.magenta;
    [SerializeField]
    private Color _attackRangeColor = Color.red;

    // test
    private NavMeshAgent navMeshAgent;
    //private NavMeshPath path;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        PlayerLocation _test = GameObject.FindGameObjectWithTag("RoomMaster").GetComponent<PlayerLocation>();
        _player = _test.GetLocation();

        navMeshAgent = GetComponent<NavMeshAgent>();

        if ((_anims == null) && (GetComponent<EnemyLocomotionAgent>() != null))
            _anims = GetComponent<EnemyLocomotionAgent>();
        else
            Debug.LogWarning("Missing Enemy Locomotion Agent component script. Please add it!");
    }

    // Update is called once per frame
    void Update()
    {
        // ADD: When ever enemy stops animation should switch from running to idle
        // if player is close enough
        // look towards him
        //CheckDistance();
        AgentMove();

        // does not work as intented at the moment.
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            _controller.enabled = false;
        }
    }

    // Tweek the method so the enemy first rotates towards the player before moving!
    // TWEEK: Require player to move into AI's field of view in order to aggro it.
    void AgentMove()
    {
        if (!_stationary && Vector3.Distance(_player.position, transform.position) < _aggroDistance)
        {
            navMeshAgent.SetDestination(_player.position);

            if (navMeshAgent.speed <= 2)
            {
                _anims.Walking(_movementSpeed, _animator);
                _anims.StopAttacking(_animator);
            }

            else if (navMeshAgent.speed > 2.1)
            {
                _anims.Running(_animator);
                _anims.StopAttacking(_animator);
            }

            if (Vector3.Distance(transform.position, _player.position) < _attackRange)
            {
                _controller.enabled = false;
                _anims.Attacking(_animator);
            }
        }
    }

    void CheckDistance()
    {
        Vector3 direction = _player.position - transform.position;
        //float angle = Vector3.Angle(direction, transform.forward); // remove this line in order to remove decreased enemies' field of view! Remove also "&& angle < 40" from the if statement below. At the moment this feature breaks the enemy's AI
        // when it cannot find player inside of its field of view causing scenario where the AI keeps on walking forward and teleporting back to it's old position after a while.

        if (Vector3.Distance(_player.position, transform.position) < _aggroDistance)
        {
            direction.y = 0;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), (Time.deltaTime * _rotationSpeed));

            // start chasing the player if he's this close
            // In this direction and with this speed!
            // If _attackRange (value of 5) is switched to _aggroDistance (value of 10) the enemy is unable to move.
            if (direction.magnitude > _attackRange)
            {
                //transform.Translate(0, 0, (Time.deltaTime * _movementSpeed));
                Move();

                // Disabeling Attack animation if the animation is playing in order to play the correct animation when enemy is supposed to follow the player
                /*if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") && Vector3.Distance(_player.position, transform.position) > _aggroDistance)
                    _anims.StopAttacking(_animator);*/

                // Triggering walking or running animation
                if (_movementSpeed < 5)
                {
                    _anims.Walking(_movementSpeed, _animator);
                    _anims.StopAttacking(_animator);
                }

                else if (_movementSpeed >= 5)
                {
                    _anims.Running(_animator);
                    _anims.StopAttacking(_animator);
                }
                
                Debug.Log("player is closing in!");
            }

            if (direction.magnitude <= _attackRange)
            {
                _anims.Attacking(_animator);
                Debug.Log("Player is too close");
            }
        }

        // if player isn't in range go idling
        else
        {
            _anims.StopAttacking(_animator);
            _anims.Idling(_animator);
        }
    }

    void Move()
    {
        if(_controller.enabled == false)
            _controller.enabled = true;

        var velocity = transform.forward * _movementSpeed;
        _controller.Move(velocity * Time.deltaTime);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = _aggroDistanceColor;
        Gizmos.DrawWireSphere(transform.position, _aggroDistance);

        Gizmos.color = _attackRangeColor;
        Gizmos.DrawWireSphere(transform.position, _attackRange);
    }
}
