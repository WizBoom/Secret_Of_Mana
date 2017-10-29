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

    public void Init()
    {
        //Make Enemies
        foreach (var enemy in m_Enemies)
        {
            if (enemy.m_VisualCharacter)
            {
                VisualCharacter character = VisualCharacter.Instantiate(enemy.m_VisualCharacter, enemy.m_LastSavedPosition,
                    Quaternion.identity) as VisualCharacter;
                enemy.m_VisualCharacter = character;
                character.m_Character = enemy;
                enemy.InitCharacter();
            }
        }

        foreach (var player in m_Players)
        {
            if (player.m_VisualCharacter)
            {
                VisualCharacter character = VisualCharacter.Instantiate(player.m_VisualCharacter, player.m_LastSavedPosition,
                    Quaternion.identity);
                //Add character controller, but disable it
                if (!character.gameObject.GetComponent<CharacterController2D>())
                {
                    var controller = character.gameObject.AddComponent<CharacterController2D>();
                    controller.m_Speed = player.m_CharacterStats.m_Speed;
                    controller.enabled = false;
                }
                player.m_VisualCharacter = character;
                character.m_Character = player;
                player.InitCharacter();
            }
        }

        if (m_Players.Count > 0)
        {
            m_CurrentPlayer = m_Players[0];
            m_CurrentPlayer.m_VisualCharacter.GetComponent<CharacterController2D>().enabled = true;
            Camera.main.transform.parent = m_CurrentPlayer.m_VisualCharacter.transform;
        }
    }
}
