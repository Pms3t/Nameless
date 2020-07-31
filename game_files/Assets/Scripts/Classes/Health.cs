using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health
{
    private int _health;
    private int _maxHealth;

    public Health(int health)
    {
        _health = health;
        _maxHealth = _health;
    }

    public int GetHealth()
    {
        return _health;
    }

    public float GetHealthPercent()
    {
        return (float)_health / _maxHealth;
    }

    public void Damage(int damage)
    {
        _health -= damage;

        if (_health < 0) 
            _health = 0;
    }

    public void Heal(int heal)
    {
        
        if(_health + heal >= _maxHealth)
            return;
        else
            _health += heal;
    }
}
