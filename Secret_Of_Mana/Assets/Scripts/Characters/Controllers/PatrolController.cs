using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolController : AIController
{
    public List<Vector3> m_PatrolPoints; 
    private float _Speed;
    private Rigidbody2D _RigidBody2D;
    private int _CurrentTargetIndex = 0;
    public float m_Distance = 0.5f;

    public override void InitController(GameObject visualCharacter)
    {
        var ai = visualCharacter.AddComponent<PatrolController>();
        ai.m_PatrolPoints = m_PatrolPoints;
        ai.m_Distance = m_Distance;
    }

    private void Start()
    {
        //Get Rigidbody
        _RigidBody2D = GetComponent<Rigidbody2D>();

        //Get Speed
        _Speed = GetComponent<VisualCharacter>().m_Character.m_CharacterStats.m_Speed;
    }

    private void Update()
    {
        //Get direction
        if (Vector3.Distance(m_PatrolPoints[_CurrentTargetIndex], transform.position) > m_Distance)
        {
            var direction = m_PatrolPoints[_CurrentTargetIndex] - this.transform.position;
            direction.Normalize();
            direction *= _Speed;
            _RigidBody2D.velocity = direction;
        }
        else
        {
            _CurrentTargetIndex++;
            if (_CurrentTargetIndex >= GameManager.m_Instance.m_CharacterManager.m_Enemies.Count)
                _CurrentTargetIndex = 0;
        }

    }
}
