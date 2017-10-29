using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CharacterManager m_CharacterManager;

    private void Awake()
    {
        m_CharacterManager.Init();
    }
}
