using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController2D : MonoBehaviour
{
    public int m_Speed { get; set; }
    private Rigidbody2D _RigidBody;

    void Awake()
    {
        _RigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!Mathf.Approximately(Input.GetAxisRaw("Horizontal"), 0f) || 
            !Mathf.Approximately(Input.GetAxisRaw("Vertical"), 0f))
        {
            _RigidBody.velocity = new Vector2(m_Speed * Input.GetAxisRaw("Horizontal"),
               m_Speed * Input.GetAxisRaw("Vertical"));
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }
}
