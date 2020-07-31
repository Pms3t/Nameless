using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : ScriptableObject
{
    // Constructor
    public Stats(int maxHealth, int damage, int stunCountCapacity)
    {
        _maxHealth = maxHealth;
        _currentHealth = _maxHealth;
        _damage = damage;
        _immortal = false;
        _unmovable = false;
        _hitStun = false;
        _stunned = false;
        _immobilizeCount = 0;
        _stunCountCapacity = stunCountCapacity;
        _stunCount = 0;
    }

    // Declaring properties
    public Animator _animator { get; set; }
    public int _maxHealth { get; set; }
    public int _currentHealth { get; set; }
    public int _damage { get; set; }

    public bool _immortal { get; set; }
    public bool _unmovable { get; set; }
    public bool _hitStun { get; set; }
    public bool _stunned { get; set; }

    public int _immobilizeCount { get; set; }
    public int _stunCountCapacity { get; set; }
    public int _stunCount { get; set; }
}