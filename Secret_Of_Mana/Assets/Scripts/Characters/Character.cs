using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Character/Character")]
public class Character : ScriptableObject
{
    public String m_CharacterName;
    public int m_MaxHealthPoints;
    private int _CurrentHealthPoints;
    public int m_MaxManaPoints;
    private int _CurrentManaPoints;
    public Weapon m_Weapon;
    private int _Level;
    public int m_Attack;
    public int m_Defence;

    public CharacterBehaviour m_CharacterBehaviour;
    public VisualCharacter m_VisualCharacter;
}
