using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SocialPlatforms;

public class BossInteraction : MonoBehaviour
{
    [SerializeField] private Loader _loader;
    [SerializeField] private bool _activateBoss = false;

    // Components from the object
    private EnemyLocomotionAgent _locomotion;
    private CharacterController _controller;
    private NavMeshAgent _agent;
    private Animator _animator;

    [SerializeField]
    [Range(1, 3)]
    private int _bossStage = 1;

    [SerializeField] 
    private bool _letItRain = false;
    [SerializeField]
    private bool _bulletHell = false;

    // Scripts that activate attack scripts for the boss
    [SerializeField] private GameObject _roomMaster;
    private StartSpawning _bossSpecialAttackSpawnProjectiles;
    private BossEvent _bossSpecialAttackMakeItRain;

    // Boss temps
    private float _startingTime = 0;

    [SerializeField] private Transform _target;

    // temp
    private int _bossHealth;

    // Start is called before the first frame update
    void Start()
    {
        _locomotion = GetComponent<EnemyLocomotionAgent>();
        _controller = GetComponent<CharacterController>();
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();

        _bossSpecialAttackSpawnProjectiles = _roomMaster.GetComponent<StartSpawning>();
        _bossSpecialAttackMakeItRain = _roomMaster.GetComponent<BossEvent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_activateBoss)
        {
            _bossHealth = GetComponent<CharacterStats>().GetCurrentHealth();

            _animator.SetBool("NonActive", false);

            if (!_letItRain && !_bulletHell)
            {
                int ranNum = Random.Range(0,2);

                if (ranNum == 0)
                    _letItRain = true;

                else if (ranNum == 1)
                    _bulletHell = true;
            }

            if (_startingTime == 0)
                _startingTime = Time.time;

            if (Time.time - _startingTime >= 4 && 
                !_animator.GetCurrentAnimatorStateInfo(0).IsName("Hurt") ||
                _animator.GetCurrentAnimatorStateInfo(0).IsName("Statue"))
            {
                // Start casting animation
                _locomotion.BossCasting(_animator);
            }

            // While boss stage is 1
            if (_bossStage == 1 &&
                _animator.GetCurrentAnimatorStateInfo(0).IsName("Casting"))
            {
                if (_letItRain)
                        _bossSpecialAttackMakeItRain.ToggleLetItRain();

                if (_bulletHell)
                    _bossSpecialAttackSpawnProjectiles.ToggleStartSpawningValue();

                if (Time.time - _startingTime >= 15 && _bossSpecialAttackMakeItRain.GetPlatformBoolValue() == false)
                {
                    _bossSpecialAttackMakeItRain.ChangeSpawningRate(5);
                    _bossSpecialAttackMakeItRain.ToggleLetItRain();
                    _bossSpecialAttackSpawnProjectiles.ToggleStartSpawningValue();
                    _bossSpecialAttackMakeItRain.TogglePlatformMovement();
                }

                if (_bossHealth <= 500)
                {
                    _animator.ResetTrigger("Hurt");
                    _animator.ResetTrigger("Casting");
                    _animator.SetTrigger("Hurt");
                    _bossStage++;
                    ResetBossAttacks();
                }
            }

            // While boss stage is 2
            else if (_bossStage == 2 &&
                _animator.GetCurrentAnimatorStateInfo(0).IsName("Casting"))
            {
                _bossSpecialAttackSpawnProjectiles.ChangeSpawnRate(1f);
                _bossSpecialAttackMakeItRain.ChangeSpawningRate(3);

                if (_letItRain)
                    _bossSpecialAttackMakeItRain.ToggleLetItRain();

                if (_bulletHell)
                    _bossSpecialAttackSpawnProjectiles.ToggleStartSpawningValue();

                if (Time.time - _startingTime >= 10)
                {
                    if (!_letItRain)
                    {
                        _bossSpecialAttackMakeItRain.ToggleLetItRain();
                        _letItRain = true;
                    }

                    if (!_bulletHell)
                    {
                        _bossSpecialAttackMakeItRain.ToggleLetItRain();
                        _bulletHell = true;
                    }
                }

                if (Time.time - _startingTime >= 20 && _bossSpecialAttackMakeItRain.GetPlatformBoolValue() == false)
                {
                    _bossSpecialAttackMakeItRain.ChangeSpawningRate(4.5f);
                    _bossSpecialAttackMakeItRain.ToggleLetItRain();
                    _bossSpecialAttackSpawnProjectiles.ToggleStartSpawningValue();
                    _bossSpecialAttackMakeItRain.TogglePlatformMovement();
                }

                if (_bossHealth <= 250)
                {
                    _animator.ResetTrigger("Hurt");
                    _animator.ResetTrigger("Casting");
                    _animator.SetTrigger("Hurt");
                    _bossStage++;
                    ResetBossAttacks();
                }
            }

            // While boss stage is 3
            else if (_bossStage == 3 &&
                _animator.GetCurrentAnimatorStateInfo(0).IsName("Casting"))
            {
                _bossSpecialAttackSpawnProjectiles.ChangeSpawnRate(0.1f);
                _bossSpecialAttackMakeItRain.ChangeSpawningRate(3);

                if (_letItRain)
                    _bossSpecialAttackMakeItRain.ToggleLetItRain();

                if (_bulletHell)
                    _bossSpecialAttackSpawnProjectiles.ToggleStartSpawningValue();

                if (Time.time - _startingTime >= 30 && _bossSpecialAttackMakeItRain.GetPlatformBoolValue() == false)
                {
                    _bossSpecialAttackMakeItRain.ChangeSpawningRate(5);
                    _bossSpecialAttackMakeItRain.ToggleLetItRain();
                    _bossSpecialAttackSpawnProjectiles.ToggleStartSpawningValue();
                    _bossSpecialAttackMakeItRain.TogglePlatformMovement();
                }

                if (_bossHealth <= 150)
                {
                    _animator.ResetTrigger("Casting");
                    _animator.SetTrigger("Hurt");
                    _bossStage++;
                    ResetBossAttacks();
                }
            }

            // While boss stage is 3
            else if (_bossStage == 4 &&
                     _animator.GetCurrentAnimatorStateInfo(0).IsName("Casting")) 
            {
                _bossSpecialAttackSpawnProjectiles.ChangeSpawnRate(0.1f);
                _bossSpecialAttackMakeItRain.ChangeSpawningRate(5);

                if (_letItRain)
                    _bossSpecialAttackMakeItRain.ToggleLetItRain();

                if (_bulletHell)
                    _bossSpecialAttackSpawnProjectiles.ToggleStartSpawningValue();

                if (Time.time - _startingTime >= 15 && _bossSpecialAttackMakeItRain.GetPlatformBoolValue() == false)
                {
                    /*_bossSpecialAttackSpawnProjectiles.ToggleStartSpawningValue();*/
                    _bossSpecialAttackMakeItRain.TogglePlatformMovement();
                }

                if (_bossHealth <= 0)
                {
                    _animator.ResetTrigger("Casting");
                    _animator.ResetTrigger("Hurt");
                    _animator.enabled = false;
                    enabled = false;

                    if(_bossSpecialAttackSpawnProjectiles.GetStartSpawningValue())
                        _bossSpecialAttackSpawnProjectiles.ToggleStartSpawningValue();

                    _loader.LoadMenu();
                }
            }
        }
    }

    public void ActivateBoss()
    {
        _activateBoss = true;
    }

    public void ResetBossAttacks()
    {
        _bossSpecialAttackMakeItRain.ChangeSpawningRate(2);

        if(_bossSpecialAttackMakeItRain.GetLetItRainValue())
            _bossSpecialAttackMakeItRain.ToggleLetItRain();

        if (_bossSpecialAttackSpawnProjectiles.GetStartSpawningValue())
            _bossSpecialAttackSpawnProjectiles.ToggleStartSpawningValue();

        if(_bossSpecialAttackMakeItRain.GetPlatformBoolValue())
            _bossSpecialAttackMakeItRain.TogglePlatformMovement();

        _startingTime = 0;
    }

    public bool ReturnActiveState()
    {
        return _activateBoss;
    }
}
