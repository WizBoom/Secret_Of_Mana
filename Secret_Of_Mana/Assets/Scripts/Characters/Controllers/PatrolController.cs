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
    private VisualCharacter _Character;

    private void Start()
    {
        //Get Rigidbody
        _RigidBody2D = GetComponent<Rigidbody2D>();

        //Get Speed
        _Speed = GetComponent<VisualCharacter>().m_Character.m_CharacterStats.m_Speed;
    }

    private void Update()
    {
        if (!_Character)
            _Character = GetComponent<VisualCharacter>();

        if (_CurrentTargetIndex >= m_PatrolPoints.Count)
            return;

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

        _Character.m_Character.m_CurrentWeaponTimer += Time.deltaTime;
        if (_Character.m_Character.m_CurrentWeaponTimer >= _Character.m_Character.m_CharacterStats.m_UnarmedRateOfAttack)
        {
            //Check if something is ine attack range
            Vector2 range = new Vector2(_Character.GetComponent<BoxCollider2D>().size.x + _Character.m_Character.m_CharacterStats.m_UnarmedRange,
                _Character.GetComponent<BoxCollider2D>().size.y + _Character.m_Character.m_CharacterStats.m_UnarmedRange);

            Collider2D[] colliders = Physics2D.OverlapCapsuleAll(this.transform.position, range, CapsuleDirection2D.Vertical, 0);
            foreach (var collider in colliders)
            {
                if (collider.tag == "Character" && collider.gameObject.layer == GameManager.m_PlayerLayer)
                {
                    _Character.m_Character.Attack(Direction.Left);
                    _Character.m_Character.m_CurrentWeaponTimer = 0;
                    return;
                }
            }
        }
    }
}
