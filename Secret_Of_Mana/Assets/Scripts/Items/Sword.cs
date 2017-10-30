using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

[CreateAssetMenu(fileName = "Sword", menuName = "Items/Weapons/Sword")]
public class Sword : Weapon
{
    public int m_ExtraDamage = 3;
    public override void Attack(Direction direction, VisualCharacter visualCharacter)
    {
        Debug.Log("Attacking...");

        //Calculate the range based on the size of the character
        Vector2 range = new Vector2(visualCharacter.GetComponent<BoxCollider2D>().size.x + m_Range,
            visualCharacter.GetComponent<BoxCollider2D>().size.y + m_Range);

        Collider2D[] colliders = Physics2D.OverlapCapsuleAll(visualCharacter.transform.position, range, CapsuleDirection2D.Vertical, 0f);
        foreach (var collider in colliders)
        {
            if (collider != visualCharacter.GetComponent<Collider2D>() && collider.tag == "Character" && 
                collider.gameObject.layer != GameManager.m_PlayerLayer)
            {
                //Get character
                VisualCharacter character = collider.GetComponent<VisualCharacter>();
                if (character)
                    character.m_Character.ApplyHealthpoints(-visualCharacter.m_Character.m_CharacterStats.m_Attack - m_ExtraDamage);
            }
        }
    }
}
