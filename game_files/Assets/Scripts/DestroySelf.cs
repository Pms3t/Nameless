using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    [SerializeField] private int _damage = 10;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
            other.gameObject.GetComponent<CharacterStats>().TakeDamage(_damage);

            if (!other.gameObject.CompareTag("Enemy"))
            Destroy(gameObject);

        //spawn effect before destroying self

    }
}
