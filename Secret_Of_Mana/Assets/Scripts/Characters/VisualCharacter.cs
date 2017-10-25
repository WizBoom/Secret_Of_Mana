using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CharacterController2D))]
public class VisualCharacter : MonoBehaviour
{
    public Character m_Character;
    private Rigidbody2D _RigidBody;

    private void Awake()
    {
        m_Character.m_VisualCharacter = GetComponent<VisualCharacter>();
        _RigidBody = GetComponent<Rigidbody2D>();
        
        //Attach rigidbody if none is present
        if (!_RigidBody)
            gameObject.AddComponent<Rigidbody2D>();
        _RigidBody.gravityScale = 0f;
        _RigidBody.freezeRotation = true;

        CharacterController2D charController = GetComponent<CharacterController2D>();

        //Attach CharacterController2 if none is present
        if (!charController)
            charController = gameObject.AddComponent<CharacterController2D>();
        charController.m_Speed = m_Character.m_Speed;
    }

    private void Update()
    {

    }
}
