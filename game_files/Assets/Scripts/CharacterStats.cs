using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts
{
    public class CharacterStats : MonoBehaviour
    {
        [SerializeField]
        private Loader _loader;
        private Animator _animator;
        [SerializeField]
        private int _maxHealth = 100;
        [SerializeField]
        private int _currentHealth;
        [SerializeField]
        private int _damage = 30;
        [SerializeField]
        private bool _immortal = false;

        private Counter _masterKillCounter;

        [SerializeField] 
        private HealthBar _healthBar;


        [SerializeField]
        [Range(1,100)]
        private float _dropChange = 25;
        [SerializeField]
        private GameObject _drop;

        void Start()
        {
            _animator = GetComponent<Animator>();
            _currentHealth = _maxHealth;

            _masterKillCounter = GameObject.FindGameObjectWithTag("RoomMaster").GetComponent<Counter>();

            // Setting health bar
            if (_healthBar != null)
            {
                _healthBar.SetMaxHealth(_maxHealth);
            }
        }

        public void TakeDamage(int damage)
        {
            if (!_immortal)
            {

                _currentHealth -= damage;
                if(GetComponent<AudioManager>())
                    GetComponent<AudioManager>().HurtAudio();

                if (_healthBar != null)
                    _healthBar.SetHealth(_currentHealth);

                //_animator.ResetTrigger("Hurt");
                if (this.gameObject.CompareTag("Enemy") && gameObject.GetComponent<EnemyInteraction>().GetStunHitOnValue())
                    _animator.SetTrigger("Hurt");

                if (_currentHealth < 0)
                {
                    Die();
                }
            }
        }

        public void Die()
        {
            // Add one to enemy counter
            if (this.gameObject.CompareTag("Enemy"))
            {
                _animator.ResetTrigger("Hurt");
                _animator.SetBool("attacking", false);

                _masterKillCounter.AddToCounter(1);

                // Item drop
                float ranNum = Random.Range(0, 100);

                if(ranNum <= _dropChange)
                    Instantiate(_drop, new Vector3(gameObject.transform.position.x, 
                    gameObject.transform.position.y + 0.5f, gameObject.transform.position.z), 
                    Quaternion.identity);

                // Deactive components
                GetComponent<EnemyInteraction>().enabled = false;
                GetComponent<NavMeshAgent>().enabled = false;

                // Deactivate collider which makes enemy pass throughable.
                GetComponent<CapsuleCollider>().enabled = false;
            }

            GetComponent<CharacterController>().enabled = false;

            if (gameObject.CompareTag("Player"))
            {
                GetComponent<PlayerMovement>().enabled = false;
                GetComponent<CharacterStats>().enabled = false;
                _loader.LoadCurrentLevel();
            }

            // Play death animation
            _animator.SetTrigger("isDead");

            // Deactivate this script so enemy is not interactable.
            this.enabled = false;
        }

        public void DisableAnimator()
        {
            _animator.enabled = false;
        }

        public int GetMaxHealth()
        {
            return _maxHealth;
        }

        public int GetCurrentHealth()
        {
            return _currentHealth;
        }

        public int GetDamage()
        {
            return _damage;
        }

        public int ReturnDamage()
        {
            return _damage;
        }

        public int ReturnHealth()
        {
            return _currentHealth;
        }

        public void Heal(int heal)
        {
            if (heal + _currentHealth > _maxHealth)
                _currentHealth = _maxHealth;

            else
                _currentHealth += heal;
        }
    }
}
