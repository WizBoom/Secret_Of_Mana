using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    public int m_Speed { get; set; }

    private void Update()
    {
        if (!Mathf.Approximately(Input.GetAxisRaw("Horizontal"), 0f) || 
            !Mathf.Approximately(Input.GetAxisRaw("Vertical"), 0f))
        {
            //Vector2 translation = Vector2.right * m_Character.m_Speed * Input.GetAxisRaw("Horizontal") * Time.deltaTime;
            //transform.Translate(new Vector3(translation.x, translation.y, 0));
            GetComponent<Rigidbody2D>().velocity = new Vector2(m_Speed * Input.GetAxisRaw("Horizontal"),
               m_Speed * Input.GetAxisRaw("Vertical"));
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }
}
