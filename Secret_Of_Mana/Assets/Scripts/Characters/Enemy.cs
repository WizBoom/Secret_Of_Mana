using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Enemy : Character
{
    public override void Attack(Direction direction)
    {
        Vector2 range = new Vector2(m_VisualCharacter.GetComponent<BoxCollider2D>().size.x + m_VisualCharacter.m_Character.m_CharacterStats.m_UnarmedRange,
            m_VisualCharacter.GetComponent<BoxCollider2D>().size.y + m_VisualCharacter.m_Character.m_CharacterStats.m_UnarmedRange);

        Collider2D[] colliders = Physics2D.OverlapCapsuleAll(m_VisualCharacter.transform.position, range, CapsuleDirection2D.Vertical, 0);
        foreach (var collider in colliders)
        {
            if (collider.tag == "Character" && collider.gameObject.layer == GameManager.m_PlayerLayer)
            {
                //Get character
                VisualCharacter character = collider.GetComponent<VisualCharacter>();
                if (character)
                {
                    character.m_Character.ApplyHealthpoints(-m_VisualCharacter.m_Character.m_CharacterStats.m_Attack);
                }
            }
        }
    }
}
