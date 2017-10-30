using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Arrow : MonoBehaviour
{
    private float _MaxTimer = 0f;
    private float _CurrentTimer = 0f;
    private VisualCharacter _OwningCharacter;
    private Bow _OwningBow;

    public void Initialize(float timer, Vector2 velocity, VisualCharacter owner, Bow owningBow)
    {
        _MaxTimer = timer;
        var rigidBody = GetComponent<Rigidbody2D>();
        if (rigidBody)
            rigidBody.velocity = velocity;
        _OwningCharacter = owner;
        _OwningBow = owningBow;
    }

    private void Update()
    {
        _CurrentTimer += Time.deltaTime;
        if (_CurrentTimer >= _MaxTimer)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Collider2D ownerCollider = _OwningCharacter.GetComponent<Collider2D>();
        if (ownerCollider && other != ownerCollider && other.tag == "Character" &&
             other.gameObject.layer != GameManager.m_PlayerLayer)
        {
            int damage = _OwningCharacter.m_Character.m_CharacterStats.m_Attack + _OwningBow.m_ExtraDamage;
            _OwningCharacter.m_Character.ApplyHealthpoints(-damage);
            Destroy(gameObject);
        }
    }
}
