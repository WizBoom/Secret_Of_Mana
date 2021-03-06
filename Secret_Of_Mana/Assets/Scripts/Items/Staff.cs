﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Staff", menuName = "Items/Weapons/Staff")]
public class Staff : Weapon
{
    public int m_HealAmount = 5;
    public int m_ManaConsumption = 5;

    public override void Attack(Direction direction, VisualCharacter visualCharacter)
    {
        Debug.Log("Healing...");
        if (visualCharacter.m_Character.ApplyManapoints(-m_ManaConsumption))
        {
            //Calculate the range based on the size of the character
            Vector2 range = new Vector2(visualCharacter.GetComponent<BoxCollider2D>().size.x + m_Range,
            visualCharacter.GetComponent<BoxCollider2D>().size.y + m_Range);

            Collider2D[] colliders = Physics2D.OverlapCapsuleAll(visualCharacter.transform.position, range, CapsuleDirection2D.Vertical, 0f);
            foreach (var collider in colliders)
            {
                if (collider != visualCharacter.GetComponent<Collider2D>() && collider.tag == "Character" && 
                    collider.gameObject.layer == GameManager.m_PlayerLayer)
                {
                    //Get character
                    VisualCharacter character = collider.GetComponent<VisualCharacter>();
                    if (character)
                    {
                        character.m_Character.ApplyHealthpoints(m_HealAmount);
                    }
                }
            }
        }
    }
}
