using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Arrow : MonoBehaviour
{
    private float _MaxTimer = 0f;
    private float _CurrentTimer = 0f;
    private Collider2D _OwningCollider;
    private Bow _OwningBow;

    public void Initialize(float timer, Vector2 velocity, Collider2D owner, Bow owningBow)
    {
        _MaxTimer = timer;
        var rigidBody = GetComponent<Rigidbody2D>();
        if (rigidBody)
            rigidBody.velocity = velocity;
        _OwningCollider = owner;
    }

    private void Update()
    {
        _CurrentTimer += Time.deltaTime;
        if (_CurrentTimer >= _MaxTimer)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other != _OwningCollider && other.tag == "Character")
        {
            Debug.Log(other.gameObject.name);
        }
    }
}
