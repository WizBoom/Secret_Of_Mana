﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : Item
{
    public float m_Range = 3f;
    public float m_RateOfAttack = 2f;
    public abstract void Attack(Direction direction, VisualCharacter visualCharacter);
}
