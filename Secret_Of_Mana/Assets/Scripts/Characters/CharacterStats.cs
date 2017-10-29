using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "Character/Stats")]
public class CharacterStats : ScriptableObject
{
    public String m_CharacterName = "Name";
    public int m_MaxHealthPoints = 10;
    public int m_MaxManaPoints = 10;
    public int m_Attack = 1;
    public int m_Defence = 1;
    public int m_Speed = 1;
}
