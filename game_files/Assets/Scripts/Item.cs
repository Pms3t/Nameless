using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private int _heal = 30;
    [SerializeField] private HealthBar _healthBar;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<AudioManager>().HealAudio();
            var player = other.gameObject.GetComponent<CharacterStats>();
            player.Heal(_heal);

            if (_healthBar != null)
                _healthBar.SetHealth(player.GetCurrentHealth());

            Destroy(gameObject);
        }
    }
}
