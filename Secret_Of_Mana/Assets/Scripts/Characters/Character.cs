using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Character
{
    public CharacterStats m_CharacterStats;
    private int _CurrentHealthpoints = 0;
    private int _CurrentManapoints = 0;
    private int _Level = 1;
    public Vector3 m_LastSavedPosition;

    public VisualCharacter m_VisualCharacter;

    public abstract void Attack(Direction direction);

    public void InitCharacter()
    {
        _CurrentHealthpoints = m_CharacterStats.m_MaxHealthPoints;
        _CurrentManapoints = m_CharacterStats.m_MaxManaPoints;
    }

    public void ApplyHealthpoints(int healthpoints)
    {
        int health = healthpoints;
        if (health < 0)
        {
            health += m_CharacterStats.m_Defence;

            //Set the damage to 1 if the defence factor is higher than 0 (so the player doesn't get healed from attacks)
            if (health >= 0)
                health = -1;
        }

        _CurrentHealthpoints += health;
        if (_CurrentHealthpoints > m_CharacterStats.m_MaxHealthPoints)
            _CurrentHealthpoints = m_CharacterStats.m_MaxHealthPoints;

        if (_CurrentHealthpoints <= 0)
        {
            Debug.Log("Thing is dead");
            return;
        }

        Debug.Log("Current Health: " + _CurrentHealthpoints);
    }

    public bool ApplyManapoints(int manapoints)
    {
        if (_CurrentManapoints + manapoints < 0)
            return false;

        _CurrentManapoints += manapoints;
        if (_CurrentManapoints >= m_CharacterStats.m_MaxManaPoints)
            _CurrentManapoints = m_CharacterStats.m_MaxManaPoints;

        Debug.Log("Current Mana: " + _CurrentManapoints);
        return true;
    }
}
