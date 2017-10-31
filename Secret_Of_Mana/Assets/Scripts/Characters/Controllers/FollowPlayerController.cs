using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerController : AIController
{
    private Transform _Target;
    private float _Speed;
    private Rigidbody2D _RigidBody2D;
    public float m_Distance = 1f;

    public override void InitController(GameObject visualCharacter)
    {
        var ai = visualCharacter.AddComponent<FollowPlayerController>();
        ai.m_Distance = m_Distance;
    }

    private void Start()
    {
        //Get player index
        int index = -1;
        for (int i = 0; i < GameManager.m_Instance.m_CharacterManager.m_Players.Count; i++)
        {
            if (GetComponent<VisualCharacter>().m_Character == GameManager.m_Instance.m_CharacterManager.m_Players[i])
            {
                index = i;

                //Set speed
                _Speed = GameManager.m_Instance.m_CharacterManager.m_Players[i].m_CharacterStats.m_Speed;
            }
        }

        if (index < 0)
            Destroy(this.gameObject);

        //Get target
        int targetIndex = index - 1;
        if (targetIndex < 0)
            targetIndex = GameManager.m_Instance.m_CharacterManager.m_Players.Count - 1;

        //Get target
        _Target = GameManager.m_Instance.m_CharacterManager.m_Players[targetIndex].m_VisualCharacter.transform;
        _RigidBody2D = GetComponent<Rigidbody2D>();

    }

    private void Update()
    {
        //Get direction
        if (Vector3.Distance(_Target.position, transform.position) > m_Distance)
        {
            var direction = _Target.position - this.transform.position;
            direction.Normalize();
            direction *= _Speed;
            _RigidBody2D.velocity = direction;
        }
        else
        {
            _RigidBody2D.velocity = Vector2.zero;
        }

    }
}
