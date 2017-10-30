using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player : Character
{
    public Weapon m_Weapon;

    public override void Attack(Direction direction)
    {
        if (m_CurrentWeaponTimer >= m_Weapon.m_RateOfAttack)
        {
            m_Weapon.Attack(direction, m_VisualCharacter);
            m_CurrentWeaponTimer = 0;
        }
    }
}
