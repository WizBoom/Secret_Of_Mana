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

    public void SetCurrentPlayer(int playerIndex)
    {
        //Return if index is out of range
        if (playerIndex >= m_Players.Count || playerIndex < 0)
            return;

        var player = m_Players[playerIndex];
        if (player == null)
            return;

        //Set current player and move main camera
        m_CurrentPlayer = player;
        m_CurrentPlayer.m_VisualCharacter.GetComponent<CharacterController2D>().enabled = true;

        var controller = m_CurrentPlayer.m_VisualCharacter.GetComponent<AIController>();
        if (controller)
            m_CurrentPlayer.m_VisualCharacter.GetComponent<AIController>().enabled = false;

        Camera.main.transform.parent = m_CurrentPlayer.m_VisualCharacter.transform;
        Camera.main.transform.localPosition = new Vector3(0, 0, -1);

        //Disable all controllers
        foreach (var p in m_Players)
        {
            if (p != player)
            {
                p.m_VisualCharacter.GetComponent<CharacterController2D>().enabled = false;
                controller = m_CurrentPlayer.m_VisualCharacter.GetComponent<AIController>();
                if (controller)
                    p.m_VisualCharacter.GetComponent<AIController>().enabled = true;
            }
        }
    }
}
