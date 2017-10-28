using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : ScriptableObject
{
    public float m_Range = 3f;
    public abstract void Attack(Direction direction, VisualCharacter visualCharacter);
}
