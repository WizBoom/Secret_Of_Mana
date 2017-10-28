using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

[CreateAssetMenu(fileName = "Bow", menuName = "Items/Weapons/Bow")]
public class Bow : Weapon
{
    public GameObject m_ArrowPrefab;
    public float m_ArrowSpeed = 1f;
    public int m_ExtraDamage = 5;

    public override void Attack(Direction direction, VisualCharacter visualCharacter)
    {
        Debug.Log("Shooting arrow...");

        //Find out velocity vector and rotation
        float angle = 0f;
        Vector2 velocity = new Vector2(0f, 0f);

        switch (direction)
        {
            case Direction.Left:
                angle = 0f;
                velocity.x = -m_ArrowSpeed;
                break;
            case Direction.Up:
                angle = -90f;
                velocity.y = m_ArrowSpeed;
                break;
            case Direction.Right:
                angle = -180f;
                velocity.x = m_ArrowSpeed;
                break;
            case Direction.Down:
                angle = -270f;
                velocity.y = -m_ArrowSpeed;
                break;
        }
        GameObject arrowObject = Instantiate(m_ArrowPrefab,visualCharacter.transform.position, Quaternion.Euler(0, 0, angle));
        Arrow arrowScript = arrowObject.GetComponent<Arrow>();
        if (!arrowScript)
        {
            Destroy(arrowObject);
            Debug.LogError("No Arrow Script on the arrow object.");
        }

        //Calculate how long the arrow will exist
        float time = m_Range/m_ArrowSpeed;

        arrowScript.Initialize(time, velocity, visualCharacter, this);
    }
}
