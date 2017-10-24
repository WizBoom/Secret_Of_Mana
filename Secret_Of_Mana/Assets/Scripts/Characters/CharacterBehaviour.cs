using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Abstract class that defines a character's behaviour
public abstract class CharacterBehaviour : ScriptableObject
{
    public abstract void Update();
}
