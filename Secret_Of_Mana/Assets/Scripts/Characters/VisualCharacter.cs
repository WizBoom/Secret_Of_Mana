using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public class VisualCharacter : MonoBehaviour
{
    public Character m_Character;
    private Rigidbody2D _RigidBody;
    private int _CurrentHealth = 0;
    private int _CurrentMana = 0;

    private void Awake()
    {
        m_Character.m_VisualCharacter = GetComponent<VisualCharacter>();
        _CurrentHealth = m_Character.m_MaxHealthPoints;
        _CurrentMana = m_Character.m_MaxManaPoints;

        _RigidBody = GetComponent<Rigidbody2D>();
        
        //Attach rigidbody if none is present
        if (!_RigidBody)
            gameObject.AddComponent<Rigidbody2D>();
        _RigidBody.gravityScale = 0f;
        _RigidBody.freezeRotation = true;

        CharacterController2D charController = GetComponent<CharacterController2D>();

        //If there is a character controller, set the speed
        if (charController)
            charController.m_Speed = m_Character.m_Speed;
    }

    public void ApplyHealthpoints(int healthpoints)
    {
        int health = healthpoints;
        if (health < 0)
        {
            health += m_Character.m_Defence;

            //Set the damage to 1 if the defence factor is higher than 0 (so the player doesn't get healed from attacks)
            if (health >= 0)
                health = -1;
        }

        _CurrentHealth += health;
        if (_CurrentHealth > m_Character.m_MaxHealthPoints)
            _CurrentHealth = m_Character.m_MaxHealthPoints;

        if (_CurrentHealth <= 0)
        {
            Debug.Log("Thing is dead");
            return;
        }

        Debug.Log("Current Health: " + _CurrentHealth);
    }

    public bool ApplyManapoints(int manapoints)
    {
        if (_CurrentMana + manapoints < 0)
            return false;

        _CurrentMana += manapoints;
        if (_CurrentMana >= m_Character.m_MaxManaPoints)
            _CurrentMana = m_Character.m_MaxManaPoints;

        Debug.Log("Current Mana: " + _CurrentMana);
        return true;
    }
}
