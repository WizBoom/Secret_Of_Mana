using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : ScriptableObject
{
    public String m_CharacterName = "Name";
    public int m_MaxHealthPoints = 10;
    public int m_MaxManaPoints = 10;
    public Weapon m_Weapon;
    private int _Level = 1;
    public int m_Attack = 1;
    public int m_Defence = 1;
    public int m_Speed = 1;

    public VisualCharacter m_VisualCharacter { get; set; }

    public abstract void Attack(Direction direction);
}
