using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterManager
{
    public List<Enemy> m_Enemies;
    public List<Player> m_Players;
    [HideInInspector]
    public Player m_CurrentPlayer;
}
