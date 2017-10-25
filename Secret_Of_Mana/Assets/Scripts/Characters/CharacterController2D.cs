using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Direction
{
    Up,
    Down,
    Left,
    Right
};

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController2D : MonoBehaviour
{
    public int m_Speed { get; set; }
    private Rigidbody2D _RigidBody;
    private Direction _PlayerDirection = Direction.Down;

    void Awake()
    {
        _RigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!Mathf.Approximately(Input.GetAxisRaw("Horizontal"), 0f) || 
            !Mathf.Approximately(Input.GetAxisRaw("Vertical"), 0f))
        {
            //Move player
            _RigidBody.velocity = new Vector2(m_Speed * Input.GetAxisRaw("Horizontal"),
               m_Speed * Input.GetAxisRaw("Vertical"));

            //Set player direction
            Direction newDirection = _PlayerDirection;
            if (!Mathf.Approximately(Input.GetAxisRaw("Vertical"), 0f))
            {
                newDirection = Input.GetAxisRaw("Vertical") >= 0 ? Direction.Up : Direction.Down;
            }
            if (!Mathf.Approximately(Input.GetAxisRaw("Horizontal"), 0f))
            {
                newDirection = Input.GetAxisRaw("Horizontal") >= 0 ? Direction.Right : Direction.Left;
            }
            SetDirection(newDirection);
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }

    private void SetDirection(Direction direction)
    {
        //No need to do anything if the direction didn't change
        if (_PlayerDirection == direction)
            return;

        if (Debug.isDebugBuild)
        {
            String debugMessage = "Direction changed to ";
            switch (direction)
            {
                case Direction.Down:
                    Debug.Log(debugMessage + "Down");
                    break;
                case Direction.Left:
                    Debug.Log(debugMessage + "Left");
                    break;
                case Direction.Right:
                    Debug.Log(debugMessage + "Right");
                    break;
                case Direction.Up:
                    Debug.Log(debugMessage + "Up");
                    break;
            }
        }

        _PlayerDirection = direction;
    }
}
