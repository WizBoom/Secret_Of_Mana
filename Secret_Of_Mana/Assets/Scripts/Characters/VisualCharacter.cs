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

    private void Start()
    {
        _RigidBody = GetComponent<Rigidbody2D>();
        
        //Attach rigidbody if none is present
        if (!_RigidBody)
            gameObject.AddComponent<Rigidbody2D>();
        _RigidBody.gravityScale = 0f;
        _RigidBody.freezeRotation = true;

        CharacterController2D charController = GetComponent<CharacterController2D>();

        //If there is a character controller, set the speed
        if (charController)
            charController.m_Speed = m_Character.m_CharacterStats.m_Speed;
    }
}
