using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player : Character
{
    public Weapon m_Weapon;

    public override void Attack(Direction direction)
    {
        m_Weapon.Attack(direction, m_VisualCharacter);
    }
}
