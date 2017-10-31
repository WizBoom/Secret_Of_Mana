using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR.WSA.Persistence;

[System.Serializable]
public abstract class Character
{
    public CharacterStats m_CharacterStats;
    public int m_CurrentHealthpoints { get; set; }
    public int m_CurrentManapoints { get; set; }
    public float m_CurrentWeaponTimer { get; set; }
    public float m_CurrentManaTimer { get; set; }
    private int _Level = 1;
    public Vector3 m_LastSavedPosition;
    public VisualCharacter m_VisualCharacter;

    public abstract void Attack(Direction direction);

    public void InitCharacter()
    {
        m_CurrentHealthpoints = m_CharacterStats.m_MaxHealthPoints;
        m_CurrentManapoints = m_CharacterStats.m_MaxManaPoints;
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

        m_CurrentHealthpoints += health;
        if (m_CurrentHealthpoints > m_CharacterStats.m_MaxHealthPoints)
            m_CurrentHealthpoints = m_CharacterStats.m_MaxHealthPoints;

        GameManager.m_Instance.m_UIManager.Refresh();

        if (m_CurrentHealthpoints <= 0)
        {
            Debug.Log("Thing is dead");

            //Remove from list
            for (int i = 0; i < GameManager.m_Instance.m_CharacterManager.m_Enemies.Count; i++)
            {
                if (GameManager.m_Instance.m_CharacterManager.m_Enemies[i] == this)
                {
                    GameObject.Destroy(m_VisualCharacter.gameObject);
                    GameManager.m_Instance.m_CharacterManager.m_Enemies.RemoveAt(i);
                }
            }
            return;
        }

        Debug.Log("Current Health: " + m_CurrentHealthpoints);
    }

    public bool ApplyManapoints(int manapoints)
    {
        if (m_CurrentManapoints + manapoints < 0)
            return false;

        m_CurrentManapoints += manapoints;
        if (m_CurrentManapoints >= m_CharacterStats.m_MaxManaPoints)
            m_CurrentManapoints = m_CharacterStats.m_MaxManaPoints;

        GameManager.m_Instance.m_UIManager.Refresh();
        Debug.Log("Current Mana: " + m_CurrentManapoints);
        return true;
    }
}
