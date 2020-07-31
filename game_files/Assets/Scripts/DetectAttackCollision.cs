using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class DetectAttackCollision : MonoBehaviour
{
    [SerializeField]
    private string _targetTag;
    [SerializeField] 
    private bool _colliderOn = false;
    [SerializeField] 
    private GameObject _parent;

    private bool played = false;

    void Update()
    {
        if(GetComponent<MeshRenderer>())
            GetComponent<MeshRenderer>().enabled = false;

        if (_colliderOn)
        {
            this.GetComponent<BoxCollider>().enabled = true;
            //this.GetComponent<MeshRenderer>().enabled = true;

            if (!played)
            {
                _parent.GetComponent<AudioManager>().AttackAudio();
                played = true;
            }
        }

        else if (!_colliderOn)
        {
            this.GetComponent<BoxCollider>().enabled = false;
            //this.GetComponent<MeshRenderer>().enabled = false;
            played = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var target = other.gameObject.GetComponent<CharacterStats>();

        if (other.tag == _targetTag)
        {
            target.TakeDamage(_parent.GetComponent<CharacterStats>().GetDamage());
            target.GetComponent<AudioManager>().HurtAudio();
        }
            
    }

    public void ChangeColliderValue(bool value)
    {
        _colliderOn = value;
    }

    public bool GetColliderValue()
    {
        return _colliderOn;
    }
}
