using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Character/Player")]
public class Player : Character
{
    public override void Attack(Direction direction)
    {
        m_Weapon.Attack(direction, m_VisualCharacter);
    }
}
